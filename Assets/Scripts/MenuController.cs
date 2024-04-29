using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject MenuCanvas;
    private GameObject IllusionCanvas;
    private OVRInput.Controller LController = OVRInput.Controller.LTouch;
    private Boolean ToggleState;
    private void RepositionCanvas()
    {
        // Reposition the Canvas to the center of the camera
        MenuCanvas.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3;
        MenuCanvas.transform.rotation = Camera.main.transform.rotation;

    }
    public void EnableMenu()
    {
        MenuCanvas.SetActive(true);
        // hide canvas
        IllusionCanvas.SetActive(false);
        // pause the game
        Time.timeScale = 0;
    }
    public void DisableMenu()
    {
        MenuCanvas.SetActive(false);
        // show canvas
        IllusionCanvas.SetActive(true);
        // resume the game
        Time.timeScale = 1;
    }

    void GetMenuButtonPressed()
    {
        if (OVRInput.GetUp(OVRInput.Button.Start, LController))
        {
            // show the menu. Call EnableMenu() 
            EnableMenu();
        }
    }

    void GetMenuButtonReleased()
    {
        if (OVRInput.GetUp(OVRInput.Button.Start, LController))
        {

            // hide the menu. Call DisableMenu() 
            DisableMenu();
        }
    }

    public void GetToggleState()
    {
        // get the state of the toggle
        ToggleState = MenuCanvas.transform.Find("Toggle").GetComponent<UnityEngine.UI.Toggle>().isOn;
        Debug.LogError(ToggleState);
    }

    void Start()
    {
        GameObject menu = GameObject.Find("Menu");
        MenuCanvas = menu.transform.Find("MenuCanvas").gameObject;
        IllusionCanvas = GameObject.Find("IllusionCanvas");
        MenuCanvas.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        if (MenuCanvas.activeSelf)
        {
            RepositionCanvas();
        }
        if (!MenuCanvas.activeSelf)
        {
            GetMenuButtonPressed();
        }
        else
        {
            GetMenuButtonReleased();
        }

    }
}
