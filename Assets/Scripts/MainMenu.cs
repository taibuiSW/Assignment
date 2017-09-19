using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    private DialogController dialog;

	// Use this for initialization
	void Start () {
        dialog = FindObjectOfType<DialogController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame() {
        PlayerPrefs.SetInt("currentLife", -1);
        SceneManager.LoadScene("Scene");
    }

    public void ShowHighScores() {
        dialog.ShowHighScore();
    }

    public void ShowOptions() {
        dialog.ShowOptions();
    }

    public void QuitGame() {
        Debug.Log("QuitGame");
        Application.Quit();
    }
}
