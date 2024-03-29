using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataToJSON : MonoBehaviour
{
    public static DataToJSON Instance;
    void Awake() {
        Instance = this;
    }
    
    public int score;
    public string playerName;

    [System.Serializable]
    public class SaveData {
        public int score;
        public string playerName;
    }

    //JSON savefile location: C:\Users\bmgib\AppData\LocalLow\DefaultCompany\SimpleBreakout
    public void Save() {
        SaveData data = new SaveData();

        data.score = score;
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load() {

        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path)) {

            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            if (data.playerName == SceneDataCarrier.playerName) {
                playerName = SceneDataCarrier.playerName;
                score = data.score;
            } 
            else if(data.playerName != SceneDataCarrier.playerName) {
                playerName = SceneDataCarrier.playerName;
                Save();
            }


            
            //playerName = data.playerName;
        }
    }

}
