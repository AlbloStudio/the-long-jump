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
        }

        private void FixedUpdate()
        {
            _controller.Move(_horizontalMovement);
            _horizontalMovement = Input.GetAxis("Horizontal") * runSpeed * Time.fixedDeltaTime;

            if (_shouldJump && _controller.CanJump())
            {
                _controller.Jump();
            }

            _shouldJump = false;
        }
    }
}