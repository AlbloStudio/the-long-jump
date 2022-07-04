using UnityEngine;
using Assets.Scripts.being;

namespace Assets.Scripts.item
{
    public class SpringJumper : MonoBehaviour
    {
        [Tooltip("How strong is the spring")]
        [SerializeField] private float jumpForce = 1.5f;

        public CharacterMover _controller;

        private void Enable()
        {
            _controller = FindObjectOfType<CharacterMover>();
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