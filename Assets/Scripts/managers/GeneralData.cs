using Assets.Scripts.being;
using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.managers
{
    public class GeneralData : Singleton<GeneralData>
    {
        [Tooltip("Player")]
        [SerializeField] private CharacterMover _player;

        [Tooltip("Player feet for stuff like springs")]
        [SerializeField] private Collider2D _playerFeet;

        [Tooltip("Main Camera to point right")]
        [SerializeField] private CinemachineVirtualCamera _mainCameraRight;

        [Tooltip("Main Camera to point left")]
        [SerializeField] private CinemachineVirtualCamera _mainCameraLeft;

        public CharacterMover Player => _player;
        public Collider2D PlayerFeet => _playerFeet;
        public CinemachineVirtualCamera MainCameraRight => _mainCameraRight;
        public CinemachineVirtualCamera MainCameraLeft => _mainCameraLeft;
    }
}