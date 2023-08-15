using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource musicAudioSource;

    private void Start()
    {
        musicAudioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        if (musicAudioSource.isPlaying) musicAudioSource.Stop();
        musicAudioSource.Play();
    }

    public void Stop()
    {
        if (musicAudioSource != null && musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
        }
    }
}
