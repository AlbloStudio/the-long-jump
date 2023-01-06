using Assets.Scripts.item;
using Assets.Scripts.utils;
using UnityEngine;

public class TeleportRay : MonoBehaviour
{
    public TeleportPoint point1;
    public TeleportPoint point2;

    private Mesh _mesh;

    private const float OFFSET = 0.1f;
    private const int VERTEX_NUMBER = 4;

    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = _mesh = new Mesh();
        _mesh.name = "Teleport Ray";
    }

    private void Update()
    {
        transform.position = point1.transform.position;
        transform.rotation = Rotation.LookAt2D(transform.position, point2.transform.position);

        CalculateVertices();
    }

    private void CalculateVertices()
    {
        Vector2[] points = new Vector2[]
        {
           Vector2.zero,
            point2.transform.position - transform.position,
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
}