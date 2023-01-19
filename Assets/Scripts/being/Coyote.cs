using UnityEngine;

namespace Assets.Scripts.being
{
    public class Coyote : MonoBehaviour
    {

        [Tooltip("Amount of time that the player can still jump after leaving ground")]
        [SerializeField] private float coyoteTime = .1f;

        public bool IsCoyoting { get; private set; }
        [HideInInspector]
        public float _downwardsGravityScale;

        private float _coyoteCounter;
        private bool _wasCoyoting;

        private Rigidbody2D _body;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _coyoteCounter = coyoteTime;
        }

        public void CountCoyoteTime(bool isGrounded)
        {
            IsCoyoting = !isGrounded && _coyoteCounter > 0;

            if (IsCoyoting)
            {
                if (!_wasCoyoting)
                {
                    StartCoyote();
                }

                _coyoteCounter -= Time.fixedDeltaTime;

            }

            if (_coyoteCounter <= 0f)
            {
                EndCoyote();
            }

            _wasCoyoting = IsCoyoting;
        }

        private void StartCoyote()
        {
            // _body.gravityScale = 0;
            _body.velocity = new(_body.velocity.x, 0f);
        }

        public void EndCoyote()
        {
            _coyoteCounter = 0f;
            // _body.gravityScale = _downwardsGravityScale;
        }

        public void RestartCoyote()
        {
            EndCoyote();
            _coyoteCounter = coyoteTime;
        }
    }
}

