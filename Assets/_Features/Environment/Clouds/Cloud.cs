using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Cloud : MonoBehaviour
{
    [SerializeField] private Vector2 _velocityRange = new(2f, 0.5f);
    [SerializeField] private Vector2 _sizeRange = new(1f, 5f);

    private ParticleSystem _particles;

    private void Awake()
    {
        _particles = GetComponent<ParticleSystem>();
        ReCalculate();
    }

    public void ReCalculate()
    {
        ParticleSystem.VelocityOverLifetimeModule vel = _particles.velocityOverLifetime;
        vel.x = Random.Range(-_velocityRange.x, _velocityRange.x);
        vel.y = Random.Range(-_velocityRange.y, _velocityRange.y);

        ParticleSystem.MainModule shape = _particles.main;
        shape.startSize = Random.Range(_sizeRange.x, _sizeRange.y);
    }
}
