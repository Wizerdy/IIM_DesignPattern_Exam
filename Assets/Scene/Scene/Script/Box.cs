using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Box : MonoBehaviour, IToggle {
    [System.Serializable]
    private struct Drop {
        public float probability;
        public GameObject item;
    }

    [SerializeField] List<Drop> droppable = new List<Drop>();
    bool active = false;

    public bool Active => active;

    public event UnityAction<bool> OnToggle;

    public void Toggle() {
        active = !active;
        if (Active) {
            DropItem();
            Destroy(gameObject);
        }
    }

    private void DropItem() {
        GameObject drop = RandomDrop();
        if (drop != null) {
            Instantiate(drop, transform.position, Quaternion.identity);
        }
    }

    private GameObject RandomDrop() {
        if (droppable.Count == 0) { return null; }
        //if (droppable.Count == 1) { return droppable[0].item; }

        float probTot = 0;
        for (int i = 0; i < droppable.Count; i++) {
            probTot += droppable[i].probability;
        }

        probTot = Mathf.Max(1f, probTot);

        float random = Random.Range(0f, probTot);
        //Debug.Log(random);
        for (int i = 0; i < droppable.Count; i++) {
            random -= droppable[i].probability;
            if (random <= 0) {
                return droppable[i].item;
            }
        }
        return null;
    }
}
