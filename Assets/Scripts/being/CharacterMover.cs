using UnityEngine;

namespace Assets.Scripts.being
{
    public class CharacterMover : MonoBehaviour
    {
        public enum CharState
        {
            Grounded,
            Coyoting,
            Airing,
            Jumping,
            Impulsing,
        }

        [Tooltip("Amount of time that the player can still jump after leaving ground")]
        [SerializeField] private float coyoteTime = .1f;

        [Tooltip("player speed")]
        [SerializeField] private float runSpeed = 40f;

        [Tooltip("Circle that determines if we are grounded or not")]
        [SerializeField] private float groundCheckRadius = 1f;

        [Tooltip("Amount of force added when the player jumps")]
        [SerializeField] private float jumpForce = 600f;

        [Tooltip("Amount of time that the player has to wait before jumping again")]
        [SerializeField] private float jumpTime = .1f;

        [Tooltip("A mask determining what is ground to the character")]
        [SerializeField] private LayerMask whatIsGround = new();

        [Tooltip(" A position marking where to check if the player is grounded")]
        [SerializeField] private Transform groundCheck;

        [Tooltip("The Physics Material to use when we are in the air")]
        [SerializeField] private PhysicsMaterial2D airPhysicsMaterial;

        [Tooltip("The Physics Material to use when we are in the ground")]
        [SerializeField] private PhysicsMaterial2D groundPhysicsMaterial;

        [Tooltip("Gravity when falliong down")]
        [SerializeField] private float _downwardsGravityScale = 9f;

        [Tooltip("Gravity when jumping up")]
        [SerializeField] private float _upwardsGravityScale = 3f;

        public bool IsGrounded { get; private set; } = false;
        public StateMachine<CharState> state = new(CharState.Airing);
        public string currentStateName = CharState.Airing.ToString();

        private Rigidbody2D _body;
        private float _originalGravityScale;

        private float _coyoteCounter;
        private float _jumpCounter;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _coyoteCounter = coyoteTime;

            _originalGravityScale = _body.gravityScale;

            state.StateChanged.AddListener(StateChanged);
        }

        private void FixedUpdate()
        {
            currentStateName = state.CurrentState.ToString();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, groundCheckRadius, whatIsGround);
            bool isGrounded = colliders.Length > 0;

            switch (state.CurrentState)
            {
                case CharState.Airing:
                    WhileAiring(isGrounded);
                    break;

                case CharState.Coyoting:
                    WhileCoyoting(isGrounded);
                    break;

                case CharState.Grounded:
                    WhileGrounded(isGrounded);
                    break;

                case CharState.Impulsing:
                    WhileImpulsing(isGrounded);
                    break;

                case CharState.Jumping:
                    WhileJumping(isGrounded);
                    break;

                default:
                    break;
            }
        }

        private void WhileAiring(bool isGrounded)
        {
            if (isGrounded)
            {
                state.ChangeState(CharState.Grounded);
            }
            else
            {
                _body.gravityScale = _body.velocity.y > 0 ? _upwardsGravityScale : _downwardsGravityScale;
            }
        }

        private void WhileCoyoting(bool isGrounded)
        {
            if (isGrounded)
            {
                state.ChangeState(CharState.Grounded);
            }
            else
            {

                _coyoteCounter -= Time.fixedDeltaTime;

                if (_coyoteCounter <= 0f)
                {
                    state.ChangeState(CharState.Airing);
                }
            }
        }

        private void WhileGrounded(bool isGrounded)
        {
            if (!isGrounded)
            {
                state.ChangeState(CharState.Coyoting);
            }
        }

        private void WhileImpulsing(bool isGrounded)
        {
            if (isGrounded)
            {
                state.ChangeState(CharState.Grounded);
            }
        }

        private void WhileJumping(bool isGrounded)
        {

            if (_jumpCounter <= 0)
            {
                if (isGrounded)
                {
                    state.ChangeState(CharState.Grounded);
                }
            }

            _body.gravityScale = _body.velocity.y > 0 ? _upwardsGravityScale : _downwardsGravityScale;
            _jumpCounter -= Time.deltaTime;
        }

        private void StateChanged(CharState newState, CharState previousState)
        {
            if (previousState == CharState.Coyoting)
            {
                OnStoppedCoyoting();
            }

            switch (previousState)
            {
                case CharState.Grounded:
                    break;

                case CharState.Coyoting:
                    OnStoppedCoyoting();
                    break;

                case CharState.Airing:
                    break;

                case CharState.Jumping:
                    OnStoppedJumping();
                    break;

                case CharState.Impulsing:
                    break;
                default:
                    break;
            }

            switch (newState)
            {
                case CharState.Airing:
                    OnAir();
                    break;

                case CharState.Coyoting:
                    OnCoyote();
                    break;

                case CharState.Grounded:
                    OnGround();
                    break;

                case CharState.Impulsing:
                    OnImpulse();
                    break;

                case CharState.Jumping:
                    OnJump();
                    break;

                default:
                    break;
            }
        }

        private void OnStoppedCoyoting()
        {
            _body.gravityScale = _originalGravityScale;
            _coyoteCounter = coyoteTime;
        }

        private void OnStoppedJumping()
        {
            _jumpCounter = jumpTime;
        }

        private void OnAir()
        {
            _body.sharedMaterial = airPhysicsMaterial;

        }

        private void OnCoyote()
        {
            _body.velocity = new(_body.velocity.x, 0f);
            _originalGravityScale = _body.gravityScale;
            _body.gravityScale = 0;
        }

        private void OnGround()
        {
            _body.sharedMaterial = groundPhysicsMaterial;

        }

        private void OnImpulse()
        {
            _body.sharedMaterial = airPhysicsMaterial;

        }

        private void OnJump()
        {
            _body.sharedMaterial = airPhysicsMaterial;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        public void Move(float move)
        {
            _body.velocity = new Vector2(move * 10f * runSpeed, _body.velocity.y);
        }

        public void Teleport(Vector2 position)
        {
            _body.velocity = Vector2.zero;
            transform.position = position;
        }

        public bool CanJump()
        {
            return state.IsInState(CharState.Grounded, CharState.Coyoting);
        }

        public bool CanMove()
        {
            return !state.IsInState(CharState.Impulsing);
        }

        public void Jump(float force = 1, Vector2? direction = null)
        {
            if (direction == null)
            {
                direction = Vector2.up;
            }

            _body.AddForce(jumpForce * force * (Vector2)direction, ForceMode2D.Force);

            state.ChangeState(CharState.Jumping);
        }

        public void Impulse(float force, Vector2 direction)
        {
            _body.velocity = Vector2.zero;
            Jump(force / jumpForce, direction);

            state.ChangeState(CharState.Impulsing);
        }
    }
}

