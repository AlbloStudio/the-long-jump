using Assets.Scripts.being;
using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.managers
{
    public class GeneralData : Singleton<GeneralData>
    {
        [Tooltip("Player")]
        public CharacterMover player;

        [Tooltip("Player feet for stuff like springs")]
        public Collider2D playerFeet;

        [Tooltip("Main Camera")]
        public CinemachineVirtualCamera mainCamera;
    }
}