using UnityEngine;

public class Ground : MonoBehaviour
{
    [Tooltip("A mask determining what is ground to the character")]
    [SerializeField] private LayerMask whatIsGround = new();

    public bool OnGround { get; private set; }
    public float Friction { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveFiction(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveFiction(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnGround = false;
        Friction = 0;
    }

    private void EvaluateCollision(Collision2D collision)
    {
        if (whatIsGround == (whatIsGround | (1 << collision.gameObject.layer)))
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector2 normal = collision.GetContact(i).normal;
                OnGround |= normal.y >= 0.9f;

                if (OnGround)
                {
                    GetComponent<Action>()._impulseVelocity = Vector2.zero;
                }
            }
        }
    }

    private void RetrieveFiction(Collision2D collision)
    {
        PhysicsMaterial2D material = collision.rigidbody ? collision.rigidbody.sharedMaterial : null;

        Friction = 0;

        if (material != null)
        {
            Friction = material.friction;
        }
    }
}
