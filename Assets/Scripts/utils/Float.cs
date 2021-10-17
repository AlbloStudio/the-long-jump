using System;

namespace Assets.Scripts.utils
{
    public class Float
    {
        public const float Tolerance = 0.001f;

        public static bool Equals(float a, float b, float tolerance = Tolerance)
        {
            return Math.Abs(a - b) > tolerance;
        }
    }
}