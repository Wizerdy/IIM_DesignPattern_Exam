using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityToggle : MonoBehaviour, IToggle {
    [SerializeField] bool active = false;

    public bool Active => active;

    [SerializeField] UnityEvent<bool> onToggle;
    public event UnityAction<bool> OnToggle { add => onToggle.AddListener(value); remove => onToggle.RemoveListener(value); }

    public void Toggle() {
        active = !active;
        onToggle?.Invoke(Active);
    }
}
