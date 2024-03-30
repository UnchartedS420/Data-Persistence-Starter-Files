using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{

    public TMP_InputField playerText;
    public TextMeshProUGUI enterNameToContinue;
    public Button StartGameButton;
    public Button QuitGameButton;
    private float timer1 = 3f; bool startTimer;
    void  Awake() {
        //playerText.text = string.Empty;
    }
    public void StartGame() {
        if (string.IsNullOrEmpty(playerText.text)) {
            timer1 = 3f;
            startTimer = true;
        }else if(!string.IsNullOrEmpty(playerText.text)){
            SceneDataCarrier.playerName = playerText.text;
            SceneManager.LoadScene(1);
        }
    }
    private void Update() {
        if (startTimer && timer1 > 0) {
            timer1 -= Time.deltaTime;
            enterNameToContinue.gameObject.SetActive(true);
            enterNameToContinue.GetComponent<CanvasGroup>().alpha = timer1 / 3;
        }else{
            startTimer = false;
            enterNameToContinue.gameObject.SetActive(false);
        }
    }

    public void QuitGame() {
        //Enter information that needs to be saved to a JSON here.


        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
 
}
