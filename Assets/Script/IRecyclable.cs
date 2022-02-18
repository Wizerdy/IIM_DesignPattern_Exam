using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRecyclable {
    bool IsActive { get; }

    void Reactivate(Vector3 position, Quaternion rotation);
    void Deactivate();
    void Delete();
}
