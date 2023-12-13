using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <class summary>
/// Collects all audio sources for enemies into one script
/// </class summary>
public class EnemyAudio : MonoBehaviour
{
    // Get audio source components
    public AudioSource audioSourceHurt;

    /// <summary>
    /// Plays hurt sound component
    /// </summary>
    public void PlaySoundHurt() {
        audioSourceHurt.Play();
    }
}
