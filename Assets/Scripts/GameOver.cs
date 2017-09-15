using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    public GameObject pauseScreen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void RestartGame() {
        Debug.Log("RestartGame");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu() {
        Debug.Log("BackToMainMenu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
