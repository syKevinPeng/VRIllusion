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
    public int PatternHeight;
    public int PatternWidth;
    public int radius = 150;

    void GeneratePattern()
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
        // TODO: save the texture to a file

        byte[] bytes = texture.EncodeToPNG();
        string derectoryPath = Application.dataPath + "/SavedTextures";
        string filePath = derectoryPath + "/pattern.png";
        File.WriteAllBytes(filePath, bytes);
        Debug.Log("=== Saved to " + filePath);
    }

    void Start()
    {
        GeneratePattern();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
