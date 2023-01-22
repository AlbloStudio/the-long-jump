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

        public PlanningMode mode;

        protected CharacterMover _controller;
        protected Collider2D _controllerFeet;
        protected Collider2D _collider;

        protected void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _controller = GeneralData.Instance.player;
            _controllerFeet = GeneralData.Instance.playerFeet;

            mode = GetComponent<Item>() == null ? PlanningMode.Playing : PlanningMode.Waiting;
        }

        public void SetPlanningMode(PlanningMode newMode)
        {
            mode = newMode;
            OnChangePlanningMode(newMode);
        }

        protected abstract void OnChangePlanningMode(PlanningMode newMode);
    }
}