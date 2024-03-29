using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class DataToJSON : MonoBehaviour
{
    public static DataToJSON Instance;
    void Awake() {
        Instance = this;
            
    }
    
    private MainManager mainManager;

    public int score;
    public string playerName;


    //JSON savefile location: C:\Users\bmgib\AppData\LocalLow\DefaultCompany\SimpleBreakout
    public void Save() {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        DataToSave data = mainManager.CreateSave();

        //data.score = score;
        //data.playerName = playerName;

        string json = JsonUtility.ToJson(data);

        Debug.Log("Saving as JSON: " + json);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load() {

        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path)) {

            string json = File.ReadAllText(path);

            DataToSave data = JsonUtility.FromJson<DataToSave>(json);

            Debug.Log("Loading as JSON: " + json);

         /*/   if (data.playerName == SceneDataCarrier.playerName) {
                playerName = SceneDataCarrier.playerName;
                score = data.score;
            } 
            else if(data.playerName != SceneDataCarrier.playerName) {
                playerName = SceneDataCarrier.playerName;
                Save();
            }
            //playerName = data.playerName; /*/
        }
    }

}
