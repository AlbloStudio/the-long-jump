using UnityEngine;

namespace Assets.Scripts.managers
{
    public class DebugManager : Singleton<DebugManager>
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.End))
            {
                TryToTeleportForward();
            }

            if (Input.GetKeyDown(KeyCode.Home))
            {
                TryToTeleportBackward();
            }
        }

        private void TryToTeleportForward()
        {
            TryToTeleport(CheckpointManager.Instance.ActiveCheckpoint.Number + 1);
        }

        private void TryToTeleportBackward()
        {
            int minNumber = Mathf.Max(0, CheckpointManager.Instance.ActiveCheckpoint.Number - 1);
            TryToTeleport(minNumber);
        }

        private void TryToTeleport(int index)
        {
            CheckpointManager.Instance.SetNewCheckpointByIndex(index);
            GeneralData.Instance.Player.Kill(Enum.DeathType.Reset, 0f);
        }
    }
}