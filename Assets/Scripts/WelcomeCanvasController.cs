using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class WelcomeCanvasController : MonoBehaviour
{
    private GameObject MenuCanvas;
    private GameObject WelcomePage;
    private GameObject BeforeStartedPage;
    private GameObject InstructionPage;
    private GameObject AssignmentPage;
    private GameObject BreakPage;
    private GameObject ThankyouPage;
    private GameObject currentPage;
    private GameObject NxtButton;
    private Toggle BSPCheckBox1;
    private Toggle BSPCheckBox2;
    private Toggle BSPCheckBox3;
    private GameObject TimelineController;
    private Boolean startTimer = false;
    private float timeRemaining = 10; // seconds
    private string nextScene;

    private GameObject TimeText;
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
            currentPage.SetActive(false);
            AssignmentPage.SetActive(true);
            currentPage = AssignmentPage;
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                nextScene = "stationary";
            }
            else
            {
                nextScene = "moving";
            }
            UpdateAssignmentPage(nextScene);
        }
        else if (currentPage == AssignmentPage)
        {
            TimelineController.GetComponent<TimelineController>().GetNextScene(nextScene);
        }
        else if (currentPage == BreakPage)
        {
            if (nextScene == "stationary") // switch to the other scene
            {
                nextScene = "moving";
            }
            else
            {
                nextScene = "stationary";
            }
            currentPage.SetActive(false);
            AssignmentPage.SetActive(true);
            currentPage = AssignmentPage;
            UpdateAssignmentPage(nextScene);
        }
        NxtButton.GetComponent<Button>().interactable = false; // avoid double click
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

    private void UpdateAssignmentPage(string assignment)
    {
        if (assignment == "stationary")
        {
            AssignmentPage.transform.Find("assignmentText").GetComponent<TextMeshProUGUI>().text =
            "Setup 1: \n" +
            "You will experience a series of visual Illusions while remaining stationary. \n" +
            "Please adjust the illusion to the point where it is no longer perceivable.";
        }
        else if (assignment == "moving")
        {
            AssignmentPage.transform.Find("assignmentText").GetComponent<TextMeshProUGUI>().text =

            "Setup 2: \n" +
            "You will experience a series of visual Illusions while walking at a comfortable pace on the treadmill. . \n" +
            "Please adjust the illusion to the point where it is no longer perceivable." +
            "Now, please step up on the treadmill and start walking at a comfortable pace. \n";

        }
        else
        {
            Debug.LogError("Invalid assignment");
        }

    }
    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay <= 0)
        {
            TimeText.GetComponent<TextMeshProUGUI>().text = "Break Completed";
        }
        else
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            TimeText.GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void SetupBreakPage()
    {
        currentPage.SetActive(false);
        BreakPage.SetActive(true);
        currentPage = BreakPage;
        startTimer = true;
    }

    public void SetupThankyouPage()
    {
        currentPage.SetActive(false);
        ThankyouPage.SetActive(true);
        currentPage = ThankyouPage;
        NxtButton.SetActive(false);
    }

    void Start()
    {
        MenuCanvas = GameObject.Find("MenuCanvas");
        WelcomePage = MenuCanvas.transform.Find("WelcomePage").gameObject;
        BeforeStartedPage = MenuCanvas.transform.Find("BeforeStartedPage").gameObject;
        InstructionPage = MenuCanvas.transform.Find("InstructionPage").gameObject;
        AssignmentPage = MenuCanvas.transform.Find("AssignmentPage").gameObject;
        BreakPage = MenuCanvas.transform.Find("BreakPage").gameObject;
        ThankyouPage = MenuCanvas.transform.Find("ThankyouPage").gameObject;


        TimelineController = GameObject.Find("TimelineController");
        DontDestroyOnLoad(GameObject.Find("TimelineController"));
        NxtButton = MenuCanvas.transform.Find("BtnInteractable").transform.Find("NextButton").gameObject;
        BSPCheckBox1 = BeforeStartedPage.transform.Find("ckbox1").GetComponent<Toggle>();
        BSPCheckBox2 = BeforeStartedPage.transform.Find("ckbox2").GetComponent<Toggle>();
        BSPCheckBox3 = BeforeStartedPage.transform.Find("ckbox3").GetComponent<Toggle>();

        TimeText = BreakPage.transform.Find("countdown").gameObject;

        currentPage = WelcomePage;
        WelcomePage.SetActive(true);
        BeforeStartedPage.SetActive(false);
        InstructionPage.SetActive(false);
        AssignmentPage.SetActive(false);
        BreakPage.SetActive(false);
        ThankyouPage.SetActive(false);


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
        NxtButton.GetComponent<Button>().interactable = currentPage != BeforeStartedPage || (BSPCheckBox1.isOn && BSPCheckBox2.isOn && BSPCheckBox3.isOn);

        // deal with timer
        if (startTimer)
        {
            NxtButton.GetComponent<Button>().interactable = false;
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
            {
                startTimer = false;
                NxtButton.GetComponent<Button>().interactable = true;

            }
        }
        DisplayTime(timeRemaining);


    }
}
