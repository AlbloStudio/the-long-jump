using Assets.Scripts.managers;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class AppearerEnd : Appearer
{
    [SerializeField] private ParticleSystem _earthQuakeParticles;
    [SerializeField] private float _earthQuakeParticlesEmission = 50f;

    protected override void OnTriggerEnter2D(Collider2D collided)
    {
        base.OnTriggerEnter2D(collided);

        if (collided.transform.Equals(GeneralData.Instance.Player.transform))
        {
            ParticleSystem.EmissionModule emission = _earthQuakeParticles.emission;
            emission.rateOverTime = _earthQuakeParticlesEmission;
        }
    }
}
