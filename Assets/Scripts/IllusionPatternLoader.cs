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

        abstractIllusionPattern ouchi = new ouchi();

        allPatterns.Add(ouchi);

        currentPattern = ouchi;
    }

    public void IncreasePatternRatio(float ratio)
    {
        currentPattern.IncreasePatternRatio(ratio);
    }
    public void DecreasePatternRatio(float ratio)
    {
        currentPattern.DecreasePatternRatio(ratio);
    }
    public float GetCurrentRatio()
    {
        return currentPattern.GetCurrentRatio();
    }
    public float GetInitRatio()
    {
        return currentPattern.GetInitRatio();
    }


    // Update is called once per frame
    void Update()
    {
        RawImage.GetComponent<UnityEngine.UI.RawImage>().texture = currentPattern.texture;

    }
}
