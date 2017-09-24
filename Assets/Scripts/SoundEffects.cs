using UnityEngine;

public class SoundEffects : MonoBehaviour {
    public AudioSource sceneMusic;
    public AudioSource playerJump;
    public AudioSource playerHurt;
    public AudioSource playerDie;
    public AudioSource healthPickup;
    public AudioSource coinPickup;
    public AudioSource stompOnEnemy;
    public AudioSource pause;
    public AudioSource gameOver;
    public AudioSource levelEnd;

    private AudioSource[] soundEffects;

    // Use this for initialization
    void Start () {
        soundEffects = new AudioSource[] {
            playerJump, playerHurt, playerDie, healthPickup,
            coinPickup, stompOnEnemy, pause, gameOver, levelEnd
        };

        MusicSwitch(GetConfig("music"));
        SoundSwitch(GetConfig("sound"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private bool GetConfig(string key) {
        bool isMute = false;
        if (PlayerPrefs.HasKey(key)) {
            isMute = (PlayerPrefs.GetInt(key) == 0);
        }
        return isMute;
    }

    private void MusicSwitch(bool boolean) {
        sceneMusic.mute = boolean;
    }

    private void SoundSwitch(bool boolean) {
        foreach (AudioSource audio in soundEffects) {
            audio.mute = boolean;
        }
    }
    
}
