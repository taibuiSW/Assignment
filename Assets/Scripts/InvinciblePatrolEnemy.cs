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
    
    virtual protected void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            int knockBackDirection = (other.transform.position.x > gameObject.transform.position.x ? 1 : -1);
            levelMgr.DamagePlayer(damage, knockBackDirection);
        }
    }
}
