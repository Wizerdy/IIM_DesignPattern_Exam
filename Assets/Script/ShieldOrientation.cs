using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldOrientation : MonoBehaviour {
    [SerializeField] SpriteRenderer spriteRef;

    Vector3 startLocalPosition = Vector3.zero;

    private void Start() {
        startLocalPosition = transform.localPosition;
    }

    private void LateUpdate() {
        transform.localPosition = !spriteRef.flipX ? startLocalPosition : startLocalPosition.Multiply(new Vector3(-1, 1, 1));
        transform.localScale = spriteRef.flipX ? Vector3.one : new Vector3(-1, 1, 1);
    }
}
