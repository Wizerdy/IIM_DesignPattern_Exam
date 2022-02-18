using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ToolsBox {
    public static Vector3 Multiply(this Vector3 vector1, Vector3 vector2) {
        return new Vector3(vector1.x * vector2.x, vector1.y * vector2.y, vector1.z * vector2.z);
    }
}
