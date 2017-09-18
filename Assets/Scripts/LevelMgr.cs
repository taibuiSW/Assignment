using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMgr : MonoBehaviour {
    private static int maxHitpoint = 6;
    private static int playerScore = 0;
    private static int life = 1;

    public float timeTillRespawn;
    public GameObject deathEffect;
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
    public GameObject scoreEffect;
    public Sprite mysticBoxEmpty;
    public SoundEffects soundEffects;

    private PlayerController playerCtrl;
    private int hitpoint;
    private Image[] hearts;

    // Use this for initialization
    void Start() {
        playerCtrl = FindObjectOfType<PlayerController>();
        UpdateScore();
        textLife.text = "x " + life;
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
        Instantiate(deathEffect, playerCtrl.gameObject.transform.position, playerCtrl.gameObject.transform.rotation);
        if (life < 0) {
            gameOver.SetActive(true);
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
