using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.trigger
{
    [RequireComponent(typeof(EdgeCollider2D))]
    public class Checkpoint : MonoBehaviour
    {
        [Tooltip("The number to press to teleport to this checkpoint")]
        [SerializeField] private int _number;
        [SerializeField] private Transform _spawnPoint;

        public UnityEvent<Checkpoint, Collider2D> CheckPointPassedEvent { get; private set; } = new();
        public int Number => _number;
        public Vector3 SpawnPoint => _spawnPoint.position;

        private EdgeCollider2D _collider;

        private void Awake()
        {
            GetComponent<TMPro.TextMeshPro>().text = _number.ToString();
            _collider = GetComponent<EdgeCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collided)
        {
            CheckPointPassedEvent.Invoke(this, collided);
        }

        private void OnDrawGizmos()
        {
            if (!_collider)
            {
                _collider = GetComponent<EdgeCollider2D>();
            }

            Gizmos.DrawLine((Vector2)transform.position + _collider.points[0], (Vector2)transform.position + _collider.points[_collider.pointCount - 1]);
        }
    }
}
