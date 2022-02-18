using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable {
    Transform GrabParent { get; }

    void Grab(Transform grabber);
    void Ungrab(Transform grabber);
}
