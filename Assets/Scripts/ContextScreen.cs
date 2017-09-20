using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContextScreen : MonoBehaviour {
    public GameObject blackScreen;
    public Text titleText;
    public GameObject highScore;
    public GameObject mainItems;
    public GameObject btnResume;

    private bool isGameOver;

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
        if (!isGameOver) {
            FindObjectOfType<SoundEffects>().pause.Play();
            titleText.text = "Pause";
            mainItems.SetActive(true);
            highScore.SetActive(false);
            btnResume.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeGame() {
        highScore.SetActive(false);
        btnResume.SetActive(false);
        mainItems.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowGameOverScreen() {
        isGameOver = true;
        titleText.text = "Game Over";
        mainItems.SetActive(true);
        highScore.SetActive(true); 
        btnResume.SetActive(false);
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
