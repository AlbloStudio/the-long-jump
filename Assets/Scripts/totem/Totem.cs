using Assets.Scripts.item;
using Assets.Scripts.managers;
using Assets.Scripts.utils;
using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.totem
{
    public class Totem : MonoBehaviour
    {
        [Tooltip("The safe area where the Objects can be placed into")]
        [SerializeField] private Collider2D safeArea;

        private List<Item> _items;
        private bool _isPlanning = false;
        private Item _activeItem;

        private void OnEnable()
        {
            _items = new List<Item>(GetComponentsInChildren<Item>(true));
        }

        private void Update()
        {
            if (_isPlanning)
            {
                HandleClickItemActivation();

                if (_activeItem)
                {
                    Drag();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsTriggeringWithPlayer(other))
            {
                EnterPlanning();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsTriggeringWithPlayer(other))
            {
                ExitPlanning();
            }
        }

        private bool IsTriggeringWithPlayer(Component collided)
        {
            return collided.gameObject.layer.Equals(LayerMask.NameToLayer("Player"));
        }

        private void EnterPlanning()
        {
            _isPlanning = true;

            GeneralData.Instance.ampleCameraCineMachine.enabled = true;

            foreach (Item item in _items)
            {
                item.EnterPlanningMode(safeArea);
            }
        }

        private void ExitPlanning()
        {
            _isPlanning = false;

            GeneralData.Instance.ampleCameraCineMachine.enabled = false;

            foreach (Item item in _items)
            {
                item.ExitPlanningMode();
            }
        }

        private void HandleClickItemActivation()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Item itemClicked = ItemClicked();

                if (itemClicked)
                {
                    _activeItem = itemClicked;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _activeItem = null;
            }
        }

        private Item ItemClicked()
        {
            Camera camera = GeneralData.Instance.ampleCamera;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, LayerMask.GetMask("Jumper"));

            return rayHit.transform ? rayHit.transform.GetComponent<Item>() : null;
        }

        private void Drag()
        {
            _activeItem.transform.position = GeneralData.Instance.mainCamera.ScreenToWorldPoint(
                new Vector3(
                    Input.mousePosition.x,
                    Input.mousePosition.y,
                    Math.Abs(GeneralData.Instance.mainCamera.transform.position.z)
                )
            );
        }
    }
}