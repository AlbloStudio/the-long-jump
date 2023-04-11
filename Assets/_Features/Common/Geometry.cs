
using UnityEngine;

namespace Assets.Scripts.utils
{
    public static class Geometry
    {
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

        public static float CalculateFacingArea(Mesh m)
        {
            Vector3[] mVertices = m.vertices;
            Vector3 result = Vector3.zero;
            for (int p = mVertices.Length - 1, q = 0; q < mVertices.Length; p = q++)
            {
                result += Vector3.Cross(mVertices[q], mVertices[p]);
            }

            Debug.Log(result);
            result *= 0.5f;
            return result.magnitude;
        }

        public static bool HasMeshSurface(Mesh mesh)
        {
            return CalculateFacingArea(mesh) > 0;
        }
    }
}