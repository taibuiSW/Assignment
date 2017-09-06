using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    public float jumpForce;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
	private Animator animator;
	private float direction;
	private bool isGrounded;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update() {
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);

		if (Input.GetButtonDown("Jump") && isGrounded) {
            rigidBody.AddForce(new Vector2(0, jumpForce));
			animator.SetInteger ("State", 2);
        }

		if (isGrounded) {
			animator.SetInteger ("State", (direction == 0f ? 0 : 1));
		} else {
			animator.SetInteger ("State", 2);
		}
    }

    void FixedUpdate () {
        direction = Input.GetAxisRaw("Horizontal");
        rigidBody.velocity = new Vector2(direction * moveSpeed, rigidBody.velocity.y);

        if (direction == 1f) {
            spriteRenderer.flipX = false;
        } else if (direction == -1f) {
            spriteRenderer.flipX = true;
        }
    }
}
