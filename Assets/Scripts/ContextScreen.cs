﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ContextScreen : MonoBehaviour {
    public GameObject blackScreen;
    public Text titleText;
    public GameObject highScore;
    public GameObject mainItems;
    public GameObject btnResume;
    public GameObject btnRestart;

    private bool isGameOver;
    private EventSystem eventSystem;

    // Use this for initialization
    void Start () {
        eventSystem = FindObjectOfType<EventSystem>();
#if !UNITY_STANDALONE
        gameObject.transform.localScale = 1.5f * gameObject.transform.localScale;
#endif
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Pause")) {
            if (Time.timeScale == 0f) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public void PauseGame() {
        if (!isGameOver) {
            FindObjectOfType<SoundEffects>().pause.Play();
            titleText.text = "Pause";
            mainItems.SetActive(true);
            highScore.SetActive(false);
            btnResume.SetActive(true);
            Time.timeScale = 0;
            eventSystem.SetSelectedGameObject(btnResume);
            
        }
    }

    public void ResumeGame() {
        highScore.SetActive(false);
        btnResume.SetActive(false);
        mainItems.SetActive(false);
        eventSystem.SetSelectedGameObject(null);
        Time.timeScale = 1f;
    }

    public void ShowGameOverScreen() {
        isGameOver = true;
        titleText.text = "Game Over";
        mainItems.SetActive(true);
        highScore.SetActive(true); 
        btnResume.SetActive(false);
        eventSystem.SetSelectedGameObject(btnRestart);
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("currentLife", -1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
