using UnityEngine;

public class CharacterEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private int minBurst = 10;
    [SerializeField] private int maxBurst = 20;

    private void Awake()
    {
        ParticleSystem.EmissionModule emission = particles.emission;
        emission.rateOverTime = 0;
    }

    public void BurstIt()
    {
        particles.Emit(Random.Range(minBurst, maxBurst));
    }
}
