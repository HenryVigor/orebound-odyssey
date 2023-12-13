using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <class summary>
/// Collects all audio sources for the player into one script
/// </class summary>
public class PlayerAudio : MonoBehaviour
{
    // Get audio source components
    public AudioSource audioSourceHurt;
    public AudioSource audioSourceMine;
    public AudioSource audioSourcePowerup;

    /// <summary>
    /// Plays hurt sound component
    /// </summary>
    public void PlaySoundHurt() {
        audioSourceHurt.Play();
    }

    /// <summary>
    /// Plays mine sound component
    /// </summary>
    public void PlaySoundMine() {
        audioSourceMine.Play();
    }

    /// <summary>
    /// Plays powerup sound component
    /// </summary>
    public void PlaySoundPowerup() {
        audioSourcePowerup.Play();
    }
}
