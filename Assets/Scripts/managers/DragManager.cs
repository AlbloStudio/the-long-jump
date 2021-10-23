using Assets.Scripts.item;
using Cinemachine;
using System;
using UnityEngine;

namespace Assets.Scripts.managers
{
    internal enum Mode
    {
        Moving = 0,
        Planning = 1,
        Dragging = 2,
    }

    public class DragManager : MonoBehaviour
    {
        public static DragManager Instance { get; private set; }

        [Tooltip("The main Camera")]
        [SerializeField] private Camera mainCamera;

        [Tooltip("The ample camera to transition when player is here")]
        [SerializeField] private CinemachineVirtualCamera ampleCamera;

        private Mode _mode;

        private Jumper _currentJumper;

        private void OnEnable()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Update()
        {
            if (_currentJumper && _mode is Mode.Dragging)
            {
                Drag();
            }
        }

        private void Drag()
        {
            _currentJumper.transform.position = mainCamera.ScreenToWorldPoint(
                new Vector3(
                    Input.mousePosition.x,
                    Input.mousePosition.y,
                    Math.Abs(mainCamera.transform.position.z)
                )
            );
        }

        public void StartPlanningMode()
        {
            _mode = Mode.Planning;

            ampleCamera.enabled = true;
        }

        public void FinishPlanningMode()
        {
            if (_mode is Mode.Planning || _mode is Mode.Dragging)
            {
                _mode = Mode.Moving;

                if (_currentJumper)
                {
                    _currentJumper.FinishDraggingMode();
                    _currentJumper = null;
                }

                ampleCamera.enabled = false;
            }
        }

        public void StartDraggingMode(Jumper jumper)
        {
            if (_mode is Mode.Planning)
            {
                _mode = Mode.Dragging;

                _currentJumper = jumper;
                _currentJumper.StartDraggingMode();
            }
        }

        public void FinishDraggingMode()
        {
            if (_mode is Mode.Dragging)
            {
                _mode = Mode.Planning;

                _currentJumper.FinishDraggingMode();
                _currentJumper = null;
            }
        }
    }
}