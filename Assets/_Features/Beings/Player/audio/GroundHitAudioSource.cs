using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GroundHitAudioSource : MonoBehaviour
{
    [SerializeField] private AudioClip _groundedClip;

    private AudioSource _audioSource;
    private AudioTriggerByClips _triggerByClips;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _triggerByClips = new AudioTriggerByClips(_audioSource);
    }

    public void PlaySound()
    {
        _triggerByClips.PlaySoundByClip(_groundedClip, new Vector2(0.95f, 1.05f), new Vector2(0.9f, 1f));
    }
}
