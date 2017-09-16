using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvinciblePatrolEnemy : ShuttleObject {
    public int damage;

    protected LevelMgr levelMgr;

    // Use this for initialization
    override protected void Start () {
        base.Start();
        levelMgr = FindObjectOfType<LevelMgr>();
    }

    virtual protected void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            levelMgr.DamagePlayer(damage);
        }
    }
    
    virtual protected void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            levelMgr.DamagePlayer(damage);
        }
    }
}
