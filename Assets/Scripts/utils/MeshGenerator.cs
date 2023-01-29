using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 _planeSize = new(1, 1);
    [SerializeField] private Vector2 _offset = Vector2.zero;
    [SerializeField] private int _planeResolution = 1;

    public Vector2 PlaneSize { get => _planeSize; set => _planeSize = value; }
    public Vector2 Offset { get => _offset; set => _offset = value; }
    public int PlaneResolution { get => _planeResolution; set => _planeResolution = value; }

    private Mesh _mesh;
    private MeshFilter _meshFilter;
    private readonly List<Vector3> _vertices = new();
    private readonly List<int> _triangles = new();
    private readonly List<Vector2> _uv = new();

    private void Awake()
    {
        _mesh = new Mesh();
        _meshFilter = GetComponent<MeshFilter>();

    }
    public void GeneratePlane()
    {
        _vertices.Clear();
        _triangles.Clear();
        _uv.Clear();

        int resolution = Mathf.Clamp(_planeResolution, 1, 50);

        GenerateVertices(resolution);
        GenerateTriangles(resolution);
        GenerateUV(resolution);

        _meshFilter.mesh = _mesh;
    }

    private void GenerateVertices(int resolution)
    {
        float xPerStep = _planeSize.x / resolution;
        float yPerStep = _planeSize.y / resolution;

        for (int y = 0; y <= resolution; y++)
        {
            for (int x = 0; x <= resolution; x++)
            {
                _vertices.Add(new Vector3(x * xPerStep, y * yPerStep, transform.position.z) + (Vector3)_offset);
            }
        }

        _mesh.vertices = _vertices.ToArray();
    }

    private void GenerateTriangles(int resolution)
    {

        for (int row = 0; row < resolution; row++)
        {
            for (int column = 0; column < resolution; column++)
            {
                int i = (row * resolution) + row + column;
                _triangles.Add(i);
                _triangles.Add(i + resolution + 1);
                _triangles.Add(i + resolution + 2);

                _triangles.Add(i);
                _triangles.Add(i + resolution + 2);
                _triangles.Add(i + 1);

            }
        }

        _mesh.triangles = _triangles.ToArray();
    }

    private void GenerateUV(int resolution)
    {
        for (int row = 0; row < resolution; row++)
        {
            for (int column = 0; column < resolution; column++)
            {
                _uv.Add(new(row, column));
                _uv.Add(new(row, column + 1));
                _uv.Add(new(row + 1, column));
                _uv.Add(new(row + 1, column + 1));
            }
        }

        _mesh.uv = _uv.ToArray();
    }
}

