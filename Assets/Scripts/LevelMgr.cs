using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMgr : MonoBehaviour {
    private static int maxHitpoint = 6;
    private static int playerScore = 0;
    private static int life = 1;

    public float timeTillRespawn;
    public GameObject explodeEffect;
    public Text textScore;
    public Text textLife;
    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Transform groundCheck;
    public GameObject gameOver;

    private PlayerController playerCtrl;
    private int hitpoint;
    private Image[] hearts;

    // Use this for initialization
    void Start() {
        playerCtrl = FindObjectOfType<PlayerController>();
        textScore.text = "Score: " + playerScore;
        textLife.text = "Life x " + life;
        hitpoint = maxHitpoint;
        hearts = new Image[] { heart1, heart2, heart3 };
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update() {

    }

    public void Respawn() {
        for (int i = 0; i < hearts.Length; i++) {
            hearts[i].sprite = heartEmpty;
        }
        life--;
        playerCtrl.gameObject.SetActive(false);
        Instantiate(explodeEffect, playerCtrl.gameObject.transform.position, playerCtrl.gameObject.transform.rotation);
        if (life < 0) {
            gameOver.SetActive(true);
        } else {
            StartCoroutine("RespawnPause");
        }
    }

    private IEnumerator RespawnPause() {
        yield return new WaitForSeconds(timeTillRespawn);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //playerCtrl.transform.position = playerCtrl.GetRespawnPosition();
        //hitpoint = maxHitpoint;
        //UpdateHealthBar();
        //playerCtrl.gameObject.SetActive(true);
    }

    public void AddScore(int scoreToAdd) {
        playerScore += scoreToAdd;
        textScore.text = "Score: " + playerScore;
    }

    public void DamagePlayer(int damage) {
        if (!playerCtrl.IsInvulnerable()) {
            hitpoint -= damage;
            UpdateHealthBar();
            playerCtrl.KnockBack();
        }
    }

    private void UpdateHealthBar() {
        if (hitpoint > 0) {
            int numberOfHeart = hitpoint / 2;
            for (int i = 0; i < hearts.Length; i++) {
                hearts[i].sprite = (i < numberOfHeart ? heartFull : heartEmpty);
            }
            if (hitpoint % 2 != 0) {
                hearts[numberOfHeart].sprite = heartHalf;
            }
        } else {
            Respawn();
        }
    }

    public Transform GetGroundCheck() {
        return groundCheck;
    }
}
