using Assets.Scripts.managers;
using System;
using UnityEngine;

namespace Assets.Scripts.item
{
    public class Item : MonoBehaviour
    {
        [Tooltip("Object in which this item will transform to")]
        [SerializeField] private Jumper jumperTemplate;

        private SpriteRenderer _spriteRenderer;
        private bool _isPlaced;

        private void OnEnable()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnMouseDown()
        {
            StartDragMode();
        }

        private void OnMouseUp()
        {
            FinishDragMode();
        }

        private void StartDragMode()
        {
            _spriteRenderer.enabled = false;

            Jumper jumper = Instantiate(jumperTemplate, transform.position, Quaternion.identity);
            DragManager.Instance.StartDraggingMode(jumper);
        }

        private void FinishDragMode()
        {
            _spriteRenderer.enabled = true;
            _isPlaced = true;
            gameObject.SetActive(false);

            DragManager.Instance.FinishDraggingMode();
        }

        public void SetActive(bool active)
        {
            if (!_isPlaced)
            {
                gameObject.SetActive(active);
            }
        }
    }
}