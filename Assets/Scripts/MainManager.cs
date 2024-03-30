using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public GameObject LevelCompletedText;
    public Text NameBestScore;
    public Button deleteHighScoresButton;
   
    private bool m_Started = false;
    public int m_Points;
    public string playerName;
    private bool m_GameOver = false;
    
    void Awake() {
        playerName = SceneDataCarrier.playerName;
        DataToJSON.Instance.Load();
    }
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        if (DataToJSON.Instance.highScore.highScores.Count == 0) {
            NameBestScore.text = "Best Score: " + " " + playerName + " " + m_Points;
        }else{
            NameBestScore.text = "Best Score: " + " " + DataToJSON.Instance.highScore.highScores[0].playerName + " " + DataToJSON.Instance.highScore.highScores[0].playerScore;
        }
        deleteHighScoresButton.onClick.AddListener(DataToJSON.Instance.DeleteJsonFile);
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                LevelCompletedText.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (GameObject.FindGameObjectWithTag("Brick") == null && !m_GameOver) {
            LevelCompletedText.SetActive(true);
            Destroy(GameObject.Find("Ball"));
            m_GameOver = true;
            GameOver();
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {  
        m_GameOver = true;
        GameOverText.SetActive(true);
        DataToJSON.Instance.Save();
        DataToJSON.Instance.Load();
        NameBestScore.text = "Best Score: " + " " + DataToJSON.Instance.highScore.highScores[0].playerName + " " + DataToJSON.Instance.highScore.highScores[0].playerScore;
    }
}
