﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour {
    public Text txtTitle;
    public Text txtHighScore;
    public GameObject title;
    public GameObject contentHighScore;
    public GameObject contentOptions;
    public Toggle togMusic;
    public Toggle togSound;

    // Use this for initialization
    void Start () {
        title.SetActive(false);
        contentHighScore.SetActive(false);
        contentOptions.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void ShowHighScore() {
        txtTitle.text = "HIGH SCORE";
        int highScore = 0;
        if (PlayerPrefs.HasKey("highScore")) {
            highScore = PlayerPrefs.GetInt("highScore");
        }
        txtHighScore.text = "" + highScore;
        SetContent(true);
    }

    public void ShowOptions() {
        txtTitle.text = "OPTIONS";
        SetToggleButtonState("music", togMusic);
        SetToggleButtonState("sound", togSound);
        SetContent(false);
    }

    private void SetToggleButtonState(string key, Toggle tog) {
        if (PlayerPrefs.HasKey(key)) {
            tog.isOn = (PlayerPrefs.GetInt(key) == 1);
        }
    }

    public void CloseDialog() {
        gameObject.SetActive(false);
    }
    
    private void SetContent(bool isHighScore) {
        title.SetActive(true);
        contentHighScore.SetActive(isHighScore);
        contentOptions.SetActive(!isHighScore);
        gameObject.SetActive(true);
    }

    public void MusicToggle(bool isOn) {
        PlayerPrefs.SetInt("music", isOn ? 1 : 0);
    }

    public void SoundToggle(bool isOn) {
        PlayerPrefs.SetInt("sound", isOn ? 1 : 0);
    }
}