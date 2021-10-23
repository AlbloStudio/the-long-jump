using Assets.Scripts.utils;
using UnityEngine;

namespace Assets.Scripts.being
{
    public static class AnimatorNames
    {
        public static readonly int Grounded = Animator.StringToHash("grounded");
        public static readonly int VelocityX = Animator.StringToHash("velocityX");
        public static readonly int VelocityY = Animator.StringToHash("velocityY");
    }

    public class CharacterController2D : MonoBehaviour
    {
        private const float GroundedRadius = .2f;

        private enum Facing
        {
            Right,
            Left
        }

        [Tooltip("Amount of force added when the player jumps")]
        [SerializeField] private float jumpForce = 600f;

        [Tooltip("Amount of time that the player can still jump after leaving ground")]
        [SerializeField] private float coyoteTime = .1f;

        [Tooltip("A mask determining what is ground to the character")]
        [SerializeField] private LayerMask whatIsGround = new LayerMask();

        [Tooltip(" A position marking where to check if the player is grounded")]
        [SerializeField] private Transform groundCheck;

        private bool _isGrounded;
        private float _coyoteCounter;
        private Facing _facing = Facing.Right;

        private Animator _animator;
        private Rigidbody2D _body;

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
            _body = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            CheckIsGrounded();
            CountCoyoteTime();
            ManageAnimations();
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

        private void CheckIsGrounded()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, GroundedRadius, whatIsGround);
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

        public void ManageAnimations()
        {
            _animator.SetBool(AnimatorNames.Grounded, _isGrounded);
            _animator.SetFloat(AnimatorNames.VelocityX, Mathf.Abs(_body.velocity.x));
            _animator.SetFloat(AnimatorNames.VelocityY, _body.velocity.y);

            Vector2 velocity = _body.velocity;
            bool isTurningLeft = velocity.x < 0 && _facing is Facing.Right;
            bool isTurningRight = velocity.x > 0 && _facing is Facing.Left;

            if (isTurningRight || isTurningLeft)
            {
                Flip();
            }
        }

        private void Flip()
        {
            _facing = _facing is Facing.Left ? Facing.Right : Facing.Left;

            Transform localTransform = transform;
            Vector3 localScale = localTransform.localScale;

            localScale.x = -localScale.x;
            localTransform.localScale = localScale;
        }
    }
}