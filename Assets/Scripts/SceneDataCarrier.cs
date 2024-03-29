using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneDataCarrier : MonoBehaviour
{
    public static SceneDataCarrier Instance;

    public static string playerName;

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            Debug.Log(playerName);
        }
    }
    
}
