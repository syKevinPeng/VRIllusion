using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class ouchiColor : abstractIllusionPattern
{
    private string pathToPattern = "Assets/SavedTextures/ouchi_illusion_color";
    private List<string> patternPaths = new List<string>();
    private float currentRatio;
    public ouchiColor()
    {
        if (Directory.Exists(pathToPattern))
        {
            string[] files = Directory.GetFiles(pathToPattern);
            foreach (string file in files)
            {
                if (file.EndsWith(".png"))
                {
                    patternPaths.Add(file);
                }
            }
        }
        else
        {
            Debug.LogError("No pattern found at " + pathToPattern);
        }
        // sort the list of pattern paths
        patternPaths.Sort();
        currentRatio = GetInitRatio();
        texture = GeneratePattern();
    }

    private float extractRatioFromPath(string path)
    {
        string[] segments = path.Split('\\');
        string fileName = segments[segments.Length - 1];
        string ratioString = Path.GetFileNameWithoutExtension(fileName);
        float ratio = float.Parse(ratioString);
        return ratio;
    }

    public override float GetCurrentRatio()
    {
        return currentRatio;
    }
    public override float GetMaxRatio()
    {
        String largestPath = patternPaths[patternPaths.Count - 1];
        return extractRatioFromPath(largestPath);
    }
    public override float GetMinRatio()
    {
        String smallestPath = patternPaths[0];
        return extractRatioFromPath(smallestPath);
    }
    public override float GetInitRatio()
    {
        return GetMinRatio();
    }
    public override void IncreasePatternRatio()
    {
        Debug.Log("Current ratio: " + currentRatio);
        if (currentRatio < GetMaxRatio())
        {
            int index = patternPaths.IndexOf(Path.Join(pathToPattern, (currentRatio.ToString() + ".png")));
            currentRatio = extractRatioFromPath(patternPaths[index + 1]);
            texture = GeneratePattern();

        }
    }
    public override void DecreasePatternRatio()
    {
        if (currentRatio > GetMinRatio())
        {
            int index = patternPaths.IndexOf(Path.Join(pathToPattern, currentRatio.ToString() + ".png"));
            currentRatio = extractRatioFromPath(patternPaths[index - 1]);
            texture = GeneratePattern();
        }
    }

    public override Texture2D GeneratePattern()
    {
        string path = Path.Join(pathToPattern, currentRatio.ToString() + ".png");
        byte[] fileData = File.ReadAllBytes(path);
        Texture2D new_texture = new Texture2D(2, 2);
        new_texture.LoadImage(fileData);
        return new_texture;
    }




}