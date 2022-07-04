using Assets.Scripts.utils;
using UnityEngine;

namespace Assets.Scripts.managers
{
    public class FogData : Singleton<FogData>
    {
        [Tooltip("Top Screen Color")]
        public Color topColor;

        [Tooltip("Bottom Screen Color")]
        public Color bottomColor;

        [Tooltip("bounds in which z will count")]
        public Vector2 bounds = new Vector2(0f, 15f);

        [Tooltip("color reach for top and bottom")]
        public Vector2 verticalLimits = new Vector2(0.5f, 0.5f);
    }
}