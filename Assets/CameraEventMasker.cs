using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraEventMasker : MonoBehaviour
{
    [SerializeField]
    private LayerMask cameraEventMask;

    private void Awake()
    {
        GetComponent<Camera>().eventMask = cameraEventMask;
    }
}