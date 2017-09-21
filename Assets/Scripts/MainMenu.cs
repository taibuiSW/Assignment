using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public GameObject helpDialog;

    private DialogController dialog;

	// Use this for initialization
	void Start () {
        if (Application.platform == RuntimePlatform.Android) {
            Screen.orientation = ScreenOrientation.Landscape;
        }

        helpDialog.SetActive(false);
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

    public void ShowHelp() {
        helpDialog.SetActive(true);
    }

    public void CloseHelp() {
        helpDialog.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
