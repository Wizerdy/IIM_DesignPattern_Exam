using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reference/SceneLoaderReference")]
public class SceneLoaderReference : Reference<SceneLoader> {
    public void LoadScene(string name) => Instance?.LoadScene(name);
    public void ReloadScene() => Instance?.ReloadScene();
}
