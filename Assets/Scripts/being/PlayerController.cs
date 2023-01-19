using UnityEngine;

namespace Assets.Scripts.being
{
    public class PlayerController : MonoBehaviour
    {
        private bool _shouldJump = false;
        private float _horizontalMovement = 0f;
        private float _xInput = 0;

        private CharacterMover _controller;

        private void Awake()
        {
            _controller = GetComponent<CharacterMover>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                _shouldJump = true;
            }

            if (Input.GetButtonUp("Jump"))
            {
                _shouldJump = false;
            }

            _xInput = Input.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            TryMove();
            TryJump();
        }

        private void TryMove()
        {
            if (_controller.CanMove())
            {
                _horizontalMovement = _xInput * Time.fixedDeltaTime;
                _controller.Move(_horizontalMovement);
            }
        }

        private void TryJump()
        {
            if (_shouldJump && _controller.CanJump())
            {
                _controller.Jump();
                _shouldJump = false;
            }
        }
    }
}