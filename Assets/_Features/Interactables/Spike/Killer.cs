using Assets.Scripts.managers;
using UnityEngine;
using static Enum;

public class Killer : MonoBehaviour
{
    [SerializeField] private DeathType _deathType;
    [SerializeField] private CollisionType _collisionType;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_collisionType == CollisionType.Collision)
        {
            Kill(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collided)
    {
        if (_collisionType == CollisionType.Trigger)
        {
            Kill(collided.transform);
        }
    }

    private void Kill(Transform transform)
    {
        if (transform.Equals(GeneralData.Instance.Player.transform))
        {
            GeneralData.Instance.Player.Kill(_deathType);
        }
    }
}
