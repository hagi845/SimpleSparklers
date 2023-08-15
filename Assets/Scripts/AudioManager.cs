using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource musicAudioSource;

    private void Start()
    {
        musicAudioSource = GetComponent<AudioSource>();

        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (!musicAudioSource.isPlaying)
        {
            musicAudioSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (musicAudioSource != null && musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
        }
    }
}
