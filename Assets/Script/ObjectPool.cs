using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : IRecyclable {
    [SerializeField] protected T prefab;
    [SerializeField] protected int deleteDelta = 1;
    protected List<T> objects = new List<T>();

    protected int objectsInUse;

    private void Start() {
        StartCoroutine(Regulation(1f));
    }

    protected T GetUsable() {
        if (objects.Count == 0) { return default(T); }
        for (int i = 0; i < objects.Count; i++) {
            if (!objects[i].IsActive) {
                return objects[i];
            }
        }
        return default(T);
    }

    IEnumerator Regulation(float refreshTime) {
        while (true) {
            yield return new WaitForSeconds(refreshTime);
            if (objectsInUse + deleteDelta < objects.Count) {
                List<T> toBin = objects.Where(rec => rec.IsActive == false).Take(objects.Count - (objectsInUse + deleteDelta)).ToList();
                for (int i = 0; i < toBin.Count; i++) {
                    toBin[i].Delete();
                    objects.Remove(toBin[i]);
                }
            }
            objectsInUse = 0;
        }
    }
}
