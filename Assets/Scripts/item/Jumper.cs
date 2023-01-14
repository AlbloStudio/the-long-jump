using UnityEngine;
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

        protected Action _charAction;
        protected Collider2D _collider;

        protected void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _charAction = GeneralData.Instance.charAction;
        }

        public void SetPlanningMode(PlanningMode newMode)
        {
            mode = newMode;
            OnChangePlanningMode(newMode);
        }

        protected abstract void OnChangePlanningMode(PlanningMode newMode);
    }
}