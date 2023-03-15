using Assets.Scripts.being;
using Assets.Scripts.managers;
using UnityEngine;

namespace Assets.Scripts.totem
{
    public class SafeArea : MonoBehaviour
    {
        private BoxCollider2D _collider;
        private MeshGenerator _meshGenerator;
        private CharacterMover _char;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _meshGenerator = GetComponent<MeshGenerator>();
        }

        private void Start()
        {
            CalculateVertices();

            _char = GeneralData.Instance.Player;
        }

        private void CalculateVertices()
        {
            Vector2 min = _collider.bounds.min - transform.position;
            Vector2 max = _collider.bounds.max - transform.position;

            Vector2 topLeft = new(min.x, max.y);
            Vector2 topRight = new(max.x, max.y);
            Vector2 bottomLeft = new(min.x, min.y);

            _meshGenerator.PlaneSize = new(topRight.x - topLeft.x, topLeft.y - bottomLeft.y);
            _meshGenerator.Offset = min;
            _meshGenerator.GeneratePlane();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == _char.gameObject)
            {
                _char.SwitchTrailEmission(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            print("exiting");
            if (other.gameObject == _char.gameObject)
            {
                _char.SwitchTrailEmission(false);
            }
        }
    }
}
