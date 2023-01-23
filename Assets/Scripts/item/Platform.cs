using static Enum;

namespace Assets.Scripts.item
{
    public class Platform : Jumper
    {
        protected override void OnChangePlanningMode(PlanningMode newMode)
        {
            switch (newMode)
            {
                case PlanningMode.Planning:
                case PlanningMode.Waiting:
                    _collider.isTrigger = true;
                    break;

                case PlanningMode.Playing:
                    _collider.isTrigger = false;
                    break;
                default:
                    break;
            }
        }
    }
}