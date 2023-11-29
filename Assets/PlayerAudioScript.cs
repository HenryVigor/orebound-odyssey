using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioScript : MonoBehaviour
{

    public AudioSource audioSourceHurt;
    public AudioSource audioSourceMine;

    public void PlaySoundHurt() {
        audioSourceHurt.Play();
    }

    public void PlaySoundMine() {
        audioSourceMine.Play();
    }
}
