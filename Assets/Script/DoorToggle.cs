using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToggle : MonoBehaviour {
    [SerializeField] List<EntityToggle> toggles = new List<EntityToggle>();
    int activeToggles = 0;

    private void Start() {
        for (int i = 0; i < toggles.Count; i++) {
            if (toggles[i] != null) {
                toggles[i].OnToggle += Toggle;
            }
        }
    }

    private void OnDestroy() {
        for (int i = 0; i < toggles.Count; i++) {
            if (toggles[i] != null) {
                toggles[i].OnToggle -= Toggle;
            }
        }
    }

    private void Toggle(bool state) {
        if (state) { activeToggles++; }
        else { activeToggles--; }

        if (activeToggles >= toggles.Count) {
            Open();
        }
    }

    private void Open() {
        gameObject.SetActive(false);
    }
}
