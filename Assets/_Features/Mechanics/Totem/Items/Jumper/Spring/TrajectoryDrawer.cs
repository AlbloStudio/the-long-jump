using UnityEngine;

public class TrajectoryDrawer : MonoBehaviour
{
    [Tooltip("The Force of the impulse")]
    [SerializeField] private float _force = 1;
    [Tooltip("The Direction of the impulse")]
    [SerializeField] private Vector2 _direction = new(1, 1);
    [Tooltip("The object that is going to be simulated")]
    [SerializeField] private Rigidbody2D _bodyToSimulate = null;

    private LineRenderer _line;

    private float _speed;

    private void Start()
    {
        _line = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        SimulateArc();
    }

    private void SimulateArc()
    {
        float maxDuration = 3f;
        float timeStepInterval = 0.01f;
        int maxSteps = (int)(maxDuration / timeStepInterval);

        float mass = _bodyToSimulate ? _bodyToSimulate.mass : 1;
        float gravityScale = _bodyToSimulate ? _bodyToSimulate.gravityScale : 1;

        Vector2 launchPosition = transform.position;

        _line.positionCount = maxSteps;

        _speed = _force / mass * Time.fixedDeltaTime;

        for (int i = 0; i < maxSteps; ++i)
        {
            Vector2 calculatedPosition = launchPosition + (_speed * i * timeStepInterval * _direction);
            calculatedPosition.y += Physics2D.gravity.y * gravityScale / 2 * Mathf.Pow((i + 1) * timeStepInterval, 2);

            _line.SetPosition(i, calculatedPosition);
        }
    }

    public void InitializeData(float force, Vector2 direction, Rigidbody2D bodyToSimulate)
    {
        _force = force;
        _direction = direction;
        _bodyToSimulate = bodyToSimulate;
    }
}