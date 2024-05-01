using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class ouchiLength : abstractIllusionPattern
{
    // Start is called before the first frame update
    public int width = 512;
    public int height = 512;
    public int InitPatternHeight = 8;
    public int InitPatternWidth = 32;
    public int radius = 150;
    private int CurrentPatternHeight;
    private int CurrentPatternWidth;
    private float stepSize;

    //constructor
    public ouchiLength(float stepSize = 0.1f)
    {
        texture = GeneratePattern(InitPatternHeight, InitPatternWidth);
        CurrentPatternHeight = InitPatternHeight;
        CurrentPatternWidth = InitPatternWidth;
        this.stepSize = stepSize;
    }



    public override Texture2D GeneratePattern()
    {
        texture = new Texture2D(width, height);
        // define the background pattern
        for (int h = 0; h < height; h++)
            for (int w = 0; w < width; w++)
            {
                texture.SetPixel(w, h, (w / CurrentPatternWidth % 2 == h / CurrentPatternHeight % 2) ? Color.white : Color.black);
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
                    texture.SetPixel(h, w, ((w / CurrentPatternWidth) % 2 == (h / CurrentPatternHeight) % 2) ? Color.black : Color.white);
                }
            }


        texture.filterMode = FilterMode.Point;
        texture.Apply();
        return texture;

    }

    public Texture2D GeneratePattern(int PatternHeight, int PatternWidth)
    {
        texture = new Texture2D(width, height);
        // define the background pattern
        for (int h = 0; h < height; h++)
            for (int w = 0; w < width; w++)
            {
                texture.SetPixel(w, h, (w / PatternWidth % 2 == h / PatternHeight % 2) ? Color.white : Color.black);
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
                    texture.SetPixel(h, w, ((w / PatternWidth) % 2 == (h / PatternHeight) % 2) ? Color.black : Color.white);
                }
            }


        texture.filterMode = FilterMode.Point;
        texture.Apply();
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
