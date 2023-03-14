using Assets.Scripts.managers;
using UnityEngine;
using static Enum;

namespace Assets.Scripts.trigger
{
    public class FallDeath : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collided)
        {
            if (collided.transform.Equals(GeneralData.Instance.Player.transform))
            {
                GeneralData.Instance.Player.Kill(DeathType.Abism);
            }
        }
    }
}