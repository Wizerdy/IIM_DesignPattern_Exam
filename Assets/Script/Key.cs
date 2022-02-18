using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Key : MonoBehaviour, IGrabbable, IUsable {
    [SerializeField] Transform key;
    [SerializeField] GameObject door;
    [SerializeField] int useNumber = 1;
    Transform startParent;

    public int UseNumber => useNumber;

    public Transform GrabParent => key;

    private void Reset() {
        key = transform;
    }

    private void Start() {
        startParent = GrabParent.parent;
    }

    public void Grab(Transform grabber) {
        GetComponent<Collider2D>().enabled = false;
    }

    public void Ungrab(Transform grabber) {
        key.parent = startParent;
        GetComponent<Collider2D>().enabled = true;
    }

    public void Use(GameObject user, List<GameObject> inRange) {
        if (door == null || inRange.Count == 0) { return; }

        GameObject theDoor = inRange.Where(go => go.transform.IsChildOf(door.transform)).FirstOrDefault();
        if (theDoor == null) { return; }
        theDoor.transform.parent.gameObject.SetActive(false);
        useNumber -= 1;

        if (useNumber <= 0) {
            key.gameObject.SetActive(false);
            Destroy(key.gameObject);
        }
    }
}
