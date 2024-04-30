
using System;
using UnityEngine;

public class ouchiColor : ouchiLength
{
    private float foregroundColor = 0.0f;
    private float backgroundColor = 1.0f;
    private float stepSize;

    public ouchiColor(float stepSize = 0.01f)
    {
        Debug.Log("========== Ouchi Color Start ==========");
        texture = GeneratePattern();
        this.stepSize = stepSize;
    }

    public override Texture2D GeneratePattern()
    {
        Color fgColor = new Color(foregroundColor, foregroundColor, foregroundColor, 1);
        Color bgColor = new Color(backgroundColor, backgroundColor, backgroundColor, 1);
        texture = new Texture2D(width, height);
        // define the background pattern
        for (int h = 0; h < height; h++)
            for (int w = 0; w < width; w++)
            {
                texture.SetPixel(w, h, (w / InitPatternWidth % 2 == h / InitPatternHeight % 2) ? fgColor : bgColor);
            }
        // define the center circular pattern
        int centerX = width / 2;
        int centerY = height / 2;
        for (int h = 0; h < height; h++)
            for (int w = 0; w < width; w++)
            {
                if (Mathf.Pow(w - centerX, 2) + Mathf.Pow(h - centerY, 2) < Mathf.Pow(radius, 2))
                {
                    // texture.SetPixel(w, h, Color.red);
                    texture.SetPixel(h, w, ((w / InitPatternWidth) % 2 == (h / InitPatternHeight) % 2) ? fgColor : bgColor);
                }
            }

        texture.filterMode = FilterMode.Point;
        texture.Apply();
        return texture;
    }

    public override float GetCurrentRatio()
    {
        if (foregroundColor == 0)
        {
            return 0.0f;
        }
        else
        {
            return (float)backgroundColor / foregroundColor;
        }
    }

    public override float GetInitRatio()
    {
        return 10;
    }

    public override void DecreasePatternRatio()
    {

        if (foregroundColor + stepSize <= 1.0f)
        {
            foregroundColor += stepSize;
            backgroundColor -= stepSize;
        }
        texture = GeneratePattern();
    }

    public override void IncreasePatternRatio()
    {
        if (backgroundColor - stepSize >= 0.0f)
        {
            backgroundColor += stepSize;
            foregroundColor -= stepSize;
        }
        texture = GeneratePattern();
    }
}