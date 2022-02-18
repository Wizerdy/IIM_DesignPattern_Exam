using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderReferenceSetter : MonoBehaviour {
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] SceneLoaderReference sceneLoaderReference;

    private void Reset() {
        sceneLoader = GetComponent<SceneLoader>();
    }

    void Awake() {
        (sceneLoaderReference as IReferenceSetter<SceneLoader>).SetInstance(sceneLoader);
    }
}
