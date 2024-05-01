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
    public static Boolean isStationaryLoaded = false;
    public static Boolean isMovingLoaded = false;
    private GameObject timeline;

    private void LoadStationaryScene()
    {
        Scene stationaryscene = SceneManager.GetSceneByBuildIndex(1);
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
        Scene motionscene = SceneManager.GetSceneByBuildIndex(2);
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
            // if (false)
            {
                Debug.Log(" === Loading stationary scene  === ");
                LoadStationaryScene();
                isStationaryLoaded = true;
            }
            else
            {
                Debug.Log(" === Loading moving scene === ");
                LoadMovingScene();
                isMovingLoaded = true;
            }
        }
        else if (isStationaryLoaded && !isMovingLoaded)
        {
            Debug.Log(" === Then Loading moving scene === ");
            LoadMovingScene();
            isMovingLoaded = true;

        }
        else if (!isStationaryLoaded && isMovingLoaded)
        {
            Debug.Log(" === Then Loading stationary scene === ");
            LoadStationaryScene();
            isStationaryLoaded = true;
        }
        else
        {
            Debug.Log("Expereinment is over, both scenes are loaded.");
            Debug.Log(" == Total Time: " + (Time.time - startTime) + " == ");
            // quit the application
            Application.Quit();
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
        timeline = GameObject.Find("TimelineController");
        // check if this object is don't destroy on load
        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
