using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    const float GROUNDED_RADIUS = .2f;

    enum Facing
    {
        Right,
        Left
    }

    [Tooltip("Amount of force added when the player jumps")]
    [SerializeField] private float JumpForce = 600f;

    [Tooltip("Amount of time that the player can still jump after leaving ground")]
    [SerializeField] private float CoyoteTime = .1f;

    [Tooltip("A mask determining what is ground to the character")]
    [SerializeField] private LayerMask WhatIsGround = new LayerMask();

    [Tooltip(" A position marking where to check if the player is grounded")]
    [SerializeField] private Transform GroundCheck;

    private bool isGrounded;
    private float coyoteCounter;
    private Facing facing = Facing.Right;

    private Animator animator;
    private Rigidbody2D body;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        CheckIsGrounded();
        CountCoyoteTime();
        ManageAnimations();
    }

    public void Move(float move)
    {
        body.velocity = new Vector2(move * 10f, body.velocity.y);
    }

    public void Jump()
    {
        bool canJump = coyoteCounter > 0;

        if (canJump)
        {
            isGrounded = false;

            body.velocity = new Vector2(body.velocity.x, 0);
            body.AddForce(new Vector2(0f, JumpForce));
        }
    }

    private void CheckIsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.transform.position, GROUNDED_RADIUS, WhatIsGround);
        isGrounded = colliders.Length > 0;
    }

    private void CountCoyoteTime()
    {
        bool isCoyoting = !isGrounded && coyoteCounter >= 0;
        if (isCoyoting)
        {
            coyoteCounter -= Time.fixedDeltaTime;
        }

        bool isJumpFinished = isGrounded && coyoteCounter != CoyoteTime;
        if (isJumpFinished)
        {
            coyoteCounter = CoyoteTime;
        }
    }

    public void ManageAnimations()
    {
        animator.SetBool("grounded", isGrounded);
        animator.SetFloat("velocityX", Mathf.Abs(body.velocity.x));
        animator.SetFloat("velocityY", body.velocity.y);

        bool isTurningLeft = body.velocity.x < 0 && facing == Facing.Right;
        bool isTurningRight = body.velocity.x > 0 && facing == Facing.Left;

        if (isTurningRight || isTurningLeft)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facing = facing == Facing.Left ? Facing.Right : Facing.Left;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}