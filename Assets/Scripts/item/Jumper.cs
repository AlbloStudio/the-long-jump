using UnityEngine;

namespace Assets.Scripts.item
{
    public class Jumper : MonoBehaviour
    {
        private Collider2D collider;

        private void OnEnable()
        {
            collider = GetComponent<Collider2D>();
        }

        public void EnterDragMode()
        {
            collider.enabled = false;
        }

        public void ExitDragMode()
        {
            collider.enabled = true;
        }
    }
}