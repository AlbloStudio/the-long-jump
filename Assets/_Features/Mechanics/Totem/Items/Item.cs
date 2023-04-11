using UnityEngine;
using UnityEngine.Rendering.Universal;
using static Enum;

namespace Assets.Scripts.item
{
    public class Item : MonoBehaviour
    {

        [Tooltip("The color when in planning mode")]
        [SerializeField] private Material _planningModeMaterial;

        [Tooltip("Can the item be moved?")]
        [SerializeField] private bool _isStatic = false;

        public bool IsStatic => _isStatic;

        private Collider2D _safeArea;
        private Jumper _jumper;
        private Renderer _renderer;
        private Light2D _light;

        private Vector2 _originalPosition;
        private Material _originalMaterial;

        private void Awake()
        {
            _originalPosition = transform.position;
            _jumper = GetComponent<Jumper>();
            _renderer = GetComponent<Renderer>();
            _light = GetComponent<Light2D>();

            _originalMaterial = _renderer.sharedMaterial;

            if (!_isStatic)
            {
                gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            if (_isStatic)
            {
                _jumper.SetPlanningMode(PlanningMode.Playing);
            }
        }

        private bool IsInSafeArea()
        {
            return _safeArea.bounds.Contains(new(transform.position.x, transform.position.y, _safeArea.transform.position.z));
        }

        public void EnterPlanningMode(Collider2D safeArea)
        {
            _safeArea = safeArea;

            if (!_isStatic)
            {
                gameObject.SetActive(true);
            }

            _jumper.SetPlanningMode(PlanningMode.Planning);

            if (_planningModeMaterial)
            {
                _renderer.sharedMaterial = _planningModeMaterial;
                if (_light)
                {
                    _light.enabled = true;
                }
            }
        }

        public void ExitPlanningMode()
        {
            _renderer.sharedMaterial = _originalMaterial;
            if (_light)
            {
                _light.enabled = false;
            }

            if (IsInSafeArea() || _isStatic)
            {
                _jumper.SetPlanningMode(PlanningMode.Playing);
            }
            else
            {
                gameObject.SetActive(false);

                transform.position = _originalPosition;

                _jumper.SetPlanningMode(PlanningMode.Waiting);
            }
        }

        public void ResetPosition()
        {
            transform.position = _originalPosition;
        }
    }
}