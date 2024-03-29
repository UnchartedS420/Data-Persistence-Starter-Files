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

            string jsonScore = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(jsonScore);
            
            score = data.score;
            playerName = data.playerName;
        }
    }

}
