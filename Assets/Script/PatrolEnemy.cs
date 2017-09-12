using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour {
    public float speed;
    public Transform endPoint;
    public bool flip;
    public int damage;
    public int score;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 targetPosition;
    private bool canMove;
    private LevelMgr levelMgr;
    private Transform playerGroundCheck;

    // Use this for initialization
    void Start() {
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        endPosition = endPoint.position;
        targetPosition = endPosition;
        levelMgr = FindObjectOfType<LevelMgr>();
        playerGroundCheck = levelMgr.GetGroundCheck();
    }

    // Update is called once per frame
    void Update() {
        if (canMove) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (transform.position == startPosition) {
                targetPosition = endPosition;
                Flip();
            } else if (transform.position == endPosition) {
                targetPosition = startPosition;
                Flip();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            if (playerGroundCheck.position.y > transform.position.y) {
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 200f));
                levelMgr.AddScore(score);
                Destroy(gameObject);
            } else {
                levelMgr.DamagePlayer(damage);
            }
        }
    }

    private void OnBecameVisible() {
        canMove = true;
    }

    private void Flip() {
        if (flip) {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

}
