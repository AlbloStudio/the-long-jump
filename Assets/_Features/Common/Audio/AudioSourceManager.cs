using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceManager : MonoBehaviour
{
    [SerializeField] private Vector2 _frequency = new();
    [SerializeField] private List<AudioClip> clips = new();
    [SerializeField] private Vector2 _pitch = new(1f, 1f);
    [SerializeField] private Vector2 _volume = new(0.8f, 1f);
    private AudioTriggerByClips _audioClips;

    private void Awake()
    {
        _audioClips = new AudioTriggerByClips(GetComponent<AudioSource>());
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(_frequency.x, _frequency.y));
        _ = StartCoroutine(StartSounds());
    }

    private IEnumerator StartSounds()
    {
        while (true)
        {
            PlayRandomSound();
            yield return new WaitForSeconds(Random.Range(_frequency.x, _frequency.y));
        }
    }

    private void PlayRandomSound()
    {
        int newClipIndex = Random.Range(0, clips.Count - 1);
        _audioClips.PlaySoundByClip(clips[newClipIndex], _pitch, _volume);
    }
}
