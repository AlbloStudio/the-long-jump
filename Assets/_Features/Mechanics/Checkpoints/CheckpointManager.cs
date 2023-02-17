using Assets.Scripts.trigger;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.managers
{
    public class CheckpointManager : Singleton<CheckpointManager>
    {
        public Checkpoint ActiveCheckpoint { get; private set; }

        private void Awake()
        {
            Object[] checkpoints = Resources.FindObjectsOfTypeAll(typeof(Checkpoint));
            foreach (Checkpoint checkPoint in checkpoints.Cast<Checkpoint>())
            {
                checkPoint.CheckPointPassedEvent.AddListener(SetNewCheckpoint);
            }

            ActiveCheckpoint = (Checkpoint)checkpoints[0];
        }

        private void SetNewCheckpoint(Checkpoint newCheckPoint, Collider2D collided)
        {
            if (collided.transform.Equals(GeneralData.Instance.Player.transform))
            {
                ActiveCheckpoint = newCheckPoint;
            }
        }
    }
}