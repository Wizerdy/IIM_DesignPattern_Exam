using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolReferenceSetter : MonoBehaviour {
    [SerializeField] BulletPool bulletPool;
    [SerializeField] BulletPoolReference bulletPoolRef;

    private void Reset() {
        bulletPool = GetComponent<BulletPool>();
    }

    void Awake() {
        (bulletPoolRef as IReferenceSetter<BulletPool>).SetInstance(bulletPool);
    }
}
