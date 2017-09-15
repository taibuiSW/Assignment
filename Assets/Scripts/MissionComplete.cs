using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionComplete : MonoBehaviour {
    private LevelMgr levelMgr;
    private CameraController cameraCtrl;
    private PlayerController playerCtrl;

	// Use this for initialization
	void Start () {
        levelMgr = FindObjectOfType<LevelMgr>();
        cameraCtrl = FindObjectOfType<CameraController>();
        playerCtrl = FindObjectOfType<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Debug.Log("Hello there");
            StartCoroutine("Pause");
        }
    }

    private IEnumerator Pause() {
        cameraCtrl.followTarget = false;
        
        yield return new WaitForSeconds(2);
    }
}
