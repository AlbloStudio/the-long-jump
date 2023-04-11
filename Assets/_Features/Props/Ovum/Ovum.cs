using Assets.Scripts.being;
using Assets.Scripts.managers;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Ovum : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collided)
    {
        if (collided.transform.Equals(GeneralData.Instance.Player.transform))
        {
            GameObject player = GeneralData.Instance.Player.gameObject;
            player.GetComponent<Renderer>().enabled = false;
            player.GetComponent<CharacterMover>().enabled = false;
            player.GetComponentInChildren<Light2D>().enabled = false;
            player.GetComponent<Rigidbody2D>().simulated = false;
        }
    }
}
