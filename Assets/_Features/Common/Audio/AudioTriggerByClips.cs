using UnityEngine;

public class AudioTriggerByClips
{
    protected readonly AudioSource _audioSource;

    public AudioTriggerByClips(AudioSource audioSource)
    {
        _audioSource = audioSource;
    }

    public void PlaySoundByClip(AudioClip clipItem, Vector2? pitchRange = null, Vector2? volumeRange = null)
    {
        if (clipItem == null)
        {
            return;
        }

        pitchRange ??= new Vector2(1f, 1f);
        volumeRange ??= new Vector2(1f, 1f);

        _audioSource.pitch = Random.Range(pitchRange.Value.x, pitchRange.Value.y);
        _audioSource.volume = Random.Range(volumeRange.Value.x, volumeRange.Value.y);

        _audioSource.PlayOneShot(clipItem);
    }
}
