using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class WheelIllusion : AbstractIllusionPattern
{
    public int width = 2048;
    public int height = 2048;
    public int numSquares = 36;
    public float radius = 500f;
    public float squareSize = 60f;
    public int dotDiameter = 8;
    public Color dotColor = Color.black;
    private int CurrentPatternHeight;
    private int CurrentPatternWidth;
    private float stepSize;

    // Constructor
    public wheelLength(float stepSize = 0.1f)
    {   
        CurrentPatternHeight = InitPatternHeight;
        CurrentPatternWidth = InitPatternWidth;
        this.stepSize = stepSize;
        texture = GeneratePattern(CurrentPatternHeight, CurrentPatternWidth);
    }

    public override Texture2D GeneratePattern()
    {
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        Color backgroundColor = Color.grey;

        // Fill background
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                texture.SetPixel(i, j, backgroundColor);
            }
        }

        Vector2 center = new Vector2(width / 2, height / 2);
        float adjustedRadius = radius * CurrentPatternWidth / 32.0f; // Adjust radius based on current pattern width

        // Draw rotated squares
        for (int i = 0; i < numSquares; i++)
        {
            float angle = 2 * Mathf.PI * i / numSquares;
            Vector2 squareCenter = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * adjustedRadius;
            DrawRotatedSquare(texture, squareCenter, CurrentPatternHeight, angle + Mathf.PI / 2, color.black, color.white); // Use CurrentPatternHeight for square size
        }

        // Draw the dot at the center
        DrawDot(texture, center, dotDiameter, dotColor);

        texture.Apply();
        return texture;
    }


    void DrawRotatedSquare(Texture2D texture, Vector2 center, float size, float angle, Color color1, Color color2)
    {
        Vector2 halfSize = Vector2.one * (size / 2);
        Vector2[] corners = new Vector2[4];
        
        // Define each corner of the square relative to the center
        corners[0] = RotatePoint(center, center - halfSize, angle);
        corners[1] = RotatePoint(center, new Vector2(center.x + halfSize.x, center.y - halfSize.y), angle);
        corners[2] = RotatePoint(center, center + halfSize, angle);
        corners[3] = RotatePoint(center, new Vector2(center.x - halfSize.x, center.y + halfSize.y), angle);

        // Colors for the lines, alternating between two provided colors
        Color[] lineColors = { color1, color2, color1, color2 };

        // Draw lines between each corner point
        for (int i = 0; i < 4; i++)
        {
            DrawLine(texture, corners[i], corners[(i + 1) % 4], lineColors[i]);
        }
    }

    Vector2 RotatePoint(Vector2 pivot, Vector2 point, float angle)
    {
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);
        point -= pivot;
        Vector2 rotated = new Vector2(point.x * cos - point.y * sin, point.x * sin + point.y * cos);
        return rotated + pivot;
    }

    void DrawLine(Texture2D texture, Vector2 start, Vector2 end, Color color)
    {
        int x0 = (int)start.x;
        int y0 = (int)start.y;
        int x1 = (int)end.x;
        int y1 = (int)end.y;

        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = (dx > dy ? dx : -dy) / 2;
        int e2;

        while (true)
        {
            texture.SetPixel(x0, y0, color);
            if (x0 == x1 && y0 == y1) break;
            e2 = err;
            if (e2 > -dx) { err -= dy; x0 += sx; }
            if (e2 < dy) { err += dx; y0 += sy; }
        }
    }


    public override float GetCurrentRatio()
    {
        return ((float)CurrentPatternWidth) / CurrentPatternHeight;
    }

    public override float GetInitRatio()
    {
        return ((float)InitPatternWidth) / InitPatternHeight;
    }

    void SetPatternRatio(float ratio)
    {
        float adjustedWidth = CurrentPatternHeight * ratio;
        if (adjustedWidth < CurrentPatternWidth)
            CurrentPatternWidth = Math.Min(CurrentPatternWidth - 1, (int)adjustedWidth);
        else if (adjustedWidth > CurrentPatternWidth)
            CurrentPatternWidth = Math.Max(CurrentPatternWidth + 1, (int)adjustedWidth);
        else
            Debug.Log("No change in pattern width! Really? Double check ratio!");
    }

    public override void IncreasePatternRatio()
    {
        // Debug.Log("current ratio: " + GetCurrentRatio() + " step: " + step);
        SetPatternRatio(GetCurrentRatio() + stepSize);
        // Debug.Log("after set pattern, current ratio: " + GetCurrentRatio() + " step: " + step);
        GeneratePattern(CurrentPatternHeight, CurrentPatternWidth);
    }
    public override void DecreasePatternRatio()
    {
        // Debug.Log("current ratio: " + GetCurrentRatio() + " step: " + step);
        SetPatternRatio(GetCurrentRatio() - stepSize);
        // Debug.Log("after set pattern, current ratio: " + GetCurrentRatio() + " step: " + step);
        GeneratePattern(CurrentPatternHeight, CurrentPatternWidth);
    }

    void ResetPattern()
    {
        CurrentPatternHeight = InitPatternHeight;
        CurrentPatternWidth = InitPatternWidth;
        GeneratePattern(CurrentPatternHeight, CurrentPatternWidth);
    }

    public override float GetMinRatio()
    {
        return 0.1f;
    }
    public override float GetMaxRatio()
    {
        return GetInitRatio() * 1.2f;
    }
}
