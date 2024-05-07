using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
// This class is used to load a collection of illusion patterns

public class IllusionPatternLoader : MonoBehaviour
{
    // a list of all the patterns
    public List<abstractIllusionPattern> allPatterns = new List<abstractIllusionPattern>();
    public abstractIllusionPattern currentPattern;
    private GameObject RawImage;
    private GameObject TimelineController;

    void Start()
    {
        RawImage = GameObject.Find("IllusionCanvas").transform.Find("Canvas").transform.Find("RawImage").gameObject;
        TimelineController = GameObject.Find("TimelineController");

        abstractIllusionPattern ouchiLength = new ouchiLength(stepSize: 0.1f);
        abstractIllusionPattern ouchiColor = new ouchiColor(stepSize: 0.04f);
        abstractIllusionPattern wheelLength = new wheelLength(stepSize: 0.01f);
        abstractIllusionPattern wheel1Length = new wheel1Length(stepSize: 0.1f);


        allPatterns.Add(ouchiLength);
        allPatterns.Add(ouchiColor);
        // allPatterns.Add(wheel1Length);

        currentPattern = ouchiLength;
        Debug.Log(" === Loading " + currentPattern + "  === ");
    }

    public abstractIllusionPattern GetNextPattern()
    {
        int index = allPatterns.IndexOf(currentPattern);
        if (index == allPatterns.Count - 1)
        {
            return null;
        }
        else
        {
            index++;
        }
        currentPattern = allPatterns[index];
        Debug.Log(" === Loading" + currentPattern + "  === ");
        return currentPattern;
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

    public string GetPatternName()
    {
        DataSaver dataSaver = TimelineController.GetComponent<TimelineController>().GetDataSaver();
        string currentScene = dataSaver.getCurrentScene();
        return currentScene + " " + currentPattern.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        RawImage.GetComponent<UnityEngine.UI.RawImage>().texture = currentPattern.texture;

    }
}
