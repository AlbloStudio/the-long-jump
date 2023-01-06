using Assets.Scripts.item;
using UnityEngine;

namespace Assets.Scripts.utils
{
    public class Rotation
    {
        public static Quaternion LookAt2D(Vector2 point1, Vector2 point2)
        {
            Vector3 dir = point2 - point1;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            return Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}