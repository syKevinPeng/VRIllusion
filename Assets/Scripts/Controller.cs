using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to manage the controller of your VR equipment


public class Controller : MonoBehaviour
{
    private OVRInput.Controller LController = OVRInput.Controller.LTouch;
    private OVRInput.Controller RController = OVRInput.Controller.RTouch;
    // raise exception if Quad is not found
    private GameObject Quad;
    // Start is called before the first frame update
    void GetIncreaseButtonPressed()
    {
        if (OVRInput.Get(OVRInput.Button.One, LController))
        {
            Debug.Log(" ===== Button One is pressed =====");
            // increase the ratio of the pattern. Call IncreasePatternRatio() in ouchi.cs
            Quad.GetComponent<ouchi>().IncreasePatternRatio();
        }
    }

    void GetDecreaseButtonPressed()
    {
        if (OVRInput.Get(OVRInput.Button.Two, LController))
        {
            Debug.Log(" ===== Button Two is pressed =====");
            // decrease the ratio of the pattern. Call DecreasePatternRatio() in ouchi.cs
            Quad.GetComponent<ouchi>().DecreasePatternRatio();
        }
    }


    void Start()
    {
        Quad = GameObject.Find("Quad");
    }

    // Update is called once per frame
    void Update()
    {
        if (Quad != null)
        {
            GetIncreaseButtonPressed();
            GetDecreaseButtonPressed();
        }
    }
}
