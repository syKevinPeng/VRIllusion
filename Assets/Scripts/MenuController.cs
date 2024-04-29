using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject MenuCanvas;

    private void RepositionCanvas()
    {
        // Reposition the Canvas to the center of the camera
        MenuCanvas.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
        MenuCanvas.transform.rotation = Camera.main.transform.rotation;

    }
    public void EnableMenu()
    {
        MenuCanvas.SetActive(true);
        // pause the game
        Time.timeScale = 0;
    }
    public void DisableMenu()
    {
        MenuCanvas.SetActive(false);
        // resume the game
        Time.timeScale = 1;
    }


    void Start()
    {
        MenuCanvas = GameObject.Find("MenuCanvas");
        MenuCanvas.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        if (MenuCanvas.activeSelf)
        {
            RepositionCanvas();
        }

    }
}
