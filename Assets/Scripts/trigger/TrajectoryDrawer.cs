using UnityEngine;

public class TrajectoryDrawer : MonoBehaviour
{
    [Tooltip("The Force of the impulse")]
    public float Force;
    [Tooltip("The Direction of the impulse")]
    public Vector2 Direction;
    [Tooltip("The object that is going to be simulated")]
    public Rigidbody2D _objectToSimulate = null;

    private LineRenderer _line;

    private float _vel;

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
        Vector2 launchPosition = transform.position;

        _line.positionCount = maxSteps;

        _vel = Force / _objectToSimulate.mass * Time.fixedDeltaTime;

        for (int i = 0; i < maxSteps; ++i)
        {
            Vector2 calculatedPosition = launchPosition + (_vel * i * timeStepInterval * Direction);
            calculatedPosition.y += Physics2D.gravity.y * _objectToSimulate.gravityScale / 2 * Mathf.Pow((i + 1) * timeStepInterval, 2);

            _line.SetPosition(i, calculatedPosition);
        }
    }
}