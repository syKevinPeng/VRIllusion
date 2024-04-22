using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ouchi : MonoBehaviour
{
    // Start is called before the first frame update
    private int width = 512;
    private int height = 512;
    public GameObject displayPlane;
    public Material material;
    private Texture2D texture;
    private int InitPatternHeight = 8;
    private int InitPatternWidth = 32;
    private int radius = 150;

    private int CurrentPatternHeight;
    private int CurrentPatternWidth;

    void GeneratePattern(int PatternHeight, int PatternWidth)
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
        material.mainTexture = texture;
        // // TODO: save the texture to a file

        // byte[] bytes = texture.EncodeToPNG();
        // string derectoryPath = Application.dataPath + "/SavedTextures";
        // string filePath = derectoryPath + "/pattern.png";
        // File.WriteAllBytes(filePath, bytes);
        // Debug.Log("=== Saved to " + filePath);
    }
    float GetCurrentRatio()
    {
        return (CurrentPatternWidth + 0f) / CurrentPatternHeight;
    }

    void SetPatternRatio(float ratio)
    {
        CurrentPatternWidth = (int)(CurrentPatternHeight * ratio);
    }

    public void IncreasePatternRatio(float step = 0.01f)
    {
        Debug.Log("current ratio: " + GetCurrentRatio() + " step: " + step);
        SetPatternRatio(GetCurrentRatio() + step);
        GeneratePattern(CurrentPatternHeight, CurrentPatternWidth);
    }
    public void DecreasePatternRatio(float step = 0.01f)
    {
        SetPatternRatio(GetCurrentRatio() - step);
        GeneratePattern(CurrentPatternHeight, CurrentPatternWidth);
    }

    void ResetPattern()
    {
        CurrentPatternHeight = InitPatternHeight;
        CurrentPatternWidth = InitPatternWidth;
        GeneratePattern(CurrentPatternHeight, CurrentPatternWidth);
    }


    void Start()
    {
        // set the event system disabled by default
        Debug.Log("========== Ouchi Start ==========");
        GeneratePattern(InitPatternHeight, InitPatternWidth);
        CurrentPatternHeight = InitPatternHeight;
        CurrentPatternWidth = InitPatternWidth;
        Debug.Log("Intial Ratio: " + GetCurrentRatio());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
