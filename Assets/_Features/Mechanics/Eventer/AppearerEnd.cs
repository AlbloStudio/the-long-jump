using Assets.Scripts.managers;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class AppearerEnd : Appearer
{
    [SerializeField] private AudioSource _earthQuakeAudioSource;
    [SerializeField] private ParticleSystem _earthQuakeParticles;
    [SerializeField] private float _earthQuakeParticlesEmission = 50f;

    protected override void OnTriggerEnter2D(Collider2D collided)
    {
        Assets.Scripts.being.CharacterMover player = GeneralData.Instance.Player;

        base.OnTriggerEnter2D(collided);

        if (collided.transform.Equals(player.transform))
        {
            ParticleSystem.EmissionModule emission = _earthQuakeParticles.emission;
            emission.rateOverTime = _earthQuakeParticlesEmission;

            if (!_earthQuakeAudioSource.isPlaying)
            {
                _earthQuakeAudioSource.Play();
            }

            player.GoBerserk();
        }
    }
}
