using UnityEngine;
using Assets.Scripts.being;
using Assets.Scripts.managers;

namespace Assets.Scripts.item
{
    public class SpringJumper : MonoBehaviour
    {
        [Tooltip("How strong is the spring")]
        [SerializeField] private float jumpForce = 1.5f;

        private CharacterMover _controller;

        private void OnEnable()
        {
            _controller = GeneralData.Instance.player;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                _controller.Jump(jumpForce);
            }
        }
    }
}