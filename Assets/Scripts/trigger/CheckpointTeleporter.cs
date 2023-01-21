using UnityEngine;

namespace Assets.Scripts.trigger
{
    public class CheckpointTeleporter : MonoBehaviour
    {
        [Tooltip("The place where the obejct will teleport to")]
        [SerializeField] private Checkpoint[] checkPoints;

        [Tooltip("The object that will be teleported")]
        [SerializeField] private Transform teleportee;

        private Checkpoint _activeCheckpoint;

        private void Awake()
        {
            foreach (Checkpoint checkPoint in checkPoints)
            {
                checkPoint.CheckPointPassedEvent.AddListener(SetNewCheckpoint);
            }
        }

        private void OnTriggerEnter2D(Collider2D collided)
        {
            if (collided.transform.Equals(teleportee))
            {
                Checkpoint teleportTarget = _activeCheckpoint ? _activeCheckpoint : checkPoints[0];
                teleportee.transform.position = teleportTarget.transform.position;
            }
        }

        private void SetNewCheckpoint(Checkpoint newCheckPoint, Collider2D collided)
        {
            if (collided.transform.Equals(teleportee))
            {
                _activeCheckpoint = newCheckPoint;
            }
        }
    }
}