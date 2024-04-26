using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class ouchi : MonoBehaviour
{
    // Start is called before the first frame update
    private int width = 512;
    private int height = 512;
    public GameObject RawImage;
    public GameObject Slider;
    public GameObject UpButton;
    public GameObject DownButton;
    // public Material material;
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
        RawImage.GetComponent<UnityEngine.UI.RawImage>().texture = texture;

    }
    float GetCurrentRatio()
    {
        return ((float)CurrentPatternWidth) / CurrentPatternHeight;
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

    public void IncreasePatternRatio(float step = 0.01f)
    {
        // Debug.Log("current ratio: " + GetCurrentRatio() + " step: " + step);
        SetPatternRatio(GetCurrentRatio() + step);
        // Debug.Log("after set pattern, current ratio: " + GetCurrentRatio() + " step: " + step);
        GeneratePattern(CurrentPatternHeight, CurrentPatternWidth);
        AdjustSliderWithValue(GetCurrentRatio());
    }
    public void DecreasePatternRatio(float step = 0.01f)
    {
        // Debug.Log("current ratio: " + GetCurrentRatio() + " step: " + step);
        SetPatternRatio(GetCurrentRatio() - step);
        // Debug.Log("after set pattern, current ratio: " + GetCurrentRatio() + " step: " + step);
        GeneratePattern(CurrentPatternHeight, CurrentPatternWidth);
        AdjustSliderWithValue(GetCurrentRatio());
    }

    void ResetPattern()
    {
        CurrentPatternHeight = InitPatternHeight;
        CurrentPatternWidth = InitPatternWidth;
        GeneratePattern(CurrentPatternHeight, CurrentPatternWidth);
    }
    private void AdjustSliderWithValue(float value)
    {
        Slider.GetComponent<UnityEngine.UI.Slider>().value = value;
    }


    void Start()
    {
        UpButton = GameObject.Find("UpButton");
        DownButton = GameObject.Find("DownButton");
        RawImage = GameObject.Find("RawImage");
        Slider = GameObject.Find("Slider");

        // // load two Sprite objects
        // Sprite UpArrowBlue = Resources.Load<Sprite>("Image/uparrow_blue");
        // Sprite UpArrowRed = Resources.Load<Sprite>("Image/uparrow_red");

        // if (UpArrowBlue == null)
        // {
        //     Debug.LogError("UpArrowBlue is null");
        // }
        // if (UpArrowRed == null)
        // {
        //     Debug.LogError("UpArrowRed is null");
        // }

        // set the event system disabled by default
        Debug.Log("========== Ouchi Start ==========");
        GeneratePattern(InitPatternHeight, InitPatternWidth);
        CurrentPatternHeight = InitPatternHeight;
        CurrentPatternWidth = InitPatternWidth;
        Debug.LogWarning("Intial Ratio: " + GetCurrentRatio());
        // Config the slider
        Slider.GetComponent<UnityEngine.UI.Slider>().maxValue = GetCurrentRatio() * 1.2f;
        Slider.GetComponent<UnityEngine.UI.Slider>().minValue = 1.0f;
        Slider.GetComponent<UnityEngine.UI.Slider>().value = GetCurrentRatio();
        Slider.GetComponent<UnityEngine.UI.Slider>().direction = UnityEngine.UI.Slider.Direction.RightToLeft;

    }




    // Update is called once per frame
    void Update()
    {

    }
}
