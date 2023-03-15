using Assets.Scripts.managers;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class TotemEvent : MonoBehaviour
{
    [SerializeField] private GameObject _totem;

    private void OnTriggerEnter2D(Collider2D collided)
    {
        if (collided.transform.Equals(GeneralData.Instance.Player.transform))
        {
            _totem.SetActive(true);
        }
    }
}
