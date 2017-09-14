using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : ShuttleObject {
    public int damage;
    public int score;

    private LevelMgr levelMgr;
    private Transform playerGroundCheck;

    // Use this for initialization
    override protected void Start() {
        base.Start();
        levelMgr = FindObjectOfType<LevelMgr>();
        playerGroundCheck = levelMgr.GetGroundCheck();
    }

    // Update is called once per frame
    //void Update() {
    
    //}

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            if (playerGroundCheck.position.y > transform.position.y && other.relativeVelocity.y < 0) {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 200f));
                levelMgr.AddScore(score);
                Destroy(gameObject);
            } else {
                levelMgr.DamagePlayer(damage);
            }
        }
    }
}
