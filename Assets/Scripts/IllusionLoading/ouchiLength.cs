using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class OuchiLength : OuchiColor // sun zi
{

    public OuchiLength()
    {
        pathToPattern = "Assets/SavedTextures/ouchi_illusion_length";
        patternPathsList = new List<string>();
        if (Directory.Exists(pathToPattern))
        {
            string[] files = Directory.GetFiles(pathToPattern);
            foreach (string file in files)
            {
                if (file.EndsWith(".png"))
                {
                    patternPathsList.Add(file);
                }
            }
        }
        else
        {
            Debug.LogError("No pattern found at " + pathToPattern);
        }
        // sort the list of pattern paths
        patternPathsList.Sort();
        currentRatio = GetInitRatio();
        texture = GeneratePattern();

    }


}



