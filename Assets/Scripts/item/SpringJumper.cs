using UnityEngine;

namespace Assets.Scripts.item
{
    public class SpringJumper : Jumper
    {
        public static class AnimatorNames
        {
            public static readonly int Activate = Animator.StringToHash("activate");
        }

        private const float _FORCE = 600f;

        [Tooltip("How strong is the spring")]
        [SerializeField] private float jumpForce = 1.5f;

        [Tooltip("Direction of the spring")]
        [SerializeField] private Vector2 direction = Vector2.up;

        private Animator _animator;

        private new void OnEnable()
        {
            base.OnEnable();

            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            _animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject == _controller.gameObject)
            {
                _controller.Impulse(_FORCE * jumpForce, direction);
                _animator.SetTrigger(AnimatorNames.Activate);
            }
        }

        protected override void OnChangePlanningMode(PlanningMode newMode)
        {
            switch (newMode)
            {
                case PlanningMode.Planning:
                case PlanningMode.Waiting:
                    _collider.isTrigger = true;
                    break;

                case PlanningMode.Playing:
                    _collider.isTrigger = false;
                    break;
            }
        }
    }
}