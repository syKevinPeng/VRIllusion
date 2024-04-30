using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionPatternLoader : MonoBehaviour
{
    // a list of all the patterns
    public List<abstractIllusionPattern> allPatterns;

    void Start()
    {
        abstractIllusionPattern ouchi = new ouchi();

        allPatterns.Add(ouchi);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
