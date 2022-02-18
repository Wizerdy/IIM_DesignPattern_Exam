using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    [SerializeField] AudioSource sourcePrefab;

    public void PlaySound(AudioClip clip) {
        AudioSource source = Instantiate(sourcePrefab.gameObject, transform.position, Quaternion.identity).GetComponent<AudioSource>();
        source.clip = clip;
        source.Play();
    }
}
