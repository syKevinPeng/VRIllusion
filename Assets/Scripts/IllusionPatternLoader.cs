using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
// This class is used to load a collection of illusion patterns

public class IllusionPatternLoader : MonoBehaviour
{
    // a list of all the patterns
    public List<AbstractIllusionPattern> allPatterns = new List<AbstractIllusionPattern>();
    public AbstractIllusionPattern currentPattern;
    private GameObject RawImage;
    private GameObject TimelineController;

    void Start()
    {
        RawImage = GameObject.Find("IllusionCanvas").transform.Find("Canvas").transform.Find("RawImage").gameObject;
        TimelineController = GameObject.Find("TimelineController");

        AbstractIllusionPattern ouchiColor = new OuchiColor();
        AbstractIllusionPattern ouchiLength = new OuchiLength();



        // allPatterns.Add(ouchiLength);
        allPatterns.Add(ouchiColor);
        allPatterns.Add(ouchiLength);

        currentPattern = ouchiLength;
    }

    public AbstractIllusionPattern GetNextPattern()
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
