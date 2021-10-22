using System;
using UnityEngine;

namespace Assets.Scripts.item
{
    public class Item : MonoBehaviour
    {
        [Tooltip("The main Camera")]
        [SerializeField] private Camera mainCamera;

        [Tooltip("Object in which this item will transform to")]
        [SerializeField] private Jumper jumperTemplate;

        private bool _isDragging;

        private SpriteRenderer _spriteRenderer;
        private Jumper _jumper;

        private void OnEnable()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (_isDragging && _jumper)
            {
                Drag();
            }
        }

        private void OnMouseDown()
        {
            InitDrag();
        }

        private void OnMouseUp()
        {
            SucceedDrag();
        }

        public bool IsDragging()
        {
            return _isDragging;
        }

        private void InitDrag()
        {
            _spriteRenderer.enabled = false;
            _isDragging = true;

            _jumper = Instantiate(jumperTemplate, transform.position, Quaternion.identity);
            _jumper.EnterDragMode();
        }

        private void SucceedDrag()
        {
            _isDragging = false;
            _jumper.ExitDragMode();
        }

        private void Drag()
        {
            _jumper.transform.position = mainCamera.ScreenToWorldPoint(
                new Vector3(
                    Input.mousePosition.x,
                    Input.mousePosition.y,
                    Math.Abs(mainCamera.transform.position.z)
                )
            );
        }

        public void FailDrag()
        {
            _isDragging = false;
            GameObject.Destroy(_jumper);
            _spriteRenderer.enabled = true;
        }
    }
}