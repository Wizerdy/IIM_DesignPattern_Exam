using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Reference/SoundPlayer")]
public class SoundPlayerReference : Reference<SoundPlayer> {
    public void PlaySound(AudioClip source) => Instance.PlaySound(source);
}
