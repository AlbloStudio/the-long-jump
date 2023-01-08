using Assets.Scripts.utils;
using UnityEngine;

namespace Assets.Scripts.being
{
    public class CharacterMover : MonoBehaviour
    {
        private const float _GROUNDED_RADIUS = .2f;

        [Tooltip("Amount of force added when the player jumps")]
        [SerializeField] private float jumpForce = 600f;

        [Tooltip("Amount of time that the player can still jump after leaving ground")]
        [SerializeField] private float coyoteTime = .1f;

        [Tooltip("A mask determining what is ground to the character")]
        [SerializeField] private LayerMask whatIsGround = new();

        [Tooltip(" A position marking where to check if the player is grounded")]
        [SerializeField] private Transform groundCheck;

        private float _coyoteCounter;

        private Rigidbody2D _body;

        public bool _isGrounded;
        private Vector2 _impulseVelocity = Vector2.zero;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            CheckIsGrounded();
            CountCoyoteTime();
        }

        private void CheckIsGrounded()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, _GROUNDED_RADIUS, whatIsGround);
            _isGrounded = colliders.Length > 0;

            if (_isGrounded)
            {
                _impulseVelocity = Vector2.zero;
            }
        }

        private void CountCoyoteTime()
        {
            bool isCoyoting = !_isGrounded && _coyoteCounter >= 0;
            if (isCoyoting)
            {
                _coyoteCounter -= Time.fixedDeltaTime;
            }

            bool isJumpFinished = _isGrounded && !Float.Equals(_coyoteCounter, coyoteTime);
            if (isJumpFinished)
            {
                _coyoteCounter = coyoteTime;
            }
        }

        public void Move(float move)
        {
            _body.velocity = new Vector2((move * 10f) + _impulseVelocity.x, _body.velocity.y);
        }

        public void Teleport(Vector2 position)
        {
            _body.velocity = Vector2.zero;
            transform.position = position;
        }

        public bool CanJump()
        {
            return _coyoteCounter > 0;
        }

        public void Jump(float force = 1)
        {
            _isGrounded = false;

            _body.AddForce(jumpForce * force * Vector2.up);
        }

        public void Impulse(float force, Vector2 direction)
        {
            _isGrounded = false;
            _impulseVelocity = direction * (force / _body.mass * Time.fixedDeltaTime);

            _body.AddForce(force * direction);
        }

        public bool IsGrounded()
        {
            return _isGrounded;
        }
    }
}