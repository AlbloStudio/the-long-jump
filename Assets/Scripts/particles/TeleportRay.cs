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
    [SerializeField] private float _particleSpeedModifier = 0.5f;

    public TeleportPointSource teleport;

    private MeshFilter _meshFilter;
    private Mesh _mesh;
    private ParticleSystem _particles;

    private Color _initialColor;

    [System.Obsolete]
    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = _mesh = new Mesh();
        _mesh.name = "Teleport Ray";

        _particles = GetComponent<ParticleSystem>();
        _initialColor = _particles.startColor;
    }

    [System.Obsolete]
    private void Update()
    {
        transform.SetPositionAndRotation(teleport.transform.position, Rotation.LookAt2D(transform.position, teleport.TargetPoint.transform.position));

        if (teleport.transform.hasChanged || teleport.TargetPoint.transform.hasChanged)
        {
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
        Vector2 bottomRight = new(points[0].x + pointDistance - (_rayOffset.x * 2), points[0].y - _rayOffset.y);

        Geometry.MeshData meshData = Geometry.CalculateRectangleMesh(topLeft, topRight, bottomLeft, bottomRight);
        _mesh.vertices = meshData.Vertices;
        _mesh.triangles = meshData.Triangles;

    }

    private void OnDrawGizmos()
    {
        foreach (Vector3 vertex in _mesh.vertices)
        {
            Gizmos.DrawSphere(vertex, 1f);
        }
    }

    private void CalculateParticles()
    {
        ParticleSystem.MainModule mainParticles = _particles.main;
        ParticleSystem.EmissionModule emission = _particles.emission;

        float area = 2 * Vector2.Distance(_mesh.vertices[0], _mesh.vertices[2]) * _rayOffset.x;

        float rateModifier = teleport.IsFarAway() ? 0.2f : 1f;
        emission.rateOverTime = _particleEmissionRate * area * rateModifier;

        Vector2 particlesDireciton = Vector3.Normalize(teleport.TargetPoint.transform.position - teleport.transform.position);
        ParticleSystem.VelocityOverLifetimeModule vel = _particles.velocityOverLifetime;
        vel.x = particlesDireciton.x * _particleSpeedModifier;
        vel.y = particlesDireciton.y * _particleSpeedModifier;

        mainParticles.startColor = teleport.IsObstructed() ? Color.black : _initialColor;
        mainParticles.gravityModifier = teleport.IsFarAway() ? 0.3f : 0;
    }
}