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
    // public Button NxtButton;
    // public InteractableUnityEventWrapper interactableUnityEventWrapper;


    public void NextBtnClick()
    {
        Debug.LogWarning("<color=red>Next Button is clicked</color>");

        // interactableUnityEventWrapper.WhenSelect.Invoke();

        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 

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
            SceneManager.LoadScene("GameScene");
        }
    }

    public void doNothing() { }

    void Start()
    {
        Debug.LogWarning("Next Btn Start()");

        // WelcomePage = GameObject.Find("Menu Canvas/WelcomePage");
        // BeforeStartedPage = GameObject.Find("Menu Canvas/BeforeStartedPage");
        // InstructionPage = GameObject.Find("Menu Canvas/InstructionPage");
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
        // if (NxtButton == null)
        // {
        //     Debug.LogError("<color=red>Button is not found</color>");
        // }

        currentPage = WelcomePage;
        BeforeStartedPage.SetActive(false);
        InstructionPage.SetActive(false);



        // call NextBtnClick when the button is clicked or selected



        // NxtButton.on
        // .AddListener(NextBtnClick);
    }

    // Update is called once per frame
    void Update()
    {


    }
}
