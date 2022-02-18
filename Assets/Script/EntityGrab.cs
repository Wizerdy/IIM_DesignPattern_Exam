using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EntityGrab : MonoBehaviour {
    [SerializeField] Transform entity;
    [SerializeField] GameObject grabbedObject;
    [SerializeField] Transform grabbedObjectParent;
    [SerializeField] List<GameObject> grabbableInRange = new List<GameObject>();
    [SerializeField] List<GameObject> objectsInRange = new List<GameObject>();

    public bool HasObjectGrabbed { get { return grabbedObject != null; } }

    #region Collisions

    public void OnTriggerEnter2D(Collider2D collision) {
        OnRangeEnter(collision.gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        OnRangeEnter(collision.gameObject);
    }

    public void OnTriggerExit2D(Collider2D collision) {
        OnRangeExit(collision.gameObject);
    }

    public void OnCollisionExit2D(Collision2D collision) {
        OnRangeExit(collision.gameObject);
    }

    private void OnRangeEnter(GameObject obj) {
        objectsInRange.Add(obj);

        IGrabbable item = obj.GetComponentInParent<IGrabbable>();
        if (item != null) {
            grabbableInRange.Add(obj);
            return;
        }
    }

    private void OnRangeExit(GameObject obj) {
        objectsInRange.Remove(obj);

        IGrabbable item = obj.GetComponentInParent<IGrabbable>();
        if (item != null) {
            grabbableInRange.Remove(obj);
            return;
        }
    }

    #endregion

    public bool GrabNearest() {
        if (grabbableInRange.Count == 0) { return false; }
        if (grabbableInRange.Count <= 1) { Grab(grabbableInRange[0]); return true; }

        GameObject obj = grabbableInRange
            .Select(go => (go, Vector3.Distance(entity.position, go.transform.position)))
            .Aggregate((a, b) => a.Item2 < b.Item2 ? a : b)
            .go;

        Grab(obj);
        return true;
    }

    public void Grab(GameObject obj) {
        IGrabbable grabbed = obj.GetComponent<IGrabbable>();
        if (grabbed == null) { return; }
        grabbed.GrabParent.parent = grabbedObjectParent;
        grabbed.GrabParent.localPosition = Vector3.zero;
        grabbed.Grab(entity);
        grabbedObject = obj;
        grabbableInRange.Remove(obj);
        objectsInRange.Remove(obj);
    }

    public void Ungrab() {
        if (grabbedObject == null) { return; }

        IGrabbable grabbed = grabbedObject.GetComponent<IGrabbable>();
        grabbed.GrabParent.parent = null;
        grabbed.Ungrab(entity);
        grabbedObject = null;
    }

    public void Use() {
        if (grabbedObject != null) {
            IUsable usable = grabbedObject.GetComponent<IUsable>();
            if (usable != null) {
                usable.Use(entity.gameObject, objectsInRange);
                if (usable.UseNumber <= 0) { Ungrab(); }
                return;
            }
        }

        for (int i = 0; i < objectsInRange.Count; i++) {
            objectsInRange[i].GetComponent<IUsable>()?.Use(entity.gameObject, grabbableInRange);
        }
    }
}
