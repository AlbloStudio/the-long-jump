using UnityEngine;

namespace Assets.Scripts
{
    public class DeathTeleporter : MonoBehaviour
    {
        [SerializeField] private Transform teleportTarget;
        [SerializeField] private Transform objectToTeleport;

        private void OnTriggerEnter2D(Collider2D collided)
        {
            if (collided.transform.Equals(objectToTeleport))
            {
                objectToTeleport.transform.position = teleportTarget.transform.position;
            }
        }
    }
}
