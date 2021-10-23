using Assets.Scripts.item;
using Assets.Scripts.managers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.totem
{
    public class Totem : MonoBehaviour
    {
        private List<Item> _items;

        private void OnEnable()
        {
            _items = new List<Item>(GetComponentsInChildren<Item>(true));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsTriggeringWithPlayer(other))
            {
                SetActiveItems(true);
                DragManager.Instance.StartPlanningMode();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsTriggeringWithPlayer(other))
            {
                SetActiveItems(false);
                DragManager.Instance.FinishPlanningMode();
            }
        }

        private static bool IsTriggeringWithPlayer(Component collided)
        {
            return collided.gameObject.layer.Equals(LayerMask.NameToLayer("Player"));
        }

        private void SetActiveItems(bool active)
        {
            foreach (Item item in _items)
            {
                item.SetActive(active);
            }
        }
    }
}