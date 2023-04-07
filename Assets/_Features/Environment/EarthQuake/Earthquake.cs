using Assets.Scripts.managers;
using UnityEngine;

public class Earthquake : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collided)
    {
        if (collided.transform.Equals(GeneralData.Instance.Player.transform))
        {

        }
    }
}
