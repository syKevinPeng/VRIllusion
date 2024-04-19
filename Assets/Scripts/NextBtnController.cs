using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextBtnController : MonoBehaviour
{
    public GameObject WelcomePage;
    public GameObject BeforeStartedPage;
    public GameObject InstructionPage;
    private GameObject currentPage;
    public void NextBtnClick()
    {
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
    void Start()
    {
        WelcomePage = GameObject.Find("WelcomePage");
        BeforeStartedPage = GameObject.Find("BeforeStartedPage");
        InstructionPage = GameObject.Find("InstructionPage");
        if (WelcomePage == null || BeforeStartedPage == null || InstructionPage == null)
        {
            Debug.LogError("WelcomePage or BeforeStartedPage or InstructionPage is not found");
        }

        currentPage = WelcomePage;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextBtnClick();
        }

    }
}
