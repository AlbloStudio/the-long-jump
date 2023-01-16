using UnityEngine;

namespace Assets.Scripts.item
{
    public class Item : MonoBehaviour
    {
        private Vector2 _originalPosition;

        private Collider2D _safeArea;
        private Jumper _jumper;

        private void Awake()
        {
            _originalPosition = transform.position;
            _jumper = GetComponent<Jumper>();

            gameObject.SetActive(false);
        }

        private bool IsInSafeArea()
        {
            return _safeArea.bounds.Contains(new(transform.position.x, transform.position.y, 0));
        }

        public void EnterPlanningMode(Collider2D safeArea)
        {
            _safeArea = safeArea;

            gameObject.SetActive(true);

            _jumper.SetPlanningMode(Jumper.PlanningMode.Planning);
        }

        public void ExitPlanningMode()
        {
            if (IsInSafeArea())
            {
                _jumper.SetPlanningMode(Jumper.PlanningMode.Playing);
            }
            else
            {
                gameObject.SetActive(false);

                transform.position = _originalPosition;

                _jumper.SetPlanningMode(Jumper.PlanningMode.Waiting);
            }
        }
    }
}