using Assets.Scripts.managers;
using UnityEngine;

public class CameraDirectionTrigger : MonoBehaviour
{
    [SerializeField] private Vector2 direction;

    private void OnTriggerEnter2D(Collider2D collided)
    {
        if (collided.transform.Equals(GeneralData.Instance.Player.transform))
        {
            GeneralData.Instance.MainCameraLeft.enabled = direction.x < 0;
        }
    }
}
