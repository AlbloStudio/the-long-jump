using Assets.Scripts.utils;
using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.managers
{
    public class GeneralData : Singleton<GeneralData>
    {
        [Tooltip("Player")]
        public Action charAction;

        [Tooltip("Main Camera")]
        public CinemachineVirtualCamera mainCamera;

        [Tooltip("Planning Ample Camera")]
        public CinemachineVirtualCamera AmpleCamera;
    }
}