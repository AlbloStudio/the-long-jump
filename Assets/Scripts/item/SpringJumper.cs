using UnityEngine;
using Assets.Scripts.being;
using Assets.Scripts.managers;

namespace Assets.Scripts.item
{
    public class SpringJumper : Jumper
    {
        private const float _FORCE = 600f;

        [Tooltip("How strong is the spring")]
        [SerializeField] private float jumpForce = 1.5f;

        [Tooltip("Direction of the spring")]
        [SerializeField] private Vector2 direction = Vector2.up;

        private new void OnEnable()
        {
            base.OnEnable();
            _collider.isTrigger = false;
        }

        private void OnDisable()
        {
            _collider.isTrigger = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!enabled)
            {
                return;
            }

            if (collision.gameObject == _controller.gameObject)
            {
                _controller.Impulse(_FORCE * jumpForce, direction);
            }
        }
    }
}