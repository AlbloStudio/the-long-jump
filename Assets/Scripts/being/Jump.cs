using Assets.Scripts.utils;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 10f)] private float jumpHeight = 10f;
    [SerializeField, Range(0, 5)] private int maxAirJumps = 0;
    [SerializeField, Range(0f, 5)] private float downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5)] private float upwardMovementMultiplier = 1.7f;
    [SerializeField] private float coyoteTime = .1f;

    private Rigidbody2D _body;
    private Ground _ground;
    private Vector2 _velocity;
    private int _jumpPhase;
    private float _defaultGravityScale;
    private float _coyoteCounter;
    private bool _desiredJump;
    private bool _onGround;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();

        _defaultGravityScale = 1f;
    }

    private void Update()
    {
        _desiredJump |= input.RetrieveJumpInput();
    }

    private void FixedUpdate()
    {
        CountCoyoteTime();

        _onGround = _ground.OnGround;
        _velocity = _body.velocity;

        if (_onGround)
        {
            _jumpPhase = 0;
        }

        if (_desiredJump)
        {
            _desiredJump = false;
            JumpAction();
        }

        if (!IsGrounded())
        {

            if (_body.velocity.y > 0)
            {
                _body.gravityScale = upwardMovementMultiplier;
            }
            else if (_body.velocity.y < 0)
            {
                _body.gravityScale = downwardMovementMultiplier;
            }
            else if (_body.velocity.y == 0)
            {
                _body.gravityScale = _defaultGravityScale;
            }
        }
        else
        {
            _body.gravityScale = _defaultGravityScale;

        }

        _body.velocity = _velocity;

    }

    private void CountCoyoteTime()
    {
        bool isCoyoting = !_onGround && _coyoteCounter >= 0;
        if (isCoyoting)
        {
            _coyoteCounter -= Time.fixedDeltaTime;
        }

        bool isJumpFinished = _onGround && !Float.Equals(_coyoteCounter, coyoteTime);
        if (isJumpFinished)
        {
            _coyoteCounter = coyoteTime;
        }
    }

    private void JumpAction()
    {
        if (IsGrounded() || _jumpPhase < maxAirJumps)
        {
            _jumpPhase += 1;

            float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            _velocity.y = jumpSpeed + GetComponent<Action>()._impulseVelocity.y;
        }
    }

    private bool IsGrounded()
    {
        return _onGround || _coyoteCounter > 0;
    }
}
