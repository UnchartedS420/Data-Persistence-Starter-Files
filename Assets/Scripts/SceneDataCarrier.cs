using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SceneDataCarrier : MonoBehaviour
{
    public static SceneDataCarrier Instance;
    public static string playerName;
    public static float volume;

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    public void VolumeChange() {
        AudioSource audioManager = GetComponent<AudioSource>();
        Slider volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        audioManager.volume = volumeSlider.value;
        volume = audioManager.volume;
    }
    
}
