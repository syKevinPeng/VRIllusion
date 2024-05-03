using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
// This class is used to manage the controller of your VR equipment


public class IllusionCanvasController : MonoBehaviour
{
    private OVRInput.Controller LController = OVRInput.Controller.LTouch;
    private OVRInput.Controller RController = OVRInput.Controller.RTouch;
    // raise exception if Quad is not found
    private GameObject RawImage;
    private GameObject Slider;
    private GameObject UpButton;
    private GameObject DownButton;
    private GameObject ConfirmButton;

    // Start is called before the first frame update
    void GetIncreaseButtonPressed()
    {
        if (OVRInput.Get(OVRInput.Button.Two, RController))
        {
            // increase the ratio of the pattern. Call IncreasePatternRatio() in ouchi.cs
            RawImage.GetComponent<IllusionPatternLoader>().IncreasePatternRatio();
            UpButton.GetComponent<UnityEngine.UI.Button>().image.sprite = Resources.Load<Sprite>("Image/uparrow_blue");
            AdjustSliderWithValue(GetCurrentRatio());
        }
        if (OVRInput.GetUp(OVRInput.Button.Two, RController))
        {
            UpButton.GetComponent<UnityEngine.UI.Button>().image.sprite = Resources.Load<Sprite>("Image/uparrow");
        }
    }

    void GetDecreaseButtonPressed()
    {
        if (OVRInput.Get(OVRInput.Button.One, RController))
        {
            // decrease the ratio of the pattern. Call DecreasePatternRatio() in ouchi.cs
            RawImage.GetComponent<IllusionPatternLoader>().DecreasePatternRatio();
            DownButton.GetComponent<UnityEngine.UI.Button>().image.sprite = Resources.Load<Sprite>("Image/downarrow_blue");
            AdjustSliderWithValue(GetCurrentRatio());
        }
        if (OVRInput.GetUp(OVRInput.Button.One, RController))
        {
            DownButton.GetComponent<UnityEngine.UI.Button>().image.sprite = Resources.Load<Sprite>("Image/downarrow");
        }
    }

    private void AdjustSliderWithValue(float value)
    {
        Slider.GetComponent<UnityEngine.UI.Slider>().value = value;
    }
    public void HideCanvas()
    {
        // gameObject.SetActive(false);
    }

    public void ShowCanvas()
    {
        gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3;
    }

    public float GetCurrentRatio()
    {
        // get the current ratio of the pattern
        return RawImage.GetComponent<IllusionPatternLoader>().GetCurrentRatio();
    }

    public float GetInitRatio()
    {
        // get the initial ratio of the pattern
        return RawImage.GetComponent<IllusionPatternLoader>().GetInitRatio();
    }

    private float GetMinRatio()
    {
        return RawImage.GetComponent<IllusionPatternLoader>().GetMinRatio();
    }

    private float GetMaxRatio()
    {
        return RawImage.GetComponent<IllusionPatternLoader>().GetMaxRatio();
    }

    private void UpdateCanvasPosition()
    {
        Vector3 newPosition = Camera.main.transform.position;
        float distance = 3.5f; // Adjust the distance as needed
        newPosition.z += distance;

        gameObject.transform.position = newPosition;
    }

    public void OnClick()
    {
        Debug.Log("Confirm Button Clicked");
        abstractIllusionPattern NextPattern = RawImage.GetComponent<IllusionPatternLoader>().GetNextPattern();
        if (NextPattern == null)
        {
            Debug.Log("No More Patterns. Moving to the next scene.");
            GameObject.Find("TimelineController").GetComponent<TimelineController>().GetNextScene();
        }

    }



    void Start()
    {
        RawImage = GameObject.Find("RawImage");
        UpButton = GameObject.Find("UpButton");
        DownButton = GameObject.Find("DownButton");
        Slider = GameObject.Find("Slider");
        ConfirmButton = GameObject.Find("ConfirmButton");
        Debug.Log("Current Ratio: " + GetCurrentRatio() + " Init Ratio: " + GetInitRatio());
        // Config the slider
        Slider.GetComponent<UnityEngine.UI.Slider>().maxValue = GetMaxRatio();
        Slider.GetComponent<UnityEngine.UI.Slider>().minValue = GetMinRatio();
        Slider.GetComponent<UnityEngine.UI.Slider>().value = GetCurrentRatio();
        Slider.GetComponent<UnityEngine.UI.Slider>().direction = UnityEngine.UI.Slider.Direction.RightToLeft;

        // intialize canvas position
        UpdateCanvasPosition();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCanvasPosition();
        if (RawImage != null)
        {
            GetIncreaseButtonPressed();
            GetDecreaseButtonPressed();
        }

    }
}
