using Assets.Scripts.managers;
using UnityEngine;

namespace Assets.Scripts.item
{
    public class Jumper : MonoBehaviour
    {
        private Collider2D _collider;

        private void OnEnable()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void OnMouseDown()
        {
            DragManager.Instance.StartDraggingMode(this);
        }

        private void OnMouseUp()
        {
            DragManager.Instance.FinishDraggingMode();
        }

        public void StartDraggingMode()
        {
            _collider.enabled = false;
        }

        public void FinishDraggingMode()
        {
            _collider.enabled = true;
        }
    }
}