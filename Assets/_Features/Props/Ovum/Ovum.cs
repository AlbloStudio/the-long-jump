using Assets.Scripts.managers;
using UnityEngine;

public class Ovum : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collided)
    {
        if (collided.transform.Equals(GeneralData.Instance.Player.transform))
        {
            GeneralData.Instance.Player.gameObject.SetActive(false);
        }
    }
}
