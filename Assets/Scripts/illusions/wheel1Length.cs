using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;


public class wheel1Length : abstractIllusionPattern
{
    public int width = 512;
    public int height = 512;
    public int InitPatternHeight = 512;
    public int InitPatternWidth = 512;
    public int numSquares = 30;
    public float radius = 400;
    public int dotDiameter = 8;
    public Color dotColor = Color.black;
    private int CurrentPatternWidth;
    private int CurrentPatternHeight;
    private float stepSize = 0.1f;

    public wheel1Length(float stepSize = 0.1f)
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

        Color[] colors = { Color.black, new Color(0.33f, 0.33f, 0.33f, 1), Color.white, new Color(0.66f, 0.66f, 0.66f, 1) };
        float[] proportions = { 0.15f, 0.30f, 0.15f, 0.30f };
        float blockHeight = 30f;
        int nBlocks = 60;

        float radiusMiddle = width * 0.4f;
        float radiusOuter = radiusMiddle + blockHeight;
        float radiusInner = radiusMiddle - blockHeight;

        float angleOffset = 10 * Mathf.PI / 60 / 10;

        texture.SetPixels(Enumerable.Repeat(Color.gray, width * height).ToArray());

        for (int i = 0; i < nBlocks; i++)
        {
            float theta = 2 * Mathf.PI * i / nBlocks;
            texture = DrawCircleSegment(texture, radiusInner, theta - angleOffset, blockHeight, proportions, colors);
            texture = DrawCircleSegment(texture, radiusMiddle, theta, blockHeight, proportions, colors);
            texture = DrawCircleSegment(texture, radiusOuter, theta + angleOffset, blockHeight, proportions, colors);
        }

        // Draw the central black dot
        DrawCentralDot(texture, width / 2, height / 2, 10, Color.black);  // Adjust the radius as needed

        texture.Apply();
        return texture;
    }



    void DrawCentralDot(Texture2D texture, int centerX, int centerY, float radius, Color color)
    {
        int minX = (int)(centerX - radius);
        int maxX = (int)(centerX + radius);
        int minY = (int)(centerY - radius);
        int maxY = (int)(centerY + radius);

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                if ((x - centerX) * (x - centerX) + (y - centerY) * (y - centerY) <= radius * radius)
                {
                    if (x >= 0 && x < texture.width && y >= 0 && y < texture.height)
                    {
                        texture.SetPixel(x, y, color);
                    }
                }
            }
        }
    }




    Texture2D DrawCircleSegment(Texture2D texture, float radius, float theta, float blockHeight, float[] proportions, Color[] colors)
    {
        int centerX = texture.width / 2;
        int centerY = texture.height / 2;
        float squareSide = blockHeight;  // Assuming square size equals blockHeight for simplicity

        // Calculate the top-left corner of the square
        float cornerX = centerX + (radius - squareSide / 2) * Mathf.Cos(theta) - squareSide / 2;
        float cornerY = centerY + (radius - squareSide / 2) * Mathf.Sin(theta) - squareSide / 2;

        // Total height of color proportions
        float totalHeight = 0;
        foreach (float proportion in proportions)
        {
            totalHeight += proportion;
        }

        // Iterate over each pixel within the square
        for (int y = 0; y < squareSide; y++)
        {
            // Determine color based on y and proportions
            float cumulativeHeight = 0;
            Color pixelColor = colors[0];  // Default color if something goes wrong
            float position = y / squareSide * totalHeight;

            for (int i = 0; i < proportions.Length; i++)
            {
                cumulativeHeight += proportions[i];
                if (position < cumulativeHeight)
                {
                    pixelColor = colors[i];
                    break;
                }
            }

            for (int x = 0; x < squareSide; x++)
            {
                // Rotate each point (x, y) around the center of the square to align with the radius
                float localX = x - squareSide / 2;
                float localY = y - squareSide / 2;
                float rotatedX = localX * Mathf.Cos(theta) - localY * Mathf.Sin(theta);
                float rotatedY = localX * Mathf.Sin(theta) + localY * Mathf.Cos(theta);

                // Translate back to global coordinates
                int finalX = (int)(cornerX + squareSide / 2 + rotatedX);
                int finalY = (int)(cornerY + squareSide / 2 + rotatedY);

                // Set the pixel color
                if (finalX >= 0 && finalX < texture.width && finalY >= 0 && finalY < texture.height)
                {
                    texture.SetPixel(finalX, finalY, pixelColor);
                }
            }
        }

        return texture;
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
