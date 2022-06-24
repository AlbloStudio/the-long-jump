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
        [SerializeField] private LayerMask whatIsGround = new LayerMask();

        [Tooltip(" A position marking where to check if the player is grounded")]
        [SerializeField] private Transform groundCheck;

        private float _coyoteCounter;

        private Animator _animator;
        private Rigidbody2D _body;

        public bool _isGrounded;

        private void OnEnable()
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
        }

        private void CountCoyoteTime()
        {
            bool isCoyoting = !_isGrounded && _coyoteCounter >= 0;
            if (isCoyoting)
            {
                _coyoteCounter -= Time.fixedDeltaTime;
            }

            bool isJumpFinished = _isGrounded && Float.Equals(_coyoteCounter, coyoteTime);
            if (isJumpFinished)
            {
                _coyoteCounter = coyoteTime;
            }
        }

        public void Move(float move)
        {
            _body.velocity = new Vector2(move * 10f, _body.velocity.y);
        }

        public void Jump()
        {
            bool canJump = _coyoteCounter > 0;

            if (canJump)
            {
                _isGrounded = false;

                _body.velocity = new Vector2(_body.velocity.x, 0);
                _body.AddForce(new Vector2(0f, jumpForce));
            }
        }

        public bool IsGrounded()
        {
            return _isGrounded;
        }
    }
}