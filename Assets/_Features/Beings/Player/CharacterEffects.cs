using UnityEngine;

public class CharacterEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fallParticles;
    [SerializeField] private ParticleSystem _runParticles;
    [SerializeField] private ParticleSystem _jumpParticles;
    [SerializeField] private Vector2 fallRange = new(10, 20);
    [SerializeField] private Vector2 jumpRange = new(5, 10);

    [SerializeField] private float _runRatio = 0.1f;

    private void Awake()
    {
        ParticleSystem.EmissionModule fallEmission = _fallParticles.emission;
        fallEmission.rateOverTime = 0;

        ParticleSystem.EmissionModule jumpEmission = _jumpParticles.emission;
        jumpEmission.rateOverTime = 0;
    }

    public void BurstFall()
    {
        _fallParticles.Emit(Random.Range(Mathf.RoundToInt(fallRange.x), Mathf.RoundToInt(fallRange.y)));
    }

    public void BurstJump()
    {
        _jumpParticles.Emit(Random.Range(Mathf.RoundToInt(jumpRange.x), Mathf.RoundToInt(jumpRange.y)));
    }

    public void ActivateRun(float moveSpeed)
    {
        ParticleSystem.EmissionModule emission = _runParticles.emission;
        emission.rateOverTime = Mathf.Abs(moveSpeed * _runRatio);
    }
}
