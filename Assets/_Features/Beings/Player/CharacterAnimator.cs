using UnityEngine;
using static Enum;

namespace Assets.Scripts.being
{
    public class CharacterAnimator : MonoBehaviour
    {
        private const float _FLIP_MARGN = .00001f;

        private Animator _animator;
        private Rigidbody2D _body;
        private CharacterMover _controller;
        private Facing _facing = Facing.Right;

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
            transform.localScale = new(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        public void ManageAnimations()
        {
            _animator.SetBool(CharAnimationNames.Grounded, _controller.state.IsInState(CharState.Grounded));
            _animator.SetFloat(CharAnimationNames.VelocityX, Mathf.Abs(_body.velocity.x));
            _animator.SetFloat(CharAnimationNames.VelocityY, _body.velocity.y);

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