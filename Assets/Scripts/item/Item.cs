using Assets.Scripts.managers;
using Assets.Scripts.utils;
using System;
using UnityEngine;

namespace Assets.Scripts.item
{
    public class Item : MonoBehaviour
    {
        private Vector2 _originalPosition;

        private Collider2D _safeArea;
        private Jumper _jumper;

        private void Awake()
        {
            _originalPosition = transform.position;
            _jumper = GetComponent<Jumper>();
        }

        private bool IsInSafeArea()
        {
            return _safeArea.bounds.Contains(transform.position);
        }

        public void EnterPlanningMode(Collider2D safeArea)
        {
            _safeArea = safeArea;

            gameObject.SetActive(true);

            _jumper.enabled = false;
        }

        public void ExitPlanningMode()
        {
            if (IsInSafeArea())
            {
                _jumper.enabled = true;
            }
            else
            {
                gameObject.SetActive(false);

                transform.position = _originalPosition;
            }
        }
    }
}