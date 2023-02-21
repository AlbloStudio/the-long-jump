using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private Vector2 _velocityRange = new(2f, 0.5f);

    private void Awake()
    {
        ParticleSystem particles = GetComponent<ParticleSystem>();

        ParticleSystem.VelocityOverLifetimeModule vel = particles.velocityOverLifetime;
        vel.x = Random.Range(-_velocityRange.x, _velocityRange.x);
        vel.y = Random.Range(-_velocityRange.y, _velocityRange.y);
    }
}
