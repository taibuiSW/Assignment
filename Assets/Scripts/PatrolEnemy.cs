using System.Collections;
using UnityEngine;

public class PatrolEnemy : InvinciblePatrolEnemy {
    public int score;
    public float bounciness = 1000f;
    public float deformation = 5f;

    private bool isDead;

    override protected void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && !isDead) {
            Rigidbody2D otherRigid = other.GetComponent<Rigidbody2D>();
            if (levelMgr.groundCheck.position.y > transform.position.y && otherRigid.velocity.y < 0) {
                otherRigid.AddForce(new Vector2(0f, bounciness));
                levelMgr.AddScore(score, transform);
                isDead = true;
                canMove = false;

                // Play stomp sound
                levelMgr.soundEffects.stompOnEnemy.Play();

                // Enemy death animation
                float height = GetComponent<SpriteRenderer>().size.y;
                transform.position = new Vector3(transform.position.x,
                        transform.position.y - (height * (deformation - 1f) / (2f * deformation)),
                        transform.position.z);
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / deformation, transform.localScale.z);
                StartCoroutine("DestroyOnPause");
            } else {
                int knockBackDirection = (other.transform.position.x > transform.position.x ? 1 : -1);
                levelMgr.DamagePlayer(damage, knockBackDirection);
            }
        }
    }

    private IEnumerator DestroyOnPause() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
