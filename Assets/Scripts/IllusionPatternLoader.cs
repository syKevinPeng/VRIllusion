using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This class is used to load a collection of illusion patterns

public class IllusionPatternLoader : MonoBehaviour
{
    // a list of all the patterns
    public List<abstractIllusionPattern> allPatterns = new List<abstractIllusionPattern>();
    public abstractIllusionPattern currentPattern;
    public GameObject RawImage;

    void Start()
    {
        RawImage = GameObject.Find("RawImage");

        abstractIllusionPattern ouchiLength = new ouchiLength(stepSize: 0.1f);
        abstractIllusionPattern ouchiColor = new ouchiColor(stepSize: 0.05f);

        allPatterns.Add(ouchiLength);
        allPatterns.Add(ouchiColor);

        currentPattern = ouchiColor;
    }

    public void IncreasePatternRatio()
    {
        currentPattern.IncreasePatternRatio();
    }
    public void DecreasePatternRatio()
    {
        currentPattern.DecreasePatternRatio();
    }
    public float GetCurrentRatio()
    {
        return currentPattern.GetCurrentRatio();
    }
    public float GetInitRatio()
    {
        return currentPattern.GetInitRatio();
    }
    public float GetMinRatio()
    {
        return currentPattern.GetMinRatio();
    }
    public float GetMaxRatio()
    {
        return currentPattern.GetMaxRatio();
    }


    // Update is called once per frame
    void Update()
    {
        RawImage.GetComponent<UnityEngine.UI.RawImage>().texture = currentPattern.texture;

    }
}
