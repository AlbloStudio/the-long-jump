using UnityEngine;

namespace Assets.Scripts.trigger
{
    public class DeathTeleporter : MonoBehaviour
    {
        [Tooltip("The place where the obejct will teleport to")]
        [SerializeField] private Transform teleportTarget;

        [Tooltip("The object that will be teleported")]
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