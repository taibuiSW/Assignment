﻿using UnityEngine;

public class PauseScreen : MonoBehaviour {
    public GameObject pauseScreen;
    public GameObject gameOverScreen;

	// Use this for initialization
	void Start () {
		
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
        if (!gameOverScreen.activeSelf) {
            FindObjectOfType<SoundEffects>().pause.Play();
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeGame() {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }
}
