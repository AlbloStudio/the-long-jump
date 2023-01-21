using Assets.Scripts.managers;
using UnityEngine;

namespace Assets.Scripts.trigger
{
    public class FallDeath : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collided)
        {
            if (collided.transform.Equals(GeneralData.Instance.player.transform))
            {
                GeneralData.Instance.player.Kill();
            }
        }
    }
}