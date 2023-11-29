using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAudioScript : MonoBehaviour
{
    public AudioSource audioSourceHurt;

    public void PlaySoundHurt() {
        audioSourceHurt.Play();
    }
}
