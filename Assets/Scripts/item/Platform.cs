using UnityEngine;
using Assets.Scripts.being;
using Assets.Scripts.managers;

namespace Assets.Scripts.item
{
    public class Platform : Jumper
    {
        private new void OnEnable()
        {
            _collider.isTrigger = false;
        }

        private void OnDisable()
        {
            _collider.isTrigger = true;
        }
    }
}