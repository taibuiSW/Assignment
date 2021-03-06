﻿using UnityEngine;

public class ShuttleObject : MonoBehaviour {
    public float speed;
    public Transform endPoint;
    public bool flip;

    protected Vector3 startPos;
    protected Vector3 endPos;
    protected Vector3 targetPos;
    protected bool canMove;

    // Use this for initialization
    virtual protected void Start() {
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        endPos = endPoint.position;
        targetPos = endPos;
    }

    // Update is called once per frame
    protected void Update() {
        if (canMove) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if (transform.position == startPos) {
                targetPos = endPos;
                Flip();
            } else if (transform.position == endPos) {
                targetPos = startPos;
                Flip();
            }
        }
    }

    protected void OnBecameVisible() {
        canMove = true;
    }

    protected void Flip() {
        if (flip) {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}