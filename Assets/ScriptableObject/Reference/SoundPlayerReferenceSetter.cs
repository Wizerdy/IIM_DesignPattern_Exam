using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayerReferenceSetter : MonoBehaviour
{
    [SerializeField] SoundPlayer soundPlayer;
    [SerializeField] SoundPlayerReference soundPlayerRef;

    private void Reset() {
        soundPlayer = GetComponent<SoundPlayer>();
    }

    void Awake() {
        (soundPlayerRef as IReferenceSetter<SoundPlayer>).SetInstance(soundPlayer);
    }
}
