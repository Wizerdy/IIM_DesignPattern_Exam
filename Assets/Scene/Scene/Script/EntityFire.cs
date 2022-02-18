using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EntityFire : MonoBehaviour
{
    [SerializeField] Transform _spawnPoint;
    //[SerializeField] Bullet _bulletPrefab;
    [SerializeField] BulletPoolReference bulletRef;
    [SerializeField] GameObject entity;
    [SerializeField] int damages = 2;

    public Action OnFire;

    private void Reset() {
        entity = gameObject;
    }

    public void FireBullet(int power)
    {
        //var b = Instantiate(_bulletPrefab, _spawnPoint.transform.position, Quaternion.identity, null)
        //    .Init(_spawnPoint.TransformDirection(Vector3.right), power, entity);
        bulletRef.Instance.Add(_spawnPoint.transform.position, Quaternion.identity,
            _spawnPoint.TransformDirection(Vector3.right), damages, entity);
        OnFire?.Invoke();
    }
}
