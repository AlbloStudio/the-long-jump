using UnityEngine;

namespace Assets.Scripts.being
{
    [RequireComponent(typeof(Coyote))]
    public class CharacterMover : MonoBehaviour
    {
        [Tooltip("Circle that determines if we are grounded or not")]
        [SerializeField] private float groundCheckRadius = 1f;

        [Tooltip("Amount of force added when the player jumps")]
        [SerializeField] private float jumpForce = 600f;

        [Tooltip("A mask determining what is ground to the character")]
        [SerializeField] private LayerMask whatIsGround = new();

        [Tooltip(" A position marking where to check if the player is grounded")]
        [SerializeField] private Transform groundCheck;

        [Tooltip("The Physics Material to use when we are in the air")]
        [SerializeField] private PhysicsMaterial2D airPhysicsMaterial;

        [Tooltip("The Physics Material to use when we are in the ground")]
        [SerializeField] private PhysicsMaterial2D groundPhysicsMaterial;

        public bool IsGrounded { get; private set; } = false;

        private Rigidbody2D _body;
        private Coyote _coyote;

        private bool _wasGrounded;
        private Vector2 _impulseVelocity = Vector2.zero;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _coyote = GetComponent<Coyote>();
        }

        private void FixedUpdate()
        {
            CheckIsGrounded();
            _coyote.CountCoyoteTime(IsGrounded);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        private void CheckIsGrounded()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, groundCheckRadius, whatIsGround);
            IsGrounded = colliders.Length > 0;

            if (IsGrounded != _wasGrounded)
            {
                if (IsGrounded)
                {
                    Land();
                }
                else
                {
                    Air();
                }
            }

            _wasGrounded = IsGrounded;
        }

        public void Move(float move)
        {
            _body.velocity = new Vector2(move * 10f, _body.velocity.y);
        }

        public void Teleport(Vector2 position)
        {
            _body.velocity = Vector2.zero;
            transform.position = position;
        }

        public bool CanJump()
        {
            return IsGrounded || _coyote.IsCoyoting;
        }

        public bool CanMove()
        {
            return _impulseVelocity.Equals(Vector2.zero);
        }

        public void Jump(float force = 1, Vector2? direction = null)
        {
            if (direction == null)
            {
                direction = Vector2.up;
            }

            _coyote.EndCoyote();

            _body.AddForce(jumpForce * force * (Vector2)direction);
        }

        public void Impulse(float force, Vector2 direction)
        {
            _impulseVelocity = direction * (force / _body.mass * Time.fixedDeltaTime);
            _body.velocity = Vector2.zero;
            Jump(force / jumpForce, direction);
        }

        private void Air()
        {
            IsGrounded = false;
            _body.sharedMaterial = airPhysicsMaterial;
        }

        private void Land()
        {
            IsGrounded = true;
            _body.sharedMaterial = groundPhysicsMaterial;

            _impulseVelocity = Vector2.zero;
            _coyote.RestartCoyote();
        }
    }
}

