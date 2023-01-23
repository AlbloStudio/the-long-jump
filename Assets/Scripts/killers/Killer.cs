using Assets.Scripts.managers;
using UnityEngine;

public class Killer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collided)
    {
        if (collided.transform.Equals(GeneralData.Instance.Player.transform))
        {
            GeneralData.Instance.Player.Kill();
        }
    }
}
