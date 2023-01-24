
using UnityEngine;

namespace Assets.Scripts.utils
{
    public static class Geometry
    {
        private const int VERTEX_NUMBER = 4;

        public class MeshData
        {
            public Vector3[] Vertices;
            public int[] Triangles;
        }

        public enum Position
        {
            top = 0,
            right = 1,
            bottom = 2,
            left = 3,
        }

        public static Vector3 GetExtremePoint(Vector2[] points, Position position)
        {
            Vector2 extreme = points[0];

            for (int i = 1; i < points.Length; i++)
            {
                switch (position)
                {
                    case Position.top:
                        if (points[i].y > extreme.y)
                        {
                            extreme = points[i];
                        }

                        break;

                    case Position.bottom:
                        if (points[i].y < extreme.y)
                        {
                            extreme = points[i];
                        }

                        break;

                    case Position.right:
                        if (points[i].x > extreme.x)
                        {
                            extreme = points[i];
                        }

                        break;

                    case Position.left:
                        if (points[i].x < extreme.x)
                        {
                            extreme = points[i];
                        }

                        break;
                    default:
                        break;
                }
            }

            return extreme;
        }

        public static MeshData CalculateRectangleMesh(Vector2 topLeft, Vector2 topRight, Vector2 bottomLeft, Vector2 bottomRight)
        {

            Vector3[] vertices = new Vector3[VERTEX_NUMBER];

            vertices[0] = bottomLeft;
            vertices[1] = topLeft;
            vertices[2] = bottomRight;
            vertices[3] = topRight;

            MeshData mesh = new()
            {
                Vertices = vertices,
                Triangles = new int[] { 0, 1, 2, 2, 1, 3 }
            };

            return mesh;
        }
    }
}