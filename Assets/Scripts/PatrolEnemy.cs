﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : InvinciblePatrolEnemy {
    public int score;
    
    private Transform playerGroundCheck;
    private bool isDead;

    // Use this for initialization
    override protected void Start() {
        base.Start();
        playerGroundCheck = levelMgr.GetGroundCheck();
    }

    // Update is called once per frame
    //void Update() {
    
    //}

    override protected void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player" && !isDead) {
            if (playerGroundCheck.position.y > transform.position.y && other.relativeVelocity.y < 0) {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 200f));
                levelMgr.AddScore(score);
                isDead = true;
                canMove = false;
                float y = gameObject.GetComponent<SpriteRenderer>().size.y;
                transform.position = new Vector3(transform.position.x, transform.position.y - (y * 2 / 5), transform.position.z);
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 5, transform.localScale.z);
                StartCoroutine("DestroyOnPause");
            } else {
                levelMgr.DamagePlayer(damage);
            }
        }
    }

    override protected void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && !isDead) {
            Rigidbody2D otherRigid = other.GetComponent<Rigidbody2D>();
            if (playerGroundCheck.position.y > transform.position.y && otherRigid.velocity.y < 0) {
                otherRigid.AddForce(new Vector2(0f, 1000f));
                levelMgr.AddScore(score);
                isDead = true;
                canMove = false;
                float y = gameObject.GetComponent<SpriteRenderer>().size.y;
                transform.position = new Vector3(transform.position.x, transform.position.y - (y * 2 / 5), transform.position.z);
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 5, transform.localScale.z);
                StartCoroutine("DestroyOnPause");
            } else {
                levelMgr.DamagePlayer(damage);
            }
        }
    }

    private IEnumerator DestroyOnPause() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
