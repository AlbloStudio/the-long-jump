using Assets.Scripts.item;
using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.totem
{
    public class Totem : MonoBehaviour
    {
        [Tooltip("The ample camera to transition when player is here")]
        [SerializeField] private CinemachineVirtualCamera ampleCamera;

        private List<Item> _items;

        private void OnEnable()
        {
            _items = new List<Item>(GetComponentsInChildren<Item>(true));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsTriggeringWithPlayer(other))
            {
                StartItemMode();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsTriggeringWithPlayer(other))
            {
                FinishItemMode();
            }
        }

        private void StartItemMode()
        {
            SetItemsActive(true);
            ampleCamera.enabled = true;
        }

        private void FinishItemMode()
        {
            SetItemsActive(false);
            ampleCamera.enabled = false;

            FailCurrentDrag();
        }

        private static bool IsTriggeringWithPlayer(Component collided)
        {
            return collided.gameObject.layer == LayerMask.NameToLayer("Player");
        }

        private void SetItemsActive(bool active)
        {
            foreach (Item item in _items)
            {
                item.gameObject.SetActive(active);
            }
        }

        private void FailCurrentDrag()
        {
            foreach (Item item in _items.Where(item => item.IsDragging()))
            {
                item.FailDrag();
            }
        }
    }
}