using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticBox : Brick {
    public int numberOfShakes;
    public int maxScore;
    
    private int shakesLeft;
    private LevelMgr levelMgr;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    override protected void Start () {
        base.Start();
        shakesLeft = numberOfShakes;
        levelMgr = FindObjectOfType<LevelMgr>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	override protected void Update () {
        base.Update();
	}

    override protected void OnPlayerHit() {
        base.OnPlayerHit();
        shakesLeft--;
        if (shakesLeft < 0) {
            spriteRenderer.sprite = levelMgr.mysticBoxEmpty;
            return;
        }
        int score = 10 * Random.Range(1, maxScore / 10);
        levelMgr.AddScore(score, transform);
        levelMgr.soundEffects.coinPickup.Play();
    }
}
