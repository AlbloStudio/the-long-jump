using Assets.Scripts.managers;
using UnityEngine;

public class CameraDirectionTrigger : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera NewCamera;

    private void OnTriggerEnter2D(Collider2D collided)
    {
        if (collided.transform.Equals(GeneralData.Instance.Player.transform))
        {
            Cinemachine.CinemachineVirtualCamera currentVirtualCamera = Cinemachine.CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera as Cinemachine.CinemachineVirtualCamera;
            currentVirtualCamera.enabled = false;

            NewCamera.enabled = true;
        }
    }
}
