using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMgr : MonoBehaviour {
    public int maxHitpoint = 6;
    public int startLife = 2;
    public float timeTillRespawn;
    public GameObject deathEffect;
    public GameObject fadeInEffect;
    public Text textScore;
    public Text textLife;
    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Transform groundCheck;
    public ContextScreen contextScreen;
    public GameObject scoreEffect;
    public Sprite mysticBoxEmpty;
    public SoundEffects soundEffects;

    private PlayerController playerCtrl;
    private int hitpoint;
    private int life;
    private int playerScore;
    private Image[] hearts;

    // Use this for initialization
    void Start() {
        fadeInEffect.SetActive(true);
        playerCtrl = FindObjectOfType<PlayerController>();
        LoadPlayerData();
        hitpoint = maxHitpoint;
        textLife.text = "x " + life;
        hearts = new Image[] { heart1, heart2, heart3 };
        UpdateHealthBar();
        UpdateScore();
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
        Instantiate(deathEffect, playerCtrl.gameObject.transform.position, playerCtrl.gameObject.transform.rotation);
        SavePlayerData();
        soundEffects.sceneMusic.Stop();
        if (life < 0) {
            GameOver();
            soundEffects.gameOver.Play();
        } else {
            soundEffects.playerDie.Play();
            StartCoroutine("RespawnPause");
        }
    }

    private IEnumerator RespawnPause() {
        yield return new WaitForSeconds(timeTillRespawn);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SavePlayerData() {
        PlayerPrefs.SetInt("currentLife", life);
        PlayerPrefs.SetInt("currentScore", playerScore);
    }

    private void LoadPlayerData() {
        if (PlayerPrefs.HasKey("currentLife")) {
            life = PlayerPrefs.GetInt("currentLife");
            playerScore = PlayerPrefs.GetInt("currentScore");
        } else {
            life = startLife;
        }

        if (life < 0) {
            life = startLife;
            playerScore = 0;
        }
    }

    public void GameOver() {
        //Text highScoreText = contextScreen.highScore.GetComponent<Text>();
        //if (PlayerPrefs.HasKey("highScore")) {
        //    int highScore = PlayerPrefs.GetInt("highScore");
        //    if (playerScore > highScore) {
        //        highScoreText.text = "new record\n" + playerScore;
        //        PlayerPrefs.SetInt("highScore", playerScore);
        //    } else {
        //        highScoreText.text = "score: " + playerScore + "\nbest: " + highScore;
        //    }
        //} else {
        //    highScoreText.text = "new record\n" + playerScore;
        //    PlayerPrefs.SetInt("highScore", playerScore);
        //}
        //contextScreen.ShowGameOverScreen();

        contextScreen.ShowGameOverScreen();
        Text highScoreText = contextScreen.highScore.GetComponent<Text>();
        highScoreText.text = "New record\n" + playerScore;
        if (PlayerPrefs.HasKey("highScore")) {
            int highScore = PlayerPrefs.GetInt("highScore");
            if (playerScore <= highScore) {
                highScoreText.text = "Score: " + playerScore + "\nBest: " + highScore;
                return;
            }
        }
        PlayerPrefs.SetInt("highScore", playerScore);
    }

    public void AddScore(int scoreToAdd, Transform parent) {
        playerScore += scoreToAdd;
        UpdateScore();
        GameObject theEffect = Instantiate(scoreEffect, parent.position, parent.rotation);
        theEffect.GetComponent<ScoreEffectController>().scoreText.text = "+" + scoreToAdd;
    }

    public void DamagePlayer(int damage, int knockBackDirection) {
        if (!playerCtrl.IsInvulnerable()) {
            hitpoint -= damage;
            soundEffects.playerHurt.Play();
            UpdateHealthBar();
            playerCtrl.KnockBack(knockBackDirection);
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

    private void UpdateScore() {
        textScore.text = "" + playerScore;
    }

    public void GetExtraHeart() {
        hitpoint += 2;
        if (hitpoint > maxHitpoint) {
            hitpoint = maxHitpoint;
        }
        UpdateHealthBar();
    }
}
