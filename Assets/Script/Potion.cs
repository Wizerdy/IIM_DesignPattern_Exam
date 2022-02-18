using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour, IGrabbable, IUsable {
    [SerializeField] Transform potion;
    [SerializeField] int useNumber = 1;
    [SerializeField] int healing = 4;

    Transform startParent;

    public Transform GrabParent => potion;

    public int UseNumber => useNumber;

    private void Reset() {
        potion = transform;
    }

    private void Start() {
        startParent = potion.parent;
    }

    public void Grab(Transform grabber) {
        GetComponent<Collider2D>().enabled = false;
    }

    public void Ungrab(Transform grabber) {
        potion.parent = startParent;
        GetComponent<Collider2D>().enabled = true;
    }

    public void Use(GameObject user, List<GameObject> inRange) {
        user.GetComponent<IHealth>().Heal(healing);

        useNumber--;
        if (useNumber <= 0) {
            potion.gameObject.SetActive(false);
            Destroy(potion.gameObject);
        }
    }
}
