using UnityEngine;

namespace Assets.Scripts.being
{
    public class PlayerController : MonoBehaviour
    {
        [Tooltip("player speed")]
        [SerializeField] private float runSpeed = 40f;

        private bool _shouldJump = false;
        private float _horizontalMovement = 0f;
        private float xInput = 0;

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

            xInput = Input.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            Move();
            Jump();
        }

        private void Move()
        {
            _horizontalMovement = xInput * runSpeed * Time.fixedDeltaTime;
            _controller.Move(_horizontalMovement);
        }

        private void Jump()
        {
            if (_shouldJump && _controller.CanJump())
            {
                _controller.Jump();
                _shouldJump = false;
            }
        }
    }
}