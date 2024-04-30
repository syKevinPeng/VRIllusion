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

    public abstract float GetCurrentRatio();

    public abstract float GetInitRatio();

}
