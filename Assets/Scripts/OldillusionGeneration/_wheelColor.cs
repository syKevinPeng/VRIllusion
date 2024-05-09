using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class _wheelColor : abstractIllusionPattern
{
    public int width = 1028;
    public int height = 1028;
    public int InitPatternHeight = 512;
    public int InitPatternWidth = 512;
    public int numSquares = 30;
    public float radius = 500;
    public new float squareSize = 20;
    public int dotDiameter = 8;
    public Color dotColor = Color.black;
    private int CurrentPatternWidth;
    private int CurrentPatternHeight;
    private float foregroundColor = 0.0f;
    private float backgroundColor = 1.0f;
    private float stepSize;

    // Constructor
    public _wheelColor(float stepSize = 0.1f)
    {
        CurrentPatternHeight = InitPatternHeight;
        CurrentPatternWidth = InitPatternWidth;
        this.stepSize = stepSize;
        texture = GeneratePattern(CurrentPatternWidth, CurrentPatternHeight);
    }

    public override Texture2D GeneratePattern()
    {
        texture = GeneratePattern(width, height);
        return texture;
    }

    public Texture2D GeneratePattern(int width, int height)
    {
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        Color bgColor = new Color(backgroundColor, backgroundColor, backgroundColor, 1);
        Color fgColor = new Color(foregroundColor, foregroundColor, foregroundColor, 1);
        Color backgroundgroundColor = Color.grey;

        // Fill background
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                texture.SetPixel(i, j, backgroundgroundColor);
            }
        }

        Vector2 center = new Vector2(width / 2, height / 2);
        float adjustedOuterRadius = radius * width / 2048f; // Adjusted to use passed width for scaling
        float adjustedInnerRadius = adjustedOuterRadius * 0.80f; // Smaller radius for inner layer
        float innerSquareSize = squareSize * 0.80f; // Optionally smaller squares for the inner layer

        // Draw outer layer of rotated squares
        for (int i = 0; i < numSquares; i++)
        {
            float angle = 2 * Mathf.PI * i / numSquares;
            Vector2 outerSquareCenter = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * adjustedOuterRadius;
            DrawRotatedSquare(texture, outerSquareCenter, squareSize, angle + Mathf.PI / 2, bgColor, fgColor);
        }

        // Draw inner layer of rotated squares
        for (int i = 0; i < numSquares; i++)
        {
            float angle = 2 * Mathf.PI * i / numSquares;
            Vector2 innerSquareCenter = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * adjustedInnerRadius;
            DrawRotatedSquare(texture, innerSquareCenter, innerSquareSize, angle + Mathf.PI / 2, bgColor, fgColor);
        }

        // Draw the dot at the center
        DrawDot(texture, center, dotDiameter, dotColor);

        texture.Apply(); // Apply all changes to the texture
        return texture;
    }




    void DrawDot(Texture2D texture, Vector2 position, int diameter, Color color)
    {
        int x0 = (int)position.x;
        int y0 = (int)position.y;

        // Draw a simple square dot
        for (int x = x0 - diameter / 2; x <= x0 + diameter / 2; x++)
        {
            for (int y = y0 - diameter / 2; y <= y0 + diameter / 2; y++)
            {
                if (x >= 0 && x < width && y >= 0 && y < height)
                {
                    texture.SetPixel(x, y, color);
                }
            }
        }
    }

    void DrawRotatedSquare(Texture2D texture, Vector2 center, float size, float angle, Color color1, Color color2)
    {
        Vector2 halfSize = Vector2.one * (size / 2);
        Vector2[] corners = new Vector2[4];

        // Define each corner of the square relative to the center
        corners[0] = RotatePoint(center, center - halfSize, angle);
        corners[1] = RotatePoint(center, center + new Vector2(halfSize.x, -halfSize.y), angle);
        corners[2] = RotatePoint(center, center + halfSize, angle);
        corners[3] = RotatePoint(center, center + new Vector2(-halfSize.x, halfSize.y), angle);

        // Colors for the lines, adjacent same color
        Color[] lineColors = { color2, color2, color1, color1 };

        // Draw lines between each corner point
        for (int i = 0; i < 4; i++)
        {
            DrawLine(texture, corners[i], corners[(i + 1) % 4], lineColors[i]);
        }
    }



    void DrawSquareMesh(Vector2 center, float size, float angle)
    {
        GameObject square = new GameObject("Rotated Square");
        MeshFilter meshFilter = square.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = square.AddComponent<MeshRenderer>();
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        int[] triangles = { 0, 1, 2, 0, 2, 3 };  // Two triangles forming a square

        // Calculate rotated vertices
        float halfSize = size / 2;
        Vector2[] corners = new Vector2[]
        {
            new Vector2(-halfSize, -halfSize),
            new Vector2(halfSize, -halfSize),
            new Vector2(halfSize, halfSize),
            new Vector2(-halfSize, halfSize)
        };

        for (int i = 0; i < 4; i++)
        {
            corners[i] = RotatePoint(center, corners[i], angle);
            vertices[i] = new Vector3(corners[i].x, corners[i].y, 0);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
        meshRenderer.material = new Material(Shader.Find("Standard"));
    }

    Vector2 RotatePoint(Vector2 pivot, Vector2 point, float angle)
    {
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);
        Vector2 dir = point - pivot;
        Vector2 rotated = new Vector2(
            dir.x * cos - dir.y * sin,
            dir.x * sin + dir.y * cos
        ) + pivot;
        return rotated;
    }


    void DrawLine(Texture2D texture, Vector2 start, Vector2 end, Color color, int thickness = 1)
    {
        int x0 = (int)start.x;
        int y0 = (int)start.y;
        int x1 = (int)end.x;
        int y1 = (int)end.y;

        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        float err = dx - dy;
        float e2;

        while (true)
        {
            // Set the main pixel
            texture.SetPixel(x0, y0, color);

            // Basic antialiasing by manipulating the alpha value of adjacent pixels
            if (x0 + sx >= 0 && x0 + sx < texture.width && y0 >= 0 && y0 < texture.height)
                texture.SetPixel(x0 + sx, y0, new Color(color.r, color.g, color.b, 0.5f));
            if (x0 >= 0 && x0 < texture.width && y0 + sy >= 0 && y0 + sy < texture.height)
                texture.SetPixel(x0, y0 + sy, new Color(color.r, color.g, color.b, 0.5f));

            if (x0 == x1 && y0 == y1) break;
            e2 = 2 * err;
            if (e2 > -dy) { err -= dy; x0 += sx; }
            if (e2 < dx) { err += dx; y0 += sy; }
        }
        texture.Apply();
    }

    private Color GrayscaleToColor(float grayscale)
    {
        return new Color(grayscale, grayscale, grayscale, 1.0f); // Assuming fully opaque
    }

    public override float GetCurrentRatio()
    {
        Debug.LogWarning("ForegroundColor: " + foregroundColor + " BackgroundColor: " + backgroundColor);
        if (foregroundColor == 0)
        {
            return 10f;
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

        if (backgroundColor > foregroundColor)
        {
            foregroundColor += stepSize;
            backgroundColor -= stepSize;
        }
        else
        {
            foregroundColor = backgroundColor = 0.5f;
        }

        texture = GeneratePattern();
    }

    public override void IncreasePatternRatio()
    {
        if (foregroundColor - stepSize >= 0.0f)
        {
            backgroundColor += stepSize;
            foregroundColor -= stepSize;
        }
        texture = GeneratePattern();
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
