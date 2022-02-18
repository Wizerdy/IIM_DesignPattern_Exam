using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActiveSwitcher : MonoBehaviour {
    [System.Serializable] struct NamedGameobject {
        public string name;
        public GameObject gameObject;
    }

    public Dictionary<string, GameObject> objects = new Dictionary<string,GameObject>();
    [SerializeField] List<NamedGameobject> keys = new List<NamedGameobject>();

    private void Reset() {
        keys = new List<NamedGameobject>();
        objects = new Dictionary<string, GameObject>();
    }

    private void OnValidate() {
        objects = new Dictionary<string, GameObject>();
        objects = keys.ToDictionary(k => k.name, k => k.gameObject);
    }

    public void Active(string name) {
        if (!objects.ContainsKey(name)) { return; }

        foreach (KeyValuePair<string, GameObject> obj in objects) {
            obj.Value?.SetActive(false);
        }
        objects[name]?.SetActive(true);
    }
}
