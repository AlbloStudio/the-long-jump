using Assets.Scripts.managers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.item
{
    public class TeleportPointSource : Jumper
    {
        [Tooltip("Jumper to teleport to")]
        public Jumper targetPoint;

        [Tooltip("Max teleporting length")]
        [SerializeField] private float _maxTeleportingLength = 20f;

        [Tooltip("Ray prefab")]
        [SerializeField] private TeleportRay _rayPrefab;

        private TeleportRay _rayPrefabInstance;

        private void Start()
        {
            if (targetPoint)
            {
                _rayPrefabInstance = Instantiate(_rayPrefab, transform.position, transform.rotation, transform.parent);
                _rayPrefabInstance.teleport = this;
            }

            _charAction = GeneralData.Instance.charAction;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (mode != PlanningMode.Playing)
            {
                return;
            }

            if (other.gameObject == _charAction.gameObject)
            {
                if (IsTeleportable())
                {
                    _charAction.Teleport(targetPoint.transform.position);
                }
            }
        }

        public bool IsRayTrazable()
        {
            return AreTeleportersInMode(new[] { PlanningMode.Planning, PlanningMode.Playing });
        }

        public bool IsFarAway()
        {
            float distance = GetDistanceBetweenTeleporters();

            return distance > _maxTeleportingLength;
        }

        public bool IsObstructed()
        {
            float distance = GetDistanceBetweenTeleporters();

            Vector3 directionToNextPoint = Vector3.Normalize(targetPoint.transform.position - transform.position);
            RaycastHit2D[] inBetweenHits = Physics2D.RaycastAll(transform.position, directionToNextPoint, distance, LayerMask.GetMask("Level", "Jumper"));
            return inBetweenHits.Length > 2;
        }

        private bool IsTeleportable()
        {
            return targetPoint && targetPoint.mode == PlanningMode.Playing && !IsFarAway() && !IsObstructed();
        }

        private float GetDistanceBetweenTeleporters()
        {
            return Vector2.Distance(transform.position, targetPoint.transform.position);
        }

        private bool AreTeleportersInMode(PlanningMode[] modes)
        {
            List<PlanningMode> allowedModes = new(modes);
            return allowedModes.Contains(mode) && allowedModes.Contains(targetPoint.mode);
        }

        protected override void OnChangePlanningMode(PlanningMode newMode)
        {
            //--
        }
    }
}