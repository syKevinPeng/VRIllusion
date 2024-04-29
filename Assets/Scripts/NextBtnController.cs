using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class NextBtnController : MonoBehaviour
{
    public GameObject WelcomePage;
    public GameObject BeforeStartedPage;
    public GameObject InstructionPage;
    private GameObject currentPage;
    public Button NxtButton;
    // public InteractableUnityEventWrapper interactableUnityEventWrapper;

    public Toggle BSPCheckBox1;
    public Toggle BSPCheckBox2;
    public Toggle BSPCheckBox3;
    private GameObject TimelineController;
    public void NextBtnClick()
    {
        Debug.LogWarning("Clicked Once");

        if (currentPage == WelcomePage)
        {
            currentPage.SetActive(false);
            BeforeStartedPage.SetActive(true);
            currentPage = BeforeStartedPage;
        }
        else if (currentPage == BeforeStartedPage)
        {
            currentPage.SetActive(false);
            InstructionPage.SetActive(true);
            currentPage = InstructionPage;
        }
        else if (currentPage == InstructionPage)
        {
            TimelineController.GetComponent<TimelineController>().GetNextScene();
        }

        NxtButton.interactable = false; // avoid double click
    }

    public void WhenHover()
    {
        Color hoverColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        NxtButton.GetComponent<Image>().color = hoverColor;
    }

    public void WhenUnhover()
    {
        // change back to white
        Color normalColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        NxtButton.GetComponent<Image>().color = normalColor;
    }

    void Start()
    {
        TimelineController = GameObject.Find("TimelineController");


        if (WelcomePage == null)
        {
            Debug.LogError("<color=red>WelcomePage </color>");
        }
        if (BeforeStartedPage == null)
        {
            Debug.LogError("<color=red>BeforeStartedPage is not found</color>");
        }
        if (InstructionPage == null)
        {
            Debug.LogError("<color=red>InstructionPage is not found</color>");
        }

        currentPage = WelcomePage;
        WelcomePage.SetActive(true);
        BeforeStartedPage.SetActive(false);
        InstructionPage.SetActive(false);

        if (BSPCheckBox1 == null)
            BSPCheckBox1 = GameObject.Find("ckbox1").GetComponent<Toggle>();
        if (BSPCheckBox2 == null)
            BSPCheckBox2 = GameObject.Find("ckbox2").GetComponent<Toggle>();
        if (BSPCheckBox3 == null)
            BSPCheckBox3 = GameObject.Find("ckbox3").GetComponent<Toggle>();

        if (BSPCheckBox1 == null)
        {
            Debug.LogError("<color=red>CheckBox1 is not found</color>");
        }


    }

    // Update is called once per frame
    void Update()
    {
        NxtButton.interactable = currentPage != BeforeStartedPage || (BSPCheckBox1.isOn && BSPCheckBox2.isOn && BSPCheckBox3.isOn);


    }
}
