using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class DataToJSON : MonoBehaviour
{
    public static DataToJSON Instance;
    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }else{
            Instance = this;
        }
    }

    private MainManager mainManager;
    private HighScoreBoard highScoreBoard;
    public HighScore highScore = new HighScore();


    private void TextToJsonSave() {
        highScore.name = mainManager.playerName;
        //int points;
        //int.TryParse(mainManager.m_Points, out points);
        highScore.score = mainManager.m_Points.ToString();
    }
   
    //JSON savefile location: C:\Users\bmgib\AppData\LocalLow\DefaultCompany\SimpleBreakout
    public void Save() {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();

        TextToJsonSave();

        highScore.highScores.Add(new HighScores(mainManager.playerName, mainManager.m_Points.ToString()));
        
        string highScoreData = JsonUtility.ToJson(highScore, true);

        Debug.Log("Save Successful.");

        File.WriteAllText(Application.persistentDataPath + "/BrickBreakerHighScores.json", highScoreData);
    }

    public void Load() {

        highScoreBoard = GameObject.Find("High Score Board").GetComponent<HighScoreBoard>();
        highScoreBoard.SetPopulatedVariablesToZero();

        string path = Application.persistentDataPath + "/BrickBreakerHighScores.json";

        if (File.Exists(path)) {

            string highScoreData = File.ReadAllText(path);

            highScore = JsonUtility.FromJson<HighScore>(highScoreData);

            Debug.Log("Load Successful.");
        }else{
            Debug.Log("File does not exist!");
            Debug.Log("Attempting to create file.");
            string highScoreData = JsonUtility.ToJson(highScore, true);
            File.WriteAllText(Application.persistentDataPath + "/BrickBreakerHighScores.json", highScoreData);
            Debug.Log("File creation Successful.");
        }
        highScoreBoard.HighScoreFunctions();
        highScoreBoard.jsonIsLoaded = true;
    }

    public void DeleteJsonFile() {
        if (highScoreBoard.jsonIsLoaded) {
            File.Delete(Application.persistentDataPath + "/BrickBreakerHighScores.json");
            Debug.Log("Deleted Successfully.");

            highScoreBoard.SetPopulatedVariablesToZero();
            highScoreBoard.DestroyHighScoreEntries();
            highScore.highScores.Clear();
            highScore.name = "";
            highScore.score = "";
            highScoreBoard.jsonIsLoaded = false;

        }else{
            Debug.Log("Could Not Delete File. Does not Exist.");
        }
    }
}   

[System.Serializable]
public class HighScore {

    public string name;
    public string score;
    public List<HighScores> highScores = new List<HighScores>();
}


[System.Serializable]
public class HighScores {
    public string playerName;
    public string playerScore;

    public HighScores(string playerName, string playerScore) {
        this.playerName = playerName;
        this.playerScore = playerScore;
    }
}
