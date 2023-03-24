using Assets.Scripts.managers;
using UnityEngine;

public class Earthquake : MonoBehaviour
{
    [SerializeField] private AudioSource _earthQuakeAudioSource;

    private void OnTriggerEnter2D(Collider2D collided)
    {
        if (collided.transform.Equals(GeneralData.Instance.Player.transform))
        {
            if (!_earthQuakeAudioSource.isPlaying)
            {
                _earthQuakeAudioSource.Play();
            }
        }
    }
}
