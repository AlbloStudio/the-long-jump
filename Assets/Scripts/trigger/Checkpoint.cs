using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.trigger
{
    [RequireComponent(typeof(EdgeCollider2D))]
    [CanEditMultipleObjects]
    public class Checkpoint : MonoBehaviour
    {
        public UnityEvent<Checkpoint, Collider2D> CheckPointPassedEvent { get; private set; } = new();

        private void OnTriggerEnter2D(Collider2D collided)
        {
            CheckPointPassedEvent.Invoke(this, collided);
        }
    }
}
