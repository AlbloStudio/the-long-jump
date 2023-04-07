using Assets.Scripts.trigger;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.managers
{
    [System.Serializable]
    public class Save
    {
        public int CheckpointIndex;

        public Save(int checkpointIndex)
        {
            CheckpointIndex = checkpointIndex;
        }
    }

    public class SaveSystem : Singleton<SaveSystem>
    {
        [SerializeField] private StartMenuHandler _startMenu;

        private CheckpointManager _checkpointManager;
        private Save _save;
        private string _filePath;

        private void Awake()
        {
            _filePath = Application.persistentDataPath + "/save.json";

            ReadFile();

            if (_save != null)
            {
                _startMenu.enabled = false;
                _startMenu.gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            _checkpointManager = CheckpointManager.Instance;
            _checkpointManager.CheckpointSet.AddListener(SaveNewCheckpoint);

            if (_save != null)
            {
                _checkpointManager.SetNewCheckpointByIndex(_save.CheckpointIndex);
                GeneralData.Instance.Player.transform.position = _checkpointManager.ActiveCheckpoint.SpawnPoint;
            }
        }

        private void SaveNewCheckpoint(Checkpoint newCheckPoint)
        {
            _save = new(newCheckPoint.Number);
            string json = JsonUtility.ToJson(_save);
            File.WriteAllText(_filePath, json);

            print("Saved game at checkpoint " + _save.CheckpointIndex);
        }

        public void ReadFile()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                _save = JsonUtility.FromJson<Save>(json);

                print("Loaded game at checkpoint " + _save.CheckpointIndex);
            }
        }
    }
}