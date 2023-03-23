using Assets.Scripts.totem;
using UnityEngine;
using static Enum;

namespace Assets.Scripts.being
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _timeToResetObjects = 1.5f;

        private CharacterMover _controller;

        private Totem _totem;
        private bool _shouldJump = false;
        private float _horizontalMovement = 0f;
        private float _xInput = 0;
        private float _resetObjectsCount = 0f;

        private void Awake()
        {
            _controller = GetComponent<CharacterMover>();
        }

        private void Update()
        {
            HandleJump();
            HandlePlayerReset();
            HandleResetObjects();

            _xInput = Input.GetAxis("Horizontal");
        }

        private void HandleJump()
        {
            if (Input.GetButtonDown("Jump"))
            {
                _shouldJump = true;
            }

            if (Input.GetButtonUp("Jump"))
            {
                _shouldJump = false;
            }
        }

        private void HandlePlayerReset()
        {
            if (Input.GetButtonUp("Reset"))
            {
                bool isNotResettingObjects = _resetObjectsCount <= 0.2f;
                if (isNotResettingObjects)
                {
                    _controller.Kill(DeathType.Reset, 0.2f);
                }

                _resetObjectsCount = 0;
            }
        }

        private void HandleResetObjects()
        {
            if (Input.GetButton("Reset"))
            {
                bool didNotResetYet = _resetObjectsCount <= _timeToResetObjects;
                if (didNotResetYet)
                {
                    _resetObjectsCount += Time.deltaTime;

                    bool isTimeToReset = _resetObjectsCount > _timeToResetObjects;
                    if (isTimeToReset && _totem != null)
                    {
                        _totem.ResetObjects();
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            Move();
            Jump();
        }

        private void Move()
        {

            _horizontalMovement = _xInput * Time.fixedDeltaTime;
            _controller.Move(_horizontalMovement);
        }

        private void Jump()
        {
            if (_shouldJump)
            {
                _controller.Jump();
                _shouldJump = false;
            }
        }

        public void SetTotem(Totem totem)
        {
            _totem = totem;
        }
    }
}