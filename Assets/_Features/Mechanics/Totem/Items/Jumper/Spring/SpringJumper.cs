using System.Collections;
using UnityEngine;
using static Enum;

namespace Assets.Scripts.item
{
    public class SpringJumper : Jumper
    {
        private BoxCollider2D _boxCollider;

        private const float _FORCE = 600f;

        [Tooltip("How strong is the spring")]
        [SerializeField] private float _jumpForce = 1.5f;

        [Tooltip("Direction of the spring")]
        [SerializeField] private Vector2 _direction = Vector2.up;

        [Tooltip("The object to draw trajectory")]
        [SerializeField] private TrajectoryDrawer _trajectoryDrawerPrefab;

        [Tooltip("If the spring should show force trajectory")]
        [SerializeField] private bool _showTrajectory = false;

        private Animator _animator;
        private TrajectoryDrawer _trajectoryDrawer;
        private Vector2 _colliderBounds;
        private Vector2 _originalColliderSize;
        private Vector2 _originalColliderOffset;

        private bool _isImpulsing = false;

        private new void Awake()
        {
            base.Awake();

            _animator = GetComponent<Animator>();

            _boxCollider = GetComponent<BoxCollider2D>();
            _originalColliderSize = _boxCollider.size;
            _originalColliderOffset = _boxCollider.offset;

            _colliderBounds = GetComponent<SpriteRenderer>().sprite.bounds.size;

            InitTrajectoryDrawer();
        }

        private void Start()
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _direction);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {

            if (Mode == PlanningMode.Playing && collider == _controllerFeet && !_isImpulsing)
            {
                EnterSpring();
            }
        }

        protected override void OnChangePlanningMode(PlanningMode newMode)
        {
            switch (newMode)
            {
                case PlanningMode.Planning:
                    _colliderBounds = GetComponent<SpriteRenderer>().sprite.bounds.size;
                    if (_showTrajectory)
                    {
                        ActivateTrajectoryDrawer(true);
                    }

                    SetColliderForClicking();
                    break;

                case PlanningMode.Waiting:
                    ActivateTrajectoryDrawer(false);
                    break;

                case PlanningMode.Playing:
                    SetColliderForBouncing();
                    ActivateTrajectoryDrawer(false);
                    break;
                default:
                    break;
            }
        }

        private void EnterSpring()
        {
            _isImpulsing = true;

            Vector3 newPosition = _boxCollider.bounds.center + _controller.transform.position - _controllerFeet.transform.position;

            _controller.Teleport(newPosition, ImpulseSpring, false);
        }

        private void ImpulseSpring()
        {
            _ = StartCoroutine(YieldImpulse());
        }

        private IEnumerator YieldImpulse()
        {
            _controller.Impulse(_FORCE * _jumpForce, _direction);
            _animator.SetTrigger(SpringAnimationNames.Activate);

            yield return new WaitForSeconds(0.3f);

            _isImpulsing = false;
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

        private void InitTrajectoryDrawer()
        {
            if (_trajectoryDrawerPrefab == null)
            {
                return;
            }

            _trajectoryDrawer = Instantiate(_trajectoryDrawerPrefab, _boxCollider.bounds.center, transform.rotation, transform);
            _trajectoryDrawer.InitializeData(_jumpForce * _FORCE, _direction, _controller.GetComponent<Rigidbody2D>());
            _trajectoryDrawer.gameObject.SetActive(false);
        }

        private void ActivateTrajectoryDrawer(bool activate)
        {
            if (_trajectoryDrawerPrefab != null)
            {

                _trajectoryDrawer.gameObject.SetActive(activate);
            }
        }
    }
}