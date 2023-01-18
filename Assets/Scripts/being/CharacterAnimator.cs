using UnityEngine;

namespace Assets.Scripts.being
{
    public static class AnimatorNames
    {
        public static readonly int Grounded = Animator.StringToHash("grounded");
        public static readonly int VelocityX = Animator.StringToHash("velocityX");
        public static readonly int VelocityY = Animator.StringToHash("velocityY");
    }

    public class CharacterAnimator : MonoBehaviour
    {
        private const float _FLIP_MARGN = .00001f;

        private enum Facing
        {
            Right,
            Left
        }

        private Facing _facing = Facing.Right;

        private Animator _animator;
        private Rigidbody2D _body;
        private CharacterMover _controller;

        private void Awake()
        {
            _controller = GetComponent<CharacterMover>();
            _animator = GetComponent<Animator>();
            _body = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            ManageAnimations();
        }

        private void Flip()
        {
            _facing = _facing is Facing.Left ? Facing.Right : Facing.Left;

            Transform localTransform = transform;
            Vector3 localScale = localTransform.localScale;

            localScale.x = -localScale.x;
            localTransform.localScale = localScale;
        }

        public void ManageAnimations()
        {
            _animator.SetBool(AnimatorNames.Grounded, _controller.IsGrounded);
            _animator.SetFloat(AnimatorNames.VelocityX, Mathf.Abs(_body.velocity.x));
            _animator.SetFloat(AnimatorNames.VelocityY, _body.velocity.y);

            Vector2 velocity = _body.velocity;
            bool isTurningLeft = velocity.x < -_FLIP_MARGN && _facing is Facing.Right;
            bool isTurningRight = velocity.x > _FLIP_MARGN && _facing is Facing.Left;

            if (isTurningRight || isTurningLeft)
            {
                Flip();
            }
        }
    }
}