using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHoldingRenderer : MonoBehaviour {
    [SerializeField] ActiveSwitcher activeSwitcher;
    [SerializeField] EntityGrab grab;
    [SerializeField] EntityFire fire;
    [SerializeField] EntityBlock block;

    bool fired = false;

    private void Start() {
        StartCoroutine(RefreshRender(0.1f));
        fire.OnFire += Fire;
    }

    private void OnDestroy() {
        fire.OnFire -= Fire;
    }

    private IEnumerator RefreshRender(float refreshTime) {
        while (true) {
            yield return new WaitForSeconds(refreshTime);
            activeSwitcher?.Active(ObjectToRender());
        }
    }

    private void Fire() {
        fired = true;
        StartCoroutine(Wait(() => fired = false, 1f));

        IEnumerator Wait(Action action, float time) {
            yield return new WaitForSeconds(time);
            action?.Invoke();
        }
    }

    private string ObjectToRender() {
        string output;
        if (block.IsBlocking) {
            output = "Shield";
        } else if (fired) {
            output = "Bow";
        } else if (grab.HasObjectGrabbed) {
            output = "Item";
        } else {
            output = "Bow";
        }

        return output;
    }
}
