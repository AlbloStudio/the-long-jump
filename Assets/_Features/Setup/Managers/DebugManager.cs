using Assets.Scripts.trigger;
using UnityEngine;

namespace Assets.Scripts.managers
{
    public class DebugManager : Singleton<DebugManager>
    {
        public Checkpoint ActiveCheckpoint { get; private set; }

        private string _added = "";
        private bool _activeReturnKey;
        private bool isInDebugMode;
        private Object[] _checkpoints;

        private void Awake()
        {
            _checkpoints = Resources.FindObjectsOfTypeAll(typeof(Checkpoint));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                isInDebugMode = !isInDebugMode;
            }
        }

        private void OnGUI()
        {
            if (!isInDebugMode)
            {
                return;
            }

            _added = GUILayout.TextField(_added, 25, GUILayout.Width(300));

            if (GUI.changed)
            {
                _activeReturnKey = true;
            }

            if (_activeReturnKey)
            {
                if (PressedEnter())
                {
                    _activeReturnKey = false;

                    TryToTeleport();
                }
            }
        }

        private bool PressedEnter()
        {
            return Event.current.isKey && Event.current.keyCode == KeyCode.Return;
        }

        private void TryToTeleport()
        {
            if (int.TryParse(_added, out int j))
            {
                Object checkpointFound = System.Array.Find(_checkpoints, checkpoint => ((Checkpoint)checkpoint).Number == j);

                if (System.Array.Find(_checkpoints, checkpoint => ((Checkpoint)checkpoint).Number == j))
                {
                    GeneralData.Instance.Player.Teleport(((Checkpoint)checkpointFound).transform.position, gameObject);
                }
            }
        }
    }
}