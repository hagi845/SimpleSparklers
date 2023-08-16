using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        Stop();
        audioSource.Play();
    }

    public void Stop()
    {
        if (audioSource.isPlaying) audioSource.Stop();
    }
}
