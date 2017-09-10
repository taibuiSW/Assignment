using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMgr : MonoBehaviour {
	public float timeTillRespawn;
    public GameObject explodeEffect;
    public Text textScore;
    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;
    public Image heart1;
    public Image heart2;
    public Image heart3;

    private PlayerController playerCtrl;
    private int playerScore;
    private int hitpoint;
    private int maxHitpoint = 6;
    private Image[] hearts;

    // Use this for initialization
    void Start () {
		playerCtrl = FindObjectOfType<PlayerController> ();
        textScore.text = "Score: " + playerScore;
        hitpoint = maxHitpoint;
        hearts = new Image[] { heart1, heart2, heart3 };
        UpdateHealthBar();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

	public void Respawn() {
		StartCoroutine ("RespawnPause");
	}

	private IEnumerator RespawnPause() {
		playerCtrl.gameObject.SetActive (false);

        Instantiate(explodeEffect, playerCtrl.gameObject.transform.position, playerCtrl.gameObject.transform.rotation);

		yield return new WaitForSeconds (timeTillRespawn);
		playerCtrl.transform.position = playerCtrl.GetRespawnPosition ();
		playerCtrl.gameObject.SetActive (true);
	}

    public void AddScore(int scoreToAdd) {
        playerScore += scoreToAdd;
        textScore.text = "Score: " + playerScore;
    }

    public void DamagePlayer(int damage) {
        hitpoint -= damage;
        UpdateHealthBar();
    }

    private void UpdateHealthBar() {
        int numberOfHeart = hitpoint / 2;
        if (numberOfHeart > 0) {
            for (int i = 0; i < numberOfHeart; i++) {
                hearts[i].sprite = heartFull;
            }
        }
        if (hitpoint % 2 != 0) {
            hearts[numberOfHeart].sprite = heartHalf;
        }
    }
}
