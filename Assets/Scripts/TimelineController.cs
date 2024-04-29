using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TimelineController : MonoBehaviour
{
    public Boolean enbaleLogger = false;
    private float startTime;
    private int UID;
    // Start is called before the first frame update
    private Boolean isStationaryLoaded = false;
    private Boolean isMovingLoaded = false;

    private void LoadStationaryScene()
    {
        SceneManager.LoadScene("IllusionStationaryScene");

    }
    private void LoadMovingScene()
    {
        SceneManager.LoadScene("IllusionMovingScene");
    }
    public void GetNextScene()
    {
        if (!isStationaryLoaded && !isMovingLoaded)
        {
            // if both scenes are not loaded, randomly load one of them
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                LoadStationaryScene();
                isStationaryLoaded = true;
            }
            else
            {
                LoadMovingScene();
                isMovingLoaded = true;
            }
        }
        else if (isStationaryLoaded && !isMovingLoaded)
        {
            LoadMovingScene();
            isMovingLoaded = true;

        }
        else if (!isStationaryLoaded && isMovingLoaded)
        {
            LoadStationaryScene();
            isStationaryLoaded = true;
        }
        else
        {
            Debug.Log("Expereinment is over, both scenes are loaded.");
            Debug.Log(" == Total Time: " + (Time.time - startTime) + " == ");
        }

    }
    void Start()
    {
        startTime = Time.time;
        // randomly generate a UID
        UID = UnityEngine.Random.Range(0, 10000);
        if (enbaleLogger)
        {
            Debug.Log(" == UID: " + UID + " == ");
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
