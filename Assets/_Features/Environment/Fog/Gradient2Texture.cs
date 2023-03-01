using UnityEngine;

public class Gradient2Texture
{

    public static Texture2D Create(Gradient[] grad, int width = 32)
    {
        Texture2D gradTex = new(width, grad.Length, TextureFormat.RGB24, false)
        {
            wrapMode = TextureWrapMode.Clamp
        };

        float inv = 1f / width;

        for (int y = 0; y < grad.Length; y++)
        {
            Gradient gradientToEvaluate = grad[y];

            for (int x = 0; x < width; x++)
            {
                float t = x * inv;
                Color col = gradientToEvaluate.Evaluate(t);
                gradTex.SetPixel(x, y, col);
            }
        }

        gradTex.Apply();

        return gradTex;
    }
}
