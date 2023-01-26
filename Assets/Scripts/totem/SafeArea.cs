using Assets.Scripts.utils;
using UnityEngine;

namespace Assets.Scripts.totem
{
    public class SafeArea : MonoBehaviour
    {
        private Mesh _mesh;
        private BoxCollider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();

            MeshFilter meshFilter = GetComponent<MeshFilter>();
            meshFilter.mesh = _mesh = new Mesh();

            CalculateVertices();
        }

        private void CalculateVertices()
        {

            Vector2 min = _collider.bounds.min - transform.position;
            Vector2 max = _collider.bounds.max - transform.position;

            Vector2 topLeft = new(min.x, max.y);
            Vector2 topRight = new(max.x, max.y);
            Vector2 bottomLeft = new(min.x, min.y);
            Vector2 bottomRight = new(max.x, min.y);

            Geometry.MeshData meshData = Geometry.CalculateRectangleMesh(topLeft, topRight, bottomLeft, bottomRight);
            _mesh.vertices = meshData.Vertices;
            _mesh.triangles = meshData.Triangles;

            Vector2[] uv = new[]
            {
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 0),
                new Vector2(1, 1),
            };
            _mesh.uv = uv;

        }
    }
}
