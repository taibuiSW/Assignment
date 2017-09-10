using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    public int score;

    private LevelMgr levelMgr;
	// Use this for initialization
	void Start () {
        levelMgr = FindObjectOfType<LevelMgr>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            levelMgr.AddScore(score);
            Destroy(gameObject);
        }
    }
}
