using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenu : MonoBehaviour
{
    bool alphaSwitch = false;

    void Awake() {
        GameObject.Find("Backdrop").GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find("Backdrop").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("Backdrop").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("MenuObjects").GetComponent<CanvasGroup>().alpha = 0f;
        GameObject.Find("MenuObjects").GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject.Find("MenuObjects").GetComponent<CanvasGroup>().interactable = false;
        Time.timeScale = 1.0f;
    }
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape)) {
        alphaSwitch = !alphaSwitch;
            if (!alphaSwitch) {
                GameObject.Find("Backdrop").GetComponent<CanvasGroup>().alpha = 0;
                GameObject.Find("Backdrop").GetComponent<CanvasGroup>().blocksRaycasts = false;
                GameObject.Find("Backdrop").GetComponent<CanvasGroup>().interactable = false;
                GameObject.Find("MenuObjects").GetComponent<CanvasGroup>().alpha = 0f;
                GameObject.Find("MenuObjects").GetComponent<CanvasGroup>().blocksRaycasts = false;
                GameObject.Find("MenuObjects").GetComponent<CanvasGroup>().interactable = false;
                Time.timeScale = 1.0f;
            }

            if (alphaSwitch) {
                GameObject.Find("Backdrop").GetComponent<CanvasGroup>().alpha = 0.6f;
                GameObject.Find("Backdrop").GetComponent<CanvasGroup>().blocksRaycasts = true;
                GameObject.Find("Backdrop").GetComponent<CanvasGroup>().interactable = true;
                GameObject.Find("MenuObjects").GetComponent<CanvasGroup>().alpha = 1.0f;
                GameObject.Find("MenuObjects").GetComponent<CanvasGroup>().blocksRaycasts = true;
                GameObject.Find("MenuObjects").GetComponent<CanvasGroup>().interactable = true;
                Time.timeScale = 0.0f;
            }    
        } 
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
