using UnityEngine;
using System.Collections.Generic;
public class DataSaver
{
    private string UID;
    private Dictionary<string, float> illusionScore = new Dictionary<string, float>();
    private string currentScene;


    // public void SaveData()
    // {
    //     // Save the data to the file
    //     string path = Application.persistentDataPath + "/data.json";
    //     string json = JsonUtility.ToJson(this);
    //     File.WriteAllText(path, json);
    // }
    public void setUID(string UID)
    {
        this.UID = UID;
    }
    public string getUID()
    {
        return this.UID;
    }
    public void setIllusionScore(string illusionName, float score)
    {
        illusionScore[illusionName] = score;
    }
    public float getIllusionScore(string illusionName)
    {
        return illusionScore[illusionName];
    }

    public void setCurrentScene(string sceneName)
    {
        this.currentScene = sceneName;
    }
    public string getCurrentScene()
    {
        return this.currentScene;
    }
    public void SaveData()
    {
        // Save the data to the file
        string path = Application.persistentDataPath + "/data.txt";
        string recordedData = this.ToString();
        System.IO.File.AppendAllText(path, recordedData);
        Debug.LogWarning("Data Saved to " + path);
    }

    public string ToJson()
    {
        string illusionScoreString = "";
        foreach (KeyValuePair<string, float> entry in illusionScore)
        {
            illusionScoreString += entry.Key + " : " + entry.Value + "\n";
        }
        return "UID: " + UID + "\n" + "Illusion Score: \n" + illusionScoreString;

    }
}