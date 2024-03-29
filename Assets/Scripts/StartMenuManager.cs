using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{

    public TextMeshProUGUI playerText;
    public Button StartGameButton;
    public Button QuitGameButton;

    public void StartGame() {
        SceneDataCarrier.playerName = playerText.text;
        SceneManager.LoadScene(1);
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
