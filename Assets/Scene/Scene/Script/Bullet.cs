using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour, IRecyclable
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _speed;
    [SerializeField] float _collisionCooldown = 0.5f;

    GameObject launcher;
    bool isActive = true;

    [SerializeField] UnityEvent onTouch;

    public Vector3 Direction { get; private set; }
    public int Power { get; private set; }
    float LaunchTime { get; set; }

    bool IRecyclable.IsActive => isActive;

    internal Bullet Init(Vector3 direction, int power, GameObject launcher)
    {
        Direction = direction;
        Power = power;
        LaunchTime = Time.fixedTime;
        this.launcher = launcher;
        isActive = true;
        return this;
    }

    void FixedUpdate()
    {
        _rb.MovePosition((transform.position + (Direction.normalized * _speed)));
    }
    
    void LateUpdate()
    {
        transform.rotation = EntityRotation.AimPositionToZRotation(transform.position, transform.position + Direction);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //if (Time.fixedTime < LaunchTime + _collisionCooldown) return;
        TouchedSomething(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (Time.fixedTime < LaunchTime + _collisionCooldown) return;
        if (TouchedSomething(collision.gameObject)) {
            (this as IRecyclable).Deactivate();
            onTouch?.Invoke();
        }
    }

    private bool TouchedSomething(GameObject entity) {
        if (entity.transform.IsChildOf(launcher.transform)) { return false; }

        entity.GetComponent<IHealth>()?.TakeDamage(Power);
        entity.GetComponent<IToggle>()?.Toggle();
        return true;
    }

    void IRecyclable.Reactivate(Vector3 position, Quaternion rotation) {
        isActive = true;
        gameObject.SetActive(true);
        transform.position = position;
        transform.rotation = rotation;
    }

    void IRecyclable.Deactivate() {
        isActive = false;
        gameObject.SetActive(false);
    }

    void IRecyclable.Delete() {
        Destroy(gameObject);
    }
}
