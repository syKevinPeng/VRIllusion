using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public abstract class abstractIllusionPattern
{
    public GameObject RawImage;
    public Texture2D texture;

    public abstract Texture2D GeneratePattern();
    // get the current ratio of the pattern
    public abstract float GetCurrentRatio();
    // increase the ratio of the pattern by step
    public abstract void IncreasePatternRatio(float step);
    // decrease the ratio of the pattern by step
    public abstract void DecreasePatternRatio(float step);
    // get the initial ratio of the pattern
    public abstract float GetInitRatio();

}
