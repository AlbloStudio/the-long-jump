using Assets.Scripts.item;
using Assets.Scripts.utils;
using UnityEngine;

public class TeleportRay : MonoBehaviour
{
    private const float OFFSET = 0.1f;
    private const int VERTEX_NUMBER = 4;

    [Tooltip("object where line starts")]
    public TeleportPointSource sourcePoint;
    [Tooltip("object where line goes")]
    public Jumper targetPoint;

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

        vertices[0] = new Vector2(points[0].x, points[0].y - OFFSET);
        vertices[1] = new Vector2(points[0].x, points[0].y + OFFSET);
        vertices[2] = new Vector2(points[0].x + pointDistance, points[0].y - OFFSET);
        vertices[3] = new Vector2(points[0].x + pointDistance, points[0].y + OFFSET);

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
        float area = 2 * OFFSET * Vector2.Distance(_mesh.vertices[0], _mesh.vertices[2]);
        _particles.emissionRate = 200f * area;
    }
}