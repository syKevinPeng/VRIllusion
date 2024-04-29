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
    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("IllusionInMotionScene");
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            Debug.LogWarning("Loading progress: " + asyncLoad.progress);
            // if (asyncLoad.progress >= 0.9f)
            if (isMovingSceneLoaded())
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    private void LoadMovingScene()
    {
        StartCoroutine(LoadAsyncScene());
    }
    private Boolean isMovingSceneLoaded()
    {
        // check if the camera is moving
        GameObject camera = GameObject.Find("OVRCameraRigInteraction");
        return camera.transform.hasChanged;

    }
    public void GetNextScene()
    {
        if (!isStationaryLoaded && !isMovingLoaded)
        {
            // if both scenes are not loaded, randomly load one of them
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                Debug.Log("Loading stationary scene");
                LoadStationaryScene();
                isStationaryLoaded = true;
            }
            else
            {
                Debug.Log("Loading moving scene");
                LoadMovingScene();
                isMovingLoaded = true;
            }
        }
        else if (isStationaryLoaded && !isMovingLoaded)
        {
            Debug.Log("Loading moving scene");
            LoadMovingScene();
            isMovingLoaded = true;

        }
        else if (!isStationaryLoaded && isMovingLoaded)
        {
            Debug.Log("Loading stationary scene");
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
