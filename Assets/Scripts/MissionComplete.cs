using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionComplete : MonoBehaviour {
    private LevelMgr levelMgr;
    private CameraController cameraCtrl;
    private PlayerController playerCtrl;
    private bool movePlayer;
    private Animator anim;

	// Use this for initialization
	void Start () {
        levelMgr = FindObjectOfType<LevelMgr>();
        cameraCtrl = FindObjectOfType<CameraController>();
        playerCtrl = FindObjectOfType<PlayerController>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		if (movePlayer) {
            playerCtrl.MoveForward();
        }
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            anim.SetBool("isFlying", true);
            levelMgr.soundEffects.sceneMusic.Stop();
            levelMgr.soundEffects.levelEnd.Play();
            levelMgr.Bonus();
            StartCoroutine("EndScene");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            movePlayer = false;
            levelMgr.GameOver();
        }
    }

    private IEnumerator EndScene() {
        cameraCtrl.target = gameObject;
        playerCtrl.canMove = false;
        
        yield return new WaitForSeconds(2);

        movePlayer = true;
    }


}
