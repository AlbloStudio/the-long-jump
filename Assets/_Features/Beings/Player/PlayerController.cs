using UnityEngine;
using static Enum;

namespace Assets.Scripts.being
{
    public class PlayerController : MonoBehaviour
    {
        private CharacterMover _controller;

        private bool _shouldJump = false;
        private float _horizontalMovement = 0f;
        private float _xInput = 0;

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

            if (Input.GetButtonUp("Reset"))
            {
                _controller.Kill(DeathType.Reset, 0.2f);
            }

            _xInput = Input.GetAxis("Horizontal");
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
    }
}