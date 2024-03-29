﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

namespace Assets.Scripts.item
{
    public class TeleportPointSource : Jumper
    {
        [Tooltip("Jumper to teleport to")]
        [SerializeField] private Jumper _targetPoint;

        [Tooltip("Max teleporting length")]
        [SerializeField] private float _maxTeleportingLength = 20f;

        [Tooltip("Ray prefab")]
        [SerializeField] private TeleportRay _rayPrefab;

        public Jumper TargetPoint => _targetPoint;

        private TeleportRay _rayPrefabInstance;

        private bool _isTeleporting = false;

        private void Start()
        {
            if (_targetPoint != null)
            {
                _rayPrefabInstance = Instantiate(_rayPrefab, transform.position, transform.rotation, transform.parent);
                _rayPrefabInstance.teleport = this;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isTeleporting && Mode == PlanningMode.Playing && other.gameObject == _controller.gameObject)
            {
                TryTeleportation();
            }
        }

        public bool IsRayTrazable()
        {
            return AreSourceAndTargetInMode(new[] { PlanningMode.Planning, PlanningMode.Playing });
        }

        public bool IsFarAway()
        {
            return GetDistanceBetweenTeleporters() > _maxTeleportingLength;
        }

        public bool IsObstructed()
        {
            Vector3 directionToNextPoint = Vector3.Normalize(_targetPoint.transform.position - transform.position);
            RaycastHit2D[] inBetweenHits = Physics2D.RaycastAll(transform.position, directionToNextPoint, GetDistanceBetweenTeleporters(), LayerMask.GetMask("Level", "LevelParticlesCollision", "Jumper", "PlatformJumper"));

            return inBetweenHits.Length > 2;
        }

        protected override void OnChangePlanningMode(PlanningMode newMode)
        {
            //--
        }

        private void TryTeleportation()
        {
            if (IsTeleportable())
            {
                Vector3 newPosition = transform.position + _controller.transform.position - _controllerFeet.transform.position;
                Vector3 targetPosition = _targetPoint.transform.position + _controller.transform.position - _controllerFeet.transform.position;

                _controller.EnterTeleport();
                _controller.AddTeleportCommand(newPosition, null, true);
                _controller.AddTeleportCommand(targetPosition, FinishTeleporation, false);

                _isTeleporting = true;
            }
        }

        private void FinishTeleporation()
        {
            _controller.ExitTeleport();

            _ = StartCoroutine(YieldFinishTeleportation());
        }

        private IEnumerator YieldFinishTeleportation()
        {
            yield return new WaitForSeconds(0.3f);

            _isTeleporting = false;
        }

        private bool IsTeleportable()
        {
            return _targetPoint && _targetPoint.Mode == PlanningMode.Playing && !IsFarAway() && !IsObstructed();
        }

        private float GetDistanceBetweenTeleporters()
        {
            return Vector2.Distance(transform.position, _targetPoint.transform.position);
        }

        private bool AreSourceAndTargetInMode(PlanningMode[] modes)
        {
            List<PlanningMode> allowedModes = new(modes);
            return allowedModes.Contains(Mode) && allowedModes.Contains(_targetPoint.Mode);
        }
    }
}