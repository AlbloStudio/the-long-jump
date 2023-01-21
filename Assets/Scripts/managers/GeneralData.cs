using Assets.Scripts.being;
using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.managers
{
    public class GeneralData : Singleton<GeneralData>
    {
        [Tooltip("Player")]
        public CharacterMover player;

        [Tooltip("Main Camera")]
        public CinemachineVirtualCamera mainCamera;
    }
}