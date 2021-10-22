using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerPlatformerController : MonoBehaviour
    {
        [Tooltip("player speed")]
        [SerializeField] private float runSpeed = 40f;

        private bool _isJumping = false;
        private float _horizontalMovement = 0f;

        private CharacterController2D _controller;

        private void OnEnable()
        {
            _controller = GetComponent<CharacterController2D>();

        }

        private void Update()
        {

            if (Input.GetButtonDown("Jump"))
            {
                _isJumping = true;
            }

            if (Input.GetButtonUp("Jump"))
            {
                _isJumping = false;
            }

            _horizontalMovement = Input.GetAxis("Horizontal") * runSpeed * Time.fixedDeltaTime;
        }

        private void FixedUpdate()
        {
            _controller.Move(_horizontalMovement);
            if (_isJumping)
            {
                _controller.Jump();
            }

            _isJumping = false;
        }
    }
}
