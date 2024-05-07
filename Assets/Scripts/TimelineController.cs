using System;
using System.Collections;
using System.Collections.Generic;
using Meta.WitAi.Attributes;
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
    public static Boolean isBreaked = false;
    private GameObject timeline;
    DataSaver dataSaver = new DataSaver();

    private void LoadStationaryScene()
    {
        Scene stationaryscene = SceneManager.GetSceneByBuildIndex(1);
        SceneManager.LoadScene("IllusionStationaryScene");
        dataSaver.setCurrentScene("Stationary");

    }
    IEnumerator LoadAsyncScene(string sceneName = "IllusionInMotionScene")
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
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

    IEnumerator AsyncLoadBreakScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("WelcomeScene");
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
        GameObject.Find("MenuCanvas").GetComponent<WelcomeCanvasController>().SetupBreakPage();

    }

    IEnumerator AsyncLoadThankyouScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("WelcomeScene");
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
        GameObject.Find("MenuCanvas").GetComponent<WelcomeCanvasController>().SetupThankyouPage();

    }

    private void LoadMovingScene()
    {
        Scene motionscene = SceneManager.GetSceneByBuildIndex(2);
        StartCoroutine(LoadAsyncScene());
        dataSaver.setCurrentScene("Moving");
    }
    private Boolean isMovingSceneLoaded()
    {
        // check if the camera is moving
        GameObject camera = GameObject.Find("OVRCameraRigInteraction");
        return camera.transform.hasChanged;

    }

    private void LoadBreakScene()
    {
        StartCoroutine(AsyncLoadBreakScene());

    }

    private void LoadThankyouScene()
    {
        StartCoroutine(AsyncLoadThankyouScene());
        dataSaver.SaveData();
        // dataSaver.printAllData();
    }
    public void GetNextScene(string sceneName = null)
    {
        if (!isStationaryLoaded && !isMovingLoaded)
        {
            // if both scenes are not loaded, randomly load one of them
            if (sceneName == "stationary")
            // if (false)
            {
                Debug.Log(" === Loading stationary scene  === ");
                LoadStationaryScene();
                isStationaryLoaded = true;
            }
            else if (sceneName == "moving")
            {
                Debug.Log(" === Loading moving scene === ");
                LoadMovingScene();
                isMovingLoaded = true;
            }
            else
            {
                Debug.LogError(" === Invalid scene name === ");
            }
        }
        else if (!isBreaked)
        {
            // if one of the scenes is loaded, load the break scene
            LoadBreakScene();
            isBreaked = true;
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
        else if (isStationaryLoaded && isMovingLoaded && isBreaked)
        {
            LoadThankyouScene();
        }
        else
        {
            Debug.LogError(" === Something went wrong === ");
        }

    }

    public DataSaver GetDataSaver()
    {
        // get the data saver object
        return dataSaver;
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
        dataSaver.setUID(UID.ToString());
        // dataSaver.printAllData();
        timeline = GameObject.Find("TimelineController");
        // check if this object is don't destroy on load
        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Debug.LogError("Applcation is paused");
            dataSaver.SaveData();
        }
    }
}
