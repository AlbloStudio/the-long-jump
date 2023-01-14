using UnityEngine;

public class Action : MonoBehaviour
{
    public Vector2 _impulseVelocity = Vector2.zero;

    private Rigidbody2D _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    public void Teleport(Vector2 position)
    {
        _body.velocity = Vector2.zero;
        transform.position = position;
    }

    public void Impulse(float force, Vector2 direction)
    {
        _impulseVelocity = direction * (force / _body.mass * Time.fixedDeltaTime);

        _body.AddForce(force * direction);
    }
}
