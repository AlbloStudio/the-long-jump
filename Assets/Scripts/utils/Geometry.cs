
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
    }
}