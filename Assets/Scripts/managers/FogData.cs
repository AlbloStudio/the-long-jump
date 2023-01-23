using UnityEngine;

namespace Assets.Scripts.managers
{
    public class FogData : Singleton<FogData>
    {
        [Tooltip("a texture containing fog colors: first column is the back, second column is the front. They show the top to bottom colors in full fog. ")]
        [SerializeField] private Texture2D _colors;

        [Tooltip("bounds in which z will count")]
        [SerializeField] private Vector2 _bounds = new(-1f, 15f);

        public Texture2D Colors => _colors;
        public Vector2 Bounds => _bounds;
    }
}