
using UnityEngine;

namespace Assets.Scripts.utils
{
    public static class Geometry
    {
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

        public static Vector2 GetRandomPointInArea(Collider2D area)
        {
            Vector2 center = area.bounds.center;
            Vector2 size = area.bounds.size;

            return center + new Vector2((Random.value - 0.5f) * size.x, (Random.value - 0.5f) * size.y);
        }
    }
}