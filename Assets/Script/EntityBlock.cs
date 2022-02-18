using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityBlock : MonoBehaviour {
    [SerializeField] Health health;
    [SerializeField] ActiveSwitcher spriteSwitcher;

    bool isBlocking = false;

    public bool IsBlocking { get { return isBlocking; } set { Block(value); } }

    public void Block(bool state) {
        isBlocking = state;
        health.CanTakeDamage = !isBlocking;
        //spriteSwitcher?.Active((state ? "Shield" : "Bow"));
    }
}
