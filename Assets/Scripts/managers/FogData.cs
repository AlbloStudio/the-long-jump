using Assets.Scripts.utils;
using UnityEngine;

namespace Assets.Scripts.managers
{
    public class FogData : Singleton<FogData>
    {
        [Tooltip("a texture containing fog colors: first column is the back, second column is the front. They show the top to bottom colors in full fog. ")]
        public Texture2D colors;

        [Tooltip("bounds in which z will count")]
        public Vector2 bounds = new Vector2(-1f, 15f);
    }
}