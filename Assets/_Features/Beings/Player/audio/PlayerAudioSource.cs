using System.Collections.Generic;
using UnityEngine;
using static Enum;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudioSource : MonoBehaviour
{
    [SerializeField] private AudioClip _jumpClip;
    [SerializeField] private AudioClip _groundedClip;
    [SerializeField] private AudioClip _drownClip;

    private AudioSource _audioSource;
    private AudioTriggerByNames<PlayerSounds> _trigger;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        Dictionary<PlayerSounds, AudioClip> audios = new()
        {
            {PlayerSounds.Jump, _jumpClip},
            {PlayerSounds.Grounded, _groundedClip},
            {PlayerSounds.Drown, _drownClip},
        };

        _trigger = new AudioTriggerByNames<PlayerSounds>(_audioSource, audios);
    }

    public void PlaySound(PlayerSounds type)
    {
        _trigger.PlaySoundByName(type, new Vector2(0.95f, 1.05f));
    }
}
