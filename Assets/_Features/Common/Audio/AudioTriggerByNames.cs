using System.Collections.Generic;
using UnityEngine;

public class AudioTriggerByNames<AudioNames> : AudioTriggerByClips
{
    private readonly Dictionary<AudioNames, AudioClip> _clips = new();

    public AudioTriggerByNames(AudioSource audioSource, Dictionary<AudioNames, AudioClip> clips) : base(audioSource)
    {
        _clips = clips;
    }

    public void PlaySoundByName(AudioNames name, Vector2? pitchRange = null, Vector2? volumeRange = null)
    {
        PlaySoundByClip(_clips[name], pitchRange, volumeRange);
    }
}
