using UnityEngine;
using Assets.Scripts.being;
using Assets.Scripts.managers;

namespace Assets.Scripts.item
{
    public class Jumper : MonoBehaviour
    {
        protected CharacterMover _controller;
        protected Collider2D _collider;

        protected void OnEnable()
        {
            _controller = GeneralData.Instance.player;
            _collider = GetComponent<Collider2D>();
        }
    }
}