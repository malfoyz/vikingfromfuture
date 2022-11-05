using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffector : MonoBehaviour
{
    public AudioSource audioSource;   // источник звука
    public AudioClip jumpSound, winSound, loseSound, shotSound;          // звуковые дорожки
    // для каждой из дорожек создаем публичный метод
    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }
    public void PlayWinSound()
    {
        audioSource.PlayOneShot(winSound);
    }
    public void PlayLoseSound()
    {
        audioSource.PlayOneShot(loseSound);
    }
    public void PlayTheShot()
    {
        audioSource.PlayOneShot(shotSound);
    }
}
