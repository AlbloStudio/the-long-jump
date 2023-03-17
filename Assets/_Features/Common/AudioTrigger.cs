using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger<AudioNames>
{
    private readonly Dictionary<AudioNames, AudioClip> _clips = new();
    private readonly AudioSource _audioSource;

    public AudioTrigger(AudioSource audioSource, Dictionary<AudioNames, AudioClip> clips)
    {
        _audioSource = audioSource;
        _clips = clips;
    }

    public void PlaySound(AudioNames name)
    {
        AudioClip clipItem = _clips[name];

        if (clipItem != null)
        {
            _audioSource.pitch = Random.Range(0.95f, 1.05f);
            _audioSource.PlayOneShot(clipItem);
        }
    }
}
