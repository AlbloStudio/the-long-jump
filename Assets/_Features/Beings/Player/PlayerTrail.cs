using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    private TrailRenderer _trail;

    private void Awake()
    {
        _trail = GetComponent<TrailRenderer>();
        _trail.emitting = false;
    }
}
