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

        [SerializeField] private CinemachineVirtualCamera _startCamera;

        public CharacterMover Player => _player;
        public Collider2D PlayerFeet => _playerFeet;
        public CinemachineVirtualCamera StartCamera => _startCamera;
    }
}