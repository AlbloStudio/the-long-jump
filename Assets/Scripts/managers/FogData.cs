using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.managers
{
    public class FogData : Singleton<FogData>
    {
        [Tooltip("a texture containing fog colors: first column is the back, second column is the front. They show the top to bottom colors in full fog. ")]
        [SerializeField] private Texture2D _colors;

        [Tooltip("bounds in which z will count")]
        [SerializeField] private Vector2 _bounds = new(-1f, 15f);

        [Tooltip("how strong parallax is. 0 means no parallax, 1 means full parallax")]
        [Range(0, 1)]
        [SerializeField] private float _horizontalParallaxSpeed = 1;

        [Tooltip("how strong parallax is. 0 means no parallax, 1 means full parallax")]
        [Range(0, 1)]
        [SerializeField] private float _verticalParallaxSpeed = 0.3f;

        public Texture2D Colors => _colors;
        public Vector2 Bounds => _bounds;

        private GameObject[] _backgroundObjects;
        private Vector3 _lastCameraPosition;

        private void Awake()
        {
            _backgroundObjects = GameObject.FindGameObjectsWithTag("BackParallax");
            _lastCameraPosition = Camera.main.transform.position;

            CinemachineCore.CameraUpdatedEvent.AddListener(CameraUpdated);
        }

        private void CameraUpdated(CinemachineBrain brain)
        {
            Vector3 distanceMadeWithCamera = brain.transform.position - _lastCameraPosition;

            foreach (GameObject backgroundObject in _backgroundObjects)
            {
                if (utils.Cameras.IsObjectVisibleInCamera(backgroundObject, Camera.main))
                {

                    float depth = backgroundObject.transform.position.z;
                    float distanceRatio = Mathf.InverseLerp(0, Instance.Bounds.y, depth);

                    float additionX = _horizontalParallaxSpeed * distanceRatio * distanceMadeWithCamera.x;
                    float additionY = _verticalParallaxSpeed * distanceRatio * distanceMadeWithCamera.y;

                    backgroundObject.transform.position += new Vector3(additionX, additionY, 0);
                }
            }

            _lastCameraPosition = brain.transform.position;
        }
    }
}