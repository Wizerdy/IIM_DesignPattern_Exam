using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsable {
    int UseNumber { get; }
    void Use(GameObject user, List<GameObject> inRange);
}
