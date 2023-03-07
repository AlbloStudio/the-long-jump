using Assets.Scripts.managers;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static Enum;

namespace Assets.Scripts.being
{
    public class CharacterMover : MonoBehaviour
    {
        [Tooltip("Amount of time that the player can still jump after leaving ground")]
        [SerializeField] private float _coyoteTime = .1f;

        [Tooltip("player speed")]
        [SerializeField] private float _runSpeed = 40f;

        [Tooltip("Circle that determines if we are grounded or not")]
        [SerializeField] private float _groundCheckRadius = 1f;

        [Tooltip("Amount of force added when the player jumps")]
        [SerializeField] private float _jumpForce = 600f;

        [Tooltip("Amount of time that the player has to wait before jumping again")]
        [SerializeField] private float _jumpTime = .1f;

        [Tooltip("A mask determining what is ground to the character")]
        [SerializeField] private LayerMask _whatIsGround = new();

        [Tooltip(" A position marking where to check if the player is grounded")]
        [SerializeField] private Transform _groundCheck;

        [SerializeField] private Renderer _trail;

        [Tooltip("The Physics Material to use when we are in the air")]
        [SerializeField] private PhysicsMaterial2D _airPhysicsMaterial;

        [Tooltip("The Physics Material to use when we are in the ground")]
        [SerializeField] private PhysicsMaterial2D _groundPhysicsMaterial;

        [Tooltip("Gravity when falliong down")]
        [SerializeField] private float _downwardsGravityScale = 9f;

        [Tooltip("Gravity when jumping up")]
        [SerializeField] private float _upwardsGravityScale = 3f;

        [Tooltip("how much in units you have to fall to die")]
        [SerializeField] private float _fallDeath = 5f;

        public bool IsGrounded { get; private set; } = false;
        public StateMachine<CharState> state = new(CharState.Airing);
        public string currentStateName = CharState.Airing.ToString();

        private Rigidbody2D _body;
        private CharacterEffects _effects;
        private CharacterAnimator _animator;
        private CharacterTeleport _charTeleporter;

        private float _originalGravityScale;
        private float _coyoteCounter;
        private float _jumpCounter;
        private float _fallDamageFirstPosition;
        private bool _isDead;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _effects = GetComponent<CharacterEffects>();
            _animator = GetComponent<CharacterAnimator>();
            _charTeleporter = GetComponent<CharacterTeleport>();
            _originalGravityScale = _body.gravityScale;

            _coyoteCounter = _coyoteTime;
            _jumpCounter = _jumpTime;
            _fallDamageFirstPosition = transform.position.y;

            state.StateChanged.AddListener(StateChanged);
        }

        private void FixedUpdate()
        {
            currentStateName = state.CurrentState.ToString();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.transform.position, _groundCheckRadius, _whatIsGround);
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
                _fallDamageFirstPosition = Mathf.Max(transform.position.y, _fallDamageFirstPosition);
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
            else
            {
                _effects.ActivateRun(_body.velocity.x);
            }
        }

        private void WhileImpulsing(bool isGrounded)
        {
            if (isGrounded)
            {
                state.ChangeState(CharState.Grounded);
            }
            else
            {
                _fallDamageFirstPosition = Mathf.Max(transform.position.y, _fallDamageFirstPosition);
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

            if (!isGrounded)
            {

                _body.gravityScale = _body.velocity.y > 0 ? _upwardsGravityScale : _downwardsGravityScale;
                _jumpCounter -= Time.deltaTime;
                _fallDamageFirstPosition = Mathf.Max(transform.position.y, _fallDamageFirstPosition);
            }
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
                    OnStoppedGrounded();
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

        private void OnStoppedGrounded()
        {
            _effects.ActivateRun(0);
        }

        private void OnStoppedCoyoting()
        {
            _body.gravityScale = _originalGravityScale;
            _coyoteCounter = _coyoteTime;
        }

        private void OnStoppedJumping()
        {
            _jumpCounter = _jumpTime;
        }

        private void OnAir()
        {
            _body.sharedMaterial = _airPhysicsMaterial;
            _fallDamageFirstPosition = transform.position.y;
        }

        private void OnCoyote()
        {
            _body.velocity = new(_body.velocity.x, 0f);
            _originalGravityScale = _body.gravityScale;
            _body.gravityScale = 0;
        }

        private void OnGround()
        {
            _body.gravityScale = _downwardsGravityScale;

            _body.sharedMaterial = _groundPhysicsMaterial;
            float fallDistance = _fallDamageFirstPosition - transform.position.y;
            if (fallDistance >= _fallDeath)
            {
                _ = StartCoroutine(KillByFallDamage());
            }

            _effects.BurstFall();
        }

        private void OnImpulse()
        {
            _body.gravityScale = _downwardsGravityScale;

            _body.sharedMaterial = _airPhysicsMaterial;
            _fallDamageFirstPosition = transform.position.y;

            _animator.TriggerJump();
        }

        private void OnJump()
        {
            _body.sharedMaterial = _airPhysicsMaterial;
            _fallDamageFirstPosition = transform.position.y;

            _effects.BurstJump();
            _animator.TriggerJump();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
        }

        private IEnumerator KillByFallDamage()
        {
            _animator.TriggerDeath();
            _isDead = true;

            yield return new WaitForSeconds(1);

            Kill();
            _isDead = false;
        }

        public void Move(float move)
        {
            if (!CanMove())
            {
                return;
            }

            float speed = move * 10f * _runSpeed;

            _body.velocity = new Vector2(speed, _body.velocity.y);
        }

        public bool CanJump()
        {
            return !_isDead && state.IsInState(CharState.Grounded, CharState.Coyoting);
        }

        public bool CanMove()
        {
            return !_isDead && !state.IsInState(CharState.Impulsing);
        }

        public void Jump(float force = 1, Vector2? direction = null)
        {
            if (!CanJump())
            {
                return;
            }

            if (direction == null)
            {
                direction = Vector2.up;
            }

            _body.AddForce(_jumpForce * force * (Vector2)direction, ForceMode2D.Force);

            state.ChangeState(CharState.Jumping);
        }

        public void Impulse(float force, Vector2 direction)
        {
            _body.velocity = Vector2.zero;

            _body.AddForce(force * direction, ForceMode2D.Force);

            state.ChangeState(CharState.Impulsing);
        }

        public void Kill()
        {
            Teleport(CheckpointManager.Instance.ActiveCheckpoint.transform.position);
        }

        public void Teleport(Vector2 position, UnityAction onTeleported = null, bool hideWhileTeleporting = true, float time = 1)
        {
            _fallDamageFirstPosition = transform.position.y;
            _charTeleporter.Teleport(position, this, onTeleported, hideWhileTeleporting, time);
        }
    }
}

