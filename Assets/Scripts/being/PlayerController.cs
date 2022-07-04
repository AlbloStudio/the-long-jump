using UnityEngine;

namespace Assets.Scripts.being
{
    public class PlayerController : MonoBehaviour
    {
        [Tooltip("player speed")]
        [SerializeField] private float runSpeed = 40f;

        private bool _shouldJump = false;
        private float _horizontalMovement = 0f;

        private CharacterMover _controller;

        private void OnEnable()
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

            _horizontalMovement = Input.GetAxis("Horizontal") * runSpeed * Time.fixedDeltaTime;
        }

        private void FixedUpdate()
        {
            _controller.Move(_horizontalMovement);

            if (_shouldJump && _controller.CanJump())
            {
                _controller.Jump();
            }

            _shouldJump = false;
        }
    }
}