using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletPool : ObjectPool<Bullet> {
    public void Add(Vector3 position, Quaternion rotation, Vector3 direction, int power, GameObject launcher) {
        Bullet bullet = GetUsable();
        if (bullet != default(Bullet)) {
            bullet.Init(direction, power, launcher);
            bullet.GetComponent<IRecyclable>()?.Reactivate(position, rotation);
        } else {
            bullet = Instantiate(prefab.gameObject, position, rotation, transform).GetComponent<Bullet>().Init(direction, power, launcher);
            objects.Add(bullet);
        }

        int number = 0;
        for (int i = 0; i < objects.Count; i++) {
            if ((objects[i] as IRecyclable).IsActive) {
                number++;
            }
        }
        objectsInUse = Mathf.Max(objectsInUse, number);
    }
}
