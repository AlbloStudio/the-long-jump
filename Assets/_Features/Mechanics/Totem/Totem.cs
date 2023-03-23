using Assets.Scripts.being;
using Assets.Scripts.item;
using Assets.Scripts.managers;
using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.totem
{
    public class Totem : MonoBehaviour
    {
        [Tooltip("The safe area where the Objects can be placed into")]
        [SerializeField] private Collider2D _safeArea;

        [Tooltip("The camera that will point when planning mode")]
        [SerializeField] private CinemachineVirtualCamera _planningCamera;

        [Tooltip("Items that work with this totem")]
        [SerializeField] private List<Item> _items;

        private MeshRenderer _safeAreaRenderer;
        private AudioSource _audioSource;

        private bool _isPlanning = false;
        private Item _activeItem;
        private Vector3 _clickOffset = Vector2.zero;
        private PlayerController _playerController;

        private Camera _mainCamera;

        private void Awake()
        {
            _safeAreaRenderer = _safeArea.GetComponent<MeshRenderer>();
            _safeAreaRenderer.enabled = false;

            _audioSource = GetComponent<AudioSource>();

            _playerController = GeneralData.Instance.Player.GetComponent<PlayerController>();

            _mainCamera = Camera.main;
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
                _audioSource.Play();
                _playerController.SetTotem(this);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsTriggeringWithPlayer(other))
            {
                ExitPlanning();
                _audioSource.Stop();
                _playerController.SetTotem(null);
            }
        }

        private bool IsTriggeringWithPlayer(Component collided)
        {
            return collided.gameObject.layer.Equals(LayerMask.NameToLayer("Player"));
        }

        private void EnterPlanning()
        {
            _isPlanning = true;

            _safeAreaRenderer.enabled = true;

            _planningCamera.gameObject.SetActive(true);
            foreach (Item item in _items)
            {
                item.EnterPlanningMode(_safeArea);
            }
        }

        private void ExitPlanning()
        {
            _isPlanning = false;
            _activeItem = null;

            _safeAreaRenderer.enabled = false;

            _planningCamera.gameObject.SetActive(false);
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
                    _clickOffset = GetClickPos() - itemClicked.transform.position;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _activeItem = null;
            }
        }

        private Item ItemClicked()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, LayerMask.GetMask("Jumper", "PlatformJumper"));

            Item itemFound = rayHit.transform ? rayHit.transform.GetComponent<Item>() : null;

            return _items.Contains(itemFound) && !itemFound.IsStatic ? itemFound : null;
        }

        private void Drag()
        {
            Vector3 mousePos = GetClickPos() - _clickOffset;

            _activeItem.transform.position = new(mousePos.x, mousePos.y, _activeItem.transform.position.z);
        }

        private Vector3 GetClickPos()
        {
            return _mainCamera.ScreenToWorldPoint(
                            new Vector3(
                                Input.mousePosition.x,
                                Input.mousePosition.y,
                                Mathf.Abs(_mainCamera.transform.position.z)
                            )
                        );
        }

        public void ResetObjects()
        {
            foreach (Item item in _items)
            {
                item.ResetPosition();
            }
        }
    }
}