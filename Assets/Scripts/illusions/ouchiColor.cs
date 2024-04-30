
using System;
using UnityEngine;

public class ouchiColor : ouchiLength
{
    private int foregroundColor = 1;
    private int backgroundColor = 255;

    public ouchiColor()
    {
        Debug.Log("========== Ouchi Color Start ==========");
        texture = GeneratePattern();
    }

    public override Texture2D GeneratePattern()
    {
        Color fgColor = new Color(foregroundColor, foregroundColor, foregroundColor);
        Color bgColor = new Color(backgroundColor, backgroundColor, backgroundColor);
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
        return (float)backgroundColor / foregroundColor;
    }

    public override float GetInitRatio()
    {
        return 255.0f;
    }

    public override void IncreasePatternRatio(float step)
    {
        if (foregroundColor + step <= 255)
        {
            foregroundColor += (int)step;
            backgroundColor -= (int)step;
        }
        texture = GeneratePattern();
    }

    public override void DecreasePatternRatio(float step)
    {
        if (backgroundColor - step >= 0)
        {
            backgroundColor += (int)step;
            foregroundColor -= (int)step;
        }
        texture = GeneratePattern();
    }
}