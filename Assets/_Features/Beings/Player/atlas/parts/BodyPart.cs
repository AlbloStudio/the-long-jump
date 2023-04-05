using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField] private Vector2 _forceRange;

    private Rigidbody2D _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();

        float forceX = Random.Range(_forceRange.x, _forceRange.y);
        float forceY = Random.Range(_forceRange.x, _forceRange.y);

        _body.AddForce(new(forceX, forceY));
    }
}
