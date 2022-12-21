using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.item
{
    public class TeleportPoint : Jumper
    {
        [Tooltip("point to teleport to")]
        [SerializeField] private TeleportPoint _nextTelepoint;

        [Tooltip("max teleporting length")]
        [SerializeField] private float _maxTeleportingLength = 20f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!enabled)
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
            float distance = Vector2.Distance(transform.position, _nextTelepoint.transform.position);

            var directionToNextPoint = Vector3.Normalize(_nextTelepoint.transform.position - transform.position);
            var inBetweenHits = Physics2D.RaycastAll(transform.position, directionToNextPoint, distance, LayerMask.GetMask("Level", "Totem Item"));

            return _nextTelepoint && _nextTelepoint.isActiveAndEnabled && distance <= _maxTeleportingLength && inBetweenHits.Length <= 2;
        }
    }
}