using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    public float jumpForce;
	public float groundCheckRadius;
	public LayerMask groundLayer;
    public float knockBackForce;
    public float knockBackLength;
    public GameObject background;
    public bool canMove;

    private float direction;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
	private Animator animator;
    private bool isGrounded;
	private LevelMgr levelMgr;
    private float knockBackCounter;
    private int knockBackDirection;
    private bool isHurt;
	private Vector3 atStart;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		levelMgr = FindObjectOfType<LevelMgr> ();
		atStart = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
        canMove = true;
    }

    // Update is called once per frame
    void Update() {
		isGrounded = Physics2D.OverlapCircle (levelMgr.groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded && canMove) {
            rigidBody.AddForce(new Vector2(0, jumpForce));
            levelMgr.soundEffects.playerJump.Play();
        }

		animator.SetBool ("isGrounded", isGrounded);
		animator.SetFloat ("speed", Mathf.Abs(rigidBody.velocity.x));
        animator.SetBool("isHurt", isHurt);

		background.transform.position = new Vector2 (transform.position.x, background.transform.position.y);
		float offset = transform.position.x - atStart.x;
		background.GetComponent<Renderer> ().sharedMaterial.mainTextureOffset = new Vector2 (0.05f*offset, 0f);
    }

    void FixedUpdate () {
        if (canMove) {
            if (knockBackCounter <= 0) {
                direction = Input.GetAxisRaw("Horizontal");
                rigidBody.velocity = new Vector2(direction * moveSpeed, rigidBody.velocity.y);

                if (direction == 1f) {
                    spriteRenderer.flipX = false;
                } else if (direction == -1f) {
                    spriteRenderer.flipX = true;
                }

                isHurt = false;
            } else {
                knockBackCounter -= Time.deltaTime;
                rigidBody.velocity = new Vector2(knockBackDirection * knockBackForce, knockBackForce);
            }
        }
    }

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Kill Zone") {
			levelMgr.Respawn ();
		}
	}

    private void OnEnable() {
        if (spriteRenderer != null)
        spriteRenderer.flipX = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Moving Platform") {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Moving Platform") {
            transform.parent = null;
        }
    }

    public void KnockBack(int knockBackDirection) {
        knockBackCounter = knockBackLength;
        this.knockBackDirection = knockBackDirection;
        isHurt = true;
    }

    public bool IsInvulnerable() {
        return isHurt;
    }

    public void MoveForward() {
        rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
    }

    public void StopMoving() {
        rigidBody.velocity = Vector2.zero;
    }
}
