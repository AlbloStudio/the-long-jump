using UnityEngine;

namespace Assets.Scripts.item
{
    public class SpringJumper : Jumper
    {
        private BoxCollider2D _boxCollider;

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
        private Vector2 _colliderBounds;
        private Vector2 _originalColliderSize;
        private Vector2 _originalColliderOffset;

        private new void Awake()
        {
            base.Awake();

            _animator = GetComponent<Animator>();

            _boxCollider = GetComponent<BoxCollider2D>();
            _originalColliderSize = _boxCollider.size;
            _originalColliderOffset = _boxCollider.offset;

            _colliderBounds = GetComponent<SpriteRenderer>().sprite.bounds.size;

        }

        private void Start()
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject == _charAction.gameObject)
            {
                _charAction.Impulse(_FORCE * jumpForce, direction);
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
                    SetColliderForClicking();
                    break;

                case PlanningMode.Playing:
                    _collider.isTrigger = false;
                    SetColliderForBouncing();
                    break;
                default:
                    break;
            }
        }

        private void SetColliderForClicking()
        {
            _boxCollider.size = _colliderBounds;
            _boxCollider.offset = new Vector2(0, 0);
        }

        private void SetColliderForBouncing()
        {
            _boxCollider.size = _originalColliderSize;
            _boxCollider.offset = _originalColliderOffset;
        }
    }
}