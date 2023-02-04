
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.utils
{
    public static class MeshUtils
    {
        public class MeshData
        {
            public Vector3[] Vertices;
            public int[] Triangles;
        }

        public static void GenerateMesh(int planeResolution, Vector2 planeSize, Transform origin, Vector3 offset, ref Mesh mesh)
        {
            if (!mesh)
            {
                mesh = new();
            }

            mesh.Clear();

            int resolution = Mathf.Clamp(planeResolution, 1, 50);

            GenerateVertices(resolution, planeSize, origin, offset, ref mesh);
            GenerateTriangles(resolution, ref mesh);
        }

        private static void GenerateVertices(int resolution, Vector2 planeSize, Transform origin, Vector3 offset, ref Mesh mesh)
        {
            List<Vector3> _vertices = new();
            List<Vector2> _uv = new();
            List<Vector3> _normal = new();

            float xPerStep = planeSize.x / resolution;
            float yPerStep = planeSize.y / resolution;

            for (int y = 0; y <= resolution; y++)
            {
                for (int x = 0; x <= resolution; x++)
                {
                    _vertices.Add(new Vector3(x * xPerStep, y * yPerStep, origin.position.z) + offset);
                    _uv.Add(new Vector2(x / (float)resolution, y / (float)resolution));
                    _normal.Add(new Vector3(0, 0, -1));

                }
            }

            mesh.vertices = _vertices.ToArray();
            mesh.uv = _uv.ToArray();
            mesh.normals = _normal.ToArray();
        }

        private static void GenerateTriangles(int resolution, ref Mesh mesh)
        {
            List<int> triangles = new();

            for (int row = 0; row < resolution; row++)
            {
                for (int column = 0; column < resolution; column++)
                {
                    int i = (row * resolution) + row + column;
                    triangles.Add(i);
                    triangles.Add(i + resolution + 1);
                    triangles.Add(i + resolution + 2);

                    triangles.Add(i);
                    triangles.Add(i + resolution + 2);
                    triangles.Add(i + 1);

                }
            }

            mesh.triangles = triangles.ToArray();
        }
    }
}