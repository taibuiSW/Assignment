using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    public float jumpForce;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask groundLayer;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
	private Animator animator;
	private bool isGrounded;
	private Vector3 respawnPosition;
	private LevelMgr levelMgr;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		respawnPosition = transform.position;
		levelMgr = FindObjectOfType<LevelMgr> ();
    }

    // Update is called once per frame
    private void Update() {
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundLayer);

		if (Input.GetButtonDown("Jump") && isGrounded) {
            rigidBody.AddForce(new Vector2(0, jumpForce));
        }

		animator.SetBool ("isGrounded", isGrounded);
		animator.SetFloat ("speed", Mathf.Abs(rigidBody.velocity.x));
    }

    void FixedUpdate () {
        float direction = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(direction * moveSpeed, rigidBody.velocity.y);

        if (direction == 1f) {
            spriteRenderer.flipX = false;
        } else if (direction == -1f) {
            spriteRenderer.flipX = true;
        }
    }

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Kill Zone") {
			levelMgr.Respawn ();
		}
	}

	public Vector3 GetRespawnPosition() {
		return respawnPosition;
	}

    private void OnEnable() {
        if (spriteRenderer != null)
        spriteRenderer.flipX = false;
    }
}
