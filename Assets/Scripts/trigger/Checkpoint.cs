using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.trigger
{
    [RequireComponent(typeof(EdgeCollider2D))]
    public class Checkpoint : MonoBehaviour
    {
        public UnityEvent<Checkpoint, Collider2D> CheckPointPassedEvent { get; private set; } = new();

        [Tooltip("The number to press to teleport to this checkpoint")]
        [SerializeField] private int _number;

        public int Number => _number;

        private void Awake()
        {
            GetComponent<TMPro.TextMeshPro>().text = _number.ToString();
        }

        private void OnTriggerEnter2D(Collider2D collided)
        {
            CheckPointPassedEvent.Invoke(this, collided);
        }
    }
}
