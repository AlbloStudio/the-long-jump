using Assets.Scripts.being;
using Assets.Scripts.utils;
using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.managers
{
    public class GeneralData : Singleton<GeneralData>
    {
        [Tooltip("Player")]
        public CharacterMover player;

        [Tooltip("Main Camera")]
        public Camera mainCamera;

        [Tooltip("Planning Ample Camera")]
        public Camera ampleCamera;

        public CinemachineVirtualCamera AmpleCameraCineMachine { get; private set; }

        private new void Awake()
        {
            AmpleCameraCineMachine = ampleCamera.GetComponent<CinemachineVirtualCamera>();
        }
    }
}