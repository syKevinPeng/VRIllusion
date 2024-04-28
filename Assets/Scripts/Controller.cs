using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
// This class is used to manage the controller of your VR equipment


public class Controller : MonoBehaviour
{
    private OVRInput.Controller LController = OVRInput.Controller.LTouch;
    // private OVRInput.Controller RController = OVRInput.Controller.RTouch;
    // raise exception if Quad is not found
    private GameObject RawImage;
    private GameObject UpButton;
    private GameObject DownButton;
    // Start is called before the first frame update
    void GetIncreaseButtonPressed()
    {
        if (OVRInput.Get(OVRInput.Button.Two, LController))
        {
            // increase the ratio of the pattern. Call IncreasePatternRatio() in ouchi.cs
            RawImage.GetComponent<ouchi>().IncreasePatternRatio(0.1f);
            UpButton.GetComponent<UnityEngine.UI.Button>().image.sprite = Resources.Load<Sprite>("Image/uparrow_blue");


        }
        if (OVRInput.GetUp(OVRInput.Button.Two, LController))
        {
            UpButton.GetComponent<UnityEngine.UI.Button>().image.sprite = Resources.Load<Sprite>("Image/uparrow");
        }
    }

    void GetDecreaseButtonPressed()
    {
        if (OVRInput.Get(OVRInput.Button.One, LController))
        {
            // decrease the ratio of the pattern. Call DecreasePatternRatio() in ouchi.cs
            RawImage.GetComponent<ouchi>().DecreasePatternRatio();
            DownButton.GetComponent<UnityEngine.UI.Button>().image.sprite = Resources.Load<Sprite>("Image/downarrow_blue");

        }
        if (OVRInput.GetUp(OVRInput.Button.One, LController))
        {
            DownButton.GetComponent<UnityEngine.UI.Button>().image.sprite = Resources.Load<Sprite>("Image/downarrow");
        }
    }


    void Start()
    {
        RawImage = GameObject.Find("RawImage");
        UpButton = GameObject.Find("UpButton");
        DownButton = GameObject.Find("DownButton");
    }

    // Update is called once per frame
    void Update()
    {
        if (RawImage != null)
        {
            GetIncreaseButtonPressed();
            GetDecreaseButtonPressed();
        }
    }
}
