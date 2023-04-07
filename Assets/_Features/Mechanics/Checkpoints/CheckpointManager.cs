using Assets.Scripts.trigger;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.managers
{
    public class CheckpointManager : Singleton<CheckpointManager>
    {
        private readonly List<Checkpoint> _checkpoints = new();

        public Checkpoint ActiveCheckpoint { get; private set; }
        public Vector3 SpawnPoint => ActiveCheckpoint.SpawnPoint;
        public UnityEvent<Checkpoint> CheckpointSet { get; private set; } = new();

        private void Awake()
        {
            Object[] checkpoints = Resources.FindObjectsOfTypeAll(typeof(Checkpoint));
            foreach (Checkpoint checkPoint in checkpoints.Cast<Checkpoint>())
            {
                _checkpoints.Add(checkPoint);
                checkPoint.CheckPointPassedEvent.AddListener(SetNewCheckpoint);
            }

            ActiveCheckpoint = (Checkpoint)checkpoints[0];
        }

        private void SetNewCheckpoint(Checkpoint newCheckPoint, Collider2D collided)
        {
            if (collided.transform.Equals(GeneralData.Instance.Player.transform))
            {
                ActiveCheckpoint = newCheckPoint;
                CheckpointSet?.Invoke(ActiveCheckpoint);
            }
        }

        public void SetNewCheckpointByIndex(int index)
        {
            Checkpoint checkpoint = _checkpoints.Find(checkpoint => checkpoint.Number == index);

            if (checkpoint)
            {
                ActiveCheckpoint = checkpoint;
            }
        }
    }
}