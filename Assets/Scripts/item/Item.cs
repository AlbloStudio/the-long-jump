using Assets.Scripts.managers;
using Assets.Scripts.utils;
using System;
using UnityEngine;

namespace Assets.Scripts.item
{
    public class Item : MonoBehaviour
    {
        [Tooltip("Object in which this item will transform to")]
        [SerializeField] private GameObject jumperTemplate;

        private GameObject _jumperInstance;
        private Vector2 _originalPosition;

        private Collider2D _safeArea;

        private void Awake()
        {
            _originalPosition = transform.position;
        }

        private bool IsInSafeArea()
        {
            return _safeArea.bounds.Contains(transform.position);
        }

        public void EnterPlanningMode(Collider2D safeArea)
        {
            _safeArea = safeArea;

            gameObject.SetActive(true);

            if (_jumperInstance)
            {
                Destroy(_jumperInstance.gameObject);
            }
        }

        public void ExitPlanningMode()
        {
            gameObject.SetActive(false);

            if (IsInSafeArea())
            {
                _jumperInstance = Instantiate(jumperTemplate, transform.position, Quaternion.identity, GeneralData.Instance.jumpersFolder.transform);
            }
            else
            {
                transform.position = _originalPosition;
            }
        }
    }
}