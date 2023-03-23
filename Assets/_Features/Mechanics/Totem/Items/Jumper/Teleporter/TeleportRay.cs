using Assets.Scripts.item;
using Assets.Scripts.utils;
using UnityEngine;

public class TeleportRay : MonoBehaviour
{

    [Tooltip("offset from one point to another")]
    [SerializeField] private Vector2 _rayOffset = new(0.1f, 0.02f);

    [Tooltip("Particle amount")]
    [SerializeField] private float _particleEmissionRate = 200f;

    [Tooltip("How fast particles move from source to target")]
    [SerializeField] private float _particleSpeedModifier = 5f;

    public TeleportPointSource teleport;

    private MeshGenerator _meshGenerator;
    private MeshFilter _meshFilter;
    private ParticleSystem _particles;
    private BoxCollider2D _collider;

    private Color _initialColor;

    private bool _isFarAway = false;
    private bool _isObstructed = false;
    private float _initialLifeTime;

    [System.Obsolete]
    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshGenerator = GetComponent<MeshGenerator>();
        _collider = GetComponent<BoxCollider2D>();

        _particles = GetComponent<ParticleSystem>();
        _initialColor = _particles.startColor;
        _initialLifeTime = _particles.main.startLifetime.constant;
    }

    private void Update()
    {
        transform.SetPositionAndRotation(teleport.transform.position, Rotation.LookAt2D(transform.position, teleport.TargetPoint.transform.position));

        if (teleport.transform.hasChanged || teleport.TargetPoint.transform.hasChanged)
        {
            teleport.transform.hasChanged = false;
            teleport.TargetPoint.transform.hasChanged = false;

            RenderRay();
        }
    }

    private void RenderRay()
    {
        ParticleSystem.EmissionModule emission = _particles.emission;

        if (!teleport.IsRayTrazable())
        {
            emission.enabled = false;
        }
        else
        {
            emission.enabled = true;

            CalculateVertices();
            CalculateParticles();
        }
    }

    private void CalculateVertices()
    {
        Vector2[] points = new Vector2[]
        {
            Vector2.zero,
            teleport.TargetPoint.transform.position - transform.position,
        };

        float pointDistance = Vector2.Distance(points[0], points[1]);

        Vector2 topLeft = new(points[0].x - _rayOffset.x, points[0].y + _rayOffset.y);
        Vector2 topRight = new(points[0].x + pointDistance - (_rayOffset.x * 2), points[0].y + _rayOffset.y);
        Vector2 bottomLeft = new(points[0].x - _rayOffset.x, points[0].y - _rayOffset.y);

        _meshGenerator.PlaneSize = new(topRight.x - topLeft.x, topLeft.y - bottomLeft.y);
        _meshGenerator.Offset = new(0, bottomLeft.y);
        _meshGenerator.GeneratePlane();

        _collider.size = new Vector2(Vector2.Distance(topRight, topLeft), 1);
        _collider.offset = new Vector2(_collider.size.x / 2, 0.17f);
    }

    private void CalculateParticles()
    {
        ParticleSystem.MainModule mainParticles = _particles.main;
        ParticleSystem.EmissionModule emission = _particles.emission;

        _isFarAway = teleport.IsFarAway();
        _isObstructed = teleport.IsObstructed();

        float area = 2 * Vector2.Distance(_meshFilter.mesh.vertices[0], _meshFilter.mesh.vertices[1]);

        float rateModifier = _isFarAway ? 0.2f : 1f;
        float emissionRate = _particleEmissionRate * area * rateModifier;
        emission.rateOverTime = emissionRate;
        mainParticles.startLifetime = _isFarAway ? 0.1f : _initialLifeTime;

        Vector2 particlesDireciton = Vector3.Normalize(teleport.TargetPoint.transform.position - teleport.transform.position);
        ParticleSystem.VelocityOverLifetimeModule vel = _particles.velocityOverLifetime;
        vel.x = _isFarAway ? 0 : particlesDireciton.x * _particleSpeedModifier;
        vel.y = _isFarAway ? 0 : particlesDireciton.y * _particleSpeedModifier;

        mainParticles.startColor = _isObstructed ? new Color(0, 0, 0, _isFarAway ? 0.5f : 0.15f) : _initialColor;
    }
}