using UnityEngine;

namespace Assets.Scripts.platforms
{
    public class Gap : MonoBehaviour
    {
        [Tooltip("the distance in unity units from left platform to right one")]
        public float distanceX = 15f;
        public float distanceY = 0;

        [Tooltip("the left platform")]
        public EdgeCollider2D left;
        [Tooltip("The right platform")]
        public EdgeCollider2D right;
    }
}
