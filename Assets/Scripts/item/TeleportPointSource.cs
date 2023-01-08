using UnityEngine;

namespace Assets.Scripts.item
{
    public class TeleportPointSource : Jumper
    {
        [Tooltip("Jumper to teleport to")]
        [SerializeField] private Jumper _nextTelepoint;

        [Tooltip("Max teleporting length")]
        [SerializeField] private float _maxTeleportingLength = 20f;

        [Tooltip("Ray prefab")]
        [SerializeField] private TeleportRay _rayPrefab;

        private TeleportRay _rayPrefabInstance;



        private void Start()
        {
            if (_nextTelepoint && !_rayPrefabInstance)
            {
                _rayPrefabInstance = Instantiate(_rayPrefab, transform.position, transform.rotation, transform);
                _rayPrefabInstance.sourcePoint = this;
                _rayPrefabInstance.targetPoint = _nextTelepoint;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (mode != PlanningMode.Playing)
            {
                return;
            }

            if (other.gameObject == _controller.gameObject)
            {
                if (IsTeleportable())
                {
                    _controller.Teleport(_nextTelepoint.transform.position);
                }
            }
        }

        private bool IsTeleportable()
        {
            if (!_nextTelepoint || _nextTelepoint.mode != PlanningMode.Playing)
            {
                return false;
            }

            float distance = Vector2.Distance(transform.position, _nextTelepoint.transform.position);

            Vector3 directionToNextPoint = Vector3.Normalize(_nextTelepoint.transform.position - transform.position);
            RaycastHit2D[] inBetweenHits = Physics2D.RaycastAll(transform.position, directionToNextPoint, distance, LayerMask.GetMask("Level", "Jumper"));

            return distance <= _maxTeleportingLength && inBetweenHits.Length <= 2;
        }

        protected override void OnChangePlanningMode(PlanningMode newMode)
        {
            //--
        }
    }
}