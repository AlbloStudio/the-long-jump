using UnityEngine;
using Assets.Scripts.being;
using Assets.Scripts.managers;
using static Enum;

namespace Assets.Scripts.item
{
    public abstract class Jumper : MonoBehaviour
    {
        public PlanningMode Mode { get; private set; }

        protected CharacterMover _controller;
        protected Collider2D _controllerFeet;
        protected Collider2D _collider;

        protected void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _controller = GeneralData.Instance.Player;
            _controllerFeet = GeneralData.Instance.PlayerFeet;

            Mode = GetComponent<Item>() == null ? PlanningMode.Playing : PlanningMode.Waiting;
        }

        public void SetPlanningMode(PlanningMode newMode)
        {
            Mode = newMode;
            OnChangePlanningMode(newMode);
        }

        protected abstract void OnChangePlanningMode(PlanningMode newMode);
    }
}