using System.Collections.Generic;
using UnityEngine;

public class HighScoreBoard : MonoBehaviour
{
    public GameObject entryPrefab; 
    public int number; public int playerNameIndex; public int playerScoreIndex;
    public List<GameObject> entries = new List<GameObject>();
    private int maxList = 10; 
    private int entryPrefabSpacing = 60;
    public bool jsonIsLoaded = false;

    public void HighScoreFunctions() {
        if (jsonIsLoaded == true) {
            DestroyHighScoreEntries();
        }
            CreateHighScoreEntries();
            PopulateHighScoreEntries(); 
    }
    public void SetPopulatedVariablesToZero() {
        number = 1;
        playerNameIndex = 0;
        playerScoreIndex = 0;
    }

    public void DestroyHighScoreEntries() {
        for (int i = 0; i < maxList; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
        entries.Clear();     
    }

    private void CreateHighScoreEntries() {

        float customPosY = 0;
        Vector3 spawnEntryPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        for (int i = 0; i < maxList; i++) {
            GameObject Clone = Instantiate(entryPrefab, spawnEntryPosition + new Vector3(0, customPosY, 0), entryPrefab.transform.rotation);
            Clone.transform.SetParent(this.transform);
            customPosY -= entryPrefabSpacing;
            entries.Add(Clone);   
        }
    }

    private int SortHighScoreEntries(HighScores a, HighScores b) {

        int intScoreA;
        int intScoreB;
        int.TryParse(a.playerScore, out intScoreA);
        int.TryParse(b.playerScore, out intScoreB);

        if (intScoreA > intScoreB) {
            return -1;
        }
        else if (intScoreA < intScoreB) {
            return 1;
        }
        return 0;
    }

    private void RemoveExcessList() {

        for (int i = DataToJSON.Instance.highScore.highScores.Count - 1; i >= maxList; i--) {
            DataToJSON.Instance.highScore.highScores.RemoveAt(i);
        }
    }

    private void PopulateHighScoreEntries() {

        DataToJSON.Instance.highScore.highScores.Sort(SortHighScoreEntries);

        RemoveExcessList();

        foreach(GameObject entry in entries) {

            if (playerNameIndex < DataToJSON.Instance.highScore.highScores.Count) {
                entry.GetComponent<HighScoreEntry>().positionText.text = number.ToString();
                number++;
                entry.GetComponent<HighScoreEntry>().nameText.text = DataToJSON.Instance.highScore.highScores[playerNameIndex].playerName;
                playerNameIndex++;
                entry.GetComponent<HighScoreEntry>().scoreText.text = DataToJSON.Instance.highScore.highScores[playerScoreIndex].playerScore;
                playerScoreIndex++;
            }
        }
    }
}
