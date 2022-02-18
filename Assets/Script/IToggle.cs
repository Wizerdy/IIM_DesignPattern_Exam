using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IToggle {
    bool Active { get; }

    event UnityAction<bool> OnToggle;

    void Toggle();
}
