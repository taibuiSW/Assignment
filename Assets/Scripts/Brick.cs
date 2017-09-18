using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {
    public float shakeSpeed = 40f;
    public float shakeDistance = 0.3f;

    protected bool canShake;
    protected Vector3 startPos;
    protected Vector3 targetPos;
    protected Vector3 currentTargetPos;

    // Use this for initialization
    virtual protected void Start () {
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        targetPos = startPos + new Vector3(0f, shakeDistance);
        currentTargetPos = targetPos;
	}
	
	// Update is called once per frame
	virtual protected void Update () {
        if (canShake) {
            if (transform.position == currentTargetPos) {
                currentTargetPos = startPos;
            }
            transform.position = Vector3.Lerp(transform.position, currentTargetPos,
                    shakeSpeed * Time.deltaTime);
            if (transform.position == startPos) {
                canShake = false;
                currentTargetPos = targetPos;
            }
        }
    }

    protected void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player" 
                && other.collider.bounds.max.y < transform.position.y) {
            OnPlayerHit();
        }
    }

    virtual protected void OnPlayerHit() {
        canShake = true;
    }
}
