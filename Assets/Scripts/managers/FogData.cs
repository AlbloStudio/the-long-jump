using Assets.Scripts.utils;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.managers
{
    public class FogData : Singleton<FogData>
    {
        public Color topColor;
        public Color bottomColor;

        [Tooltip("bounds in which z will count")]
        public Vector2 bounds = new Vector2(0f, 15f);

        [Tooltip("color reach for top and bottom")]
        public Vector2 verticalLimits = new Vector2(0.5f, 0.5f);
    }
}