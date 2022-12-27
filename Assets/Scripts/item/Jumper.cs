using UnityEngine;
using Assets.Scripts.being;
using Assets.Scripts.managers;

namespace Assets.Scripts.item
{
    public abstract class Jumper : MonoBehaviour
    {
        public enum PlanningMode
        {
            Waiting = 0,
            Playing = 1,
            Planning = 2,
        }

        public PlanningMode mode = PlanningMode.Waiting;

        protected CharacterMover _controller;
        protected Collider2D _collider;

        protected void OnEnable()
        {
            _controller = GeneralData.Instance.player;
            _collider = GetComponent<Collider2D>();
        }

        public void SetPlanningMode(PlanningMode newMode)
        {
            mode = newMode;
            OnChangePlanningMode(newMode);
        }

        protected abstract void OnChangePlanningMode(PlanningMode newMode);
    }
}