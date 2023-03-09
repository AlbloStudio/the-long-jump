using UnityEngine;

namespace Assets.Scripts.managers
{
    public class FogData : Singleton<FogData>
    {
        [Tooltip("a texture containing fog colors: first column is the back, second column is the front. They show the top to bottom colors in full fog. ")]
        [SerializeField] private Gradient _colors;

        [Tooltip("Gradient for colors for the front")]
        [SerializeField] private Gradient _frontColors;

        [Tooltip("bounds in which z will count")]
        [SerializeField] private Vector2 _bounds = new(-1f, 15f);

        [SerializeField] private Material _foggyMaterial;

        [SerializeField] private bool _fogInEditor = false;

        public Gradient Colors => _colors;
        public Gradient FrontColors => _frontColors;
        public Vector2 Bounds => _bounds;
    }
}