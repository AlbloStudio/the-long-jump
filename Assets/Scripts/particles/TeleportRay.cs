using Assets.Scripts.item;
using Assets.Scripts.utils;
using UnityEngine;

public class TeleportRay : MonoBehaviour
{
    private const int VERTEX_NUMBER = 4;

    [Tooltip("object where line starts")]
    public TeleportPointSource sourcePoint;
    [Tooltip("object where line goes")]
    public Jumper targetPoint;

    [Tooltip("offset from one point to another")]
    [SerializeField] private Vector2 rayOffset = new(0.1f, 0.02f);

    [Tooltip("Particle amount")]
    [SerializeField] private float _particleEmissionRate = 200f;

    [Tooltip("How fast particles move from source to target")]
    [SerializeField] private float _particleSpeedModifier = 0.5f;

    private Mesh _mesh;
    private ParticleSystem _particles;

    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = _mesh = new Mesh();
        _mesh.name = "Teleport Ray";

        _particles = GetComponent<ParticleSystem>();
    }

    [System.Obsolete]
    private void Update()
    {
        transform.SetPositionAndRotation(sourcePoint.transform.position, Rotation.LookAt2D(transform.position, targetPoint.transform.position));

        CalculateVertices();

        CalculateParticles();
    }

    private void CalculateVertices()
    {
        Vector2[] points = new Vector2[]
        {
           Vector2.zero,
            targetPoint.transform.position - transform.position,
        };

        Vector3[] vertices = new Vector3[VERTEX_NUMBER];

        float pointDistance = Vector2.Distance(points[0], points[1]);

        vertices[0] = new Vector2(points[0].x - rayOffset.x, points[0].y - rayOffset.y);
        vertices[1] = new Vector2(points[0].x - rayOffset.x, points[0].y + rayOffset.y);
        vertices[2] = new Vector2(points[0].x + pointDistance - (rayOffset.x * 2), points[0].y - rayOffset.y);
        vertices[3] = new Vector2(points[0].x + pointDistance - (rayOffset.x * 2), points[0].y + rayOffset.y);

        _mesh.vertices = vertices;

        _mesh.triangles = new int[]
         {
            0,
            1,
            2,
            2,
            1,
            3
        };
    }

    [System.Obsolete]
    private void CalculateParticles()
    {
        float area = 2 * Vector2.Distance(_mesh.vertices[0], _mesh.vertices[2]) * rayOffset.x;
        _particles.emissionRate = _particleEmissionRate * area;

        Vector2 particlesDireciton = Vector3.Normalize(targetPoint.transform.position - sourcePoint.transform.position);
        ParticleSystem.VelocityOverLifetimeModule vel = _particles.velocityOverLifetime;
        vel.x = particlesDireciton.x * _particleSpeedModifier;
        vel.y = particlesDireciton.y * _particleSpeedModifier;
    }
}