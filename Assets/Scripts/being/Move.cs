using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 11f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 11f;

    private Vector2 _direction;
    private Vector2 _desiredVelocity;
    private Vector2 _velocity;
    private Rigidbody2D _body;
    private Ground _ground;
    private float _maxSpeedChange;
    private float _acceleration;
    private bool _onGround;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
    }

    private void Update()
    {
        _direction.x = input.RetrieveMoveInput();
        _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(maxSpeed - _ground.Friction, 0f);
    }

    private void FixedUpdate()
    {
        _onGround = _ground.OnGround;
        _velocity = _body.velocity;

        _acceleration = _onGround ? maxAcceleration : maxAirAcceleration;
        _maxSpeedChange = _acceleration * Time.fixedDeltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x + GetComponent<Action>()._impulseVelocity.x, _maxSpeedChange);

        _body.velocity = _velocity;
    }
}
