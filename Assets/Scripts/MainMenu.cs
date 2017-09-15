using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame() {
        SceneManager.LoadScene("Scene1");
    }

    public void ShowHighScores() {
        Debug.Log("ShowHighScores");
    }

    public void ShowHelp() {
        Debug.Log("ShowHelp");
    }
    
    public void SoundToggle() {
        Debug.Log("SoundToggle");
    }

    public void QuitGame() {
        Debug.Log("QuitGame");
        Application.Quit();
    }
}
