using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlShakeReferenceSetter : MonoBehaviour
{
    [SerializeField] ControlShake controlShake;
    [SerializeField] ControlShakeReference controlShakeRef;

    private void Reset() {
        controlShake = GetComponent<ControlShake>();
    }

    void Awake() {
        (controlShakeRef as IReferenceSetter<ControlShake>).SetInstance(controlShake);
    }
}
