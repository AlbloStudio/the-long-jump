using UnityEngine;

public static class Utils
{
    public static float RandomRange(Vector2 bounds)
    {
        return Random.Range(bounds.x, bounds.y);
    }
}