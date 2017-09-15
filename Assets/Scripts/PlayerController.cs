using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    public float jumpForce;
	public float groundCheckRadius;
	public LayerMask groundLayer;
    public float knockBackForce;
    public float knockBackLength;
    public GameObject background;

    private float direction;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
	private Animator animator;
    private Transform groundCheck;
    private bool isGrounded;
	private LevelMgr levelMgr;
    private float knockBackCounter;
    private bool isHurt;
	private Vector3 atStart;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		levelMgr = FindObjectOfType<LevelMgr> ();
        groundCheck = levelMgr.GetGroundCheck();
		atStart = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    private void Update() {
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundLayer);

		if (Input.GetButtonDown("Jump") && isGrounded) {
            rigidBody.AddForce(new Vector2(0, jumpForce));
        }

		animator.SetBool ("isGrounded", isGrounded);
		animator.SetFloat ("speed", Mathf.Abs(rigidBody.velocity.x));
        animator.SetBool("isHurt", isHurt);

		background.transform.position = new Vector2 (transform.position.x, background.transform.position.y);
		float offset = transform.position.x - atStart.x;
		background.GetComponent<Renderer> ().sharedMaterial.mainTextureOffset = new Vector2 (0.05f*offset, 0f);
    }

    void FixedUpdate () {
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
            rigidBody.velocity = new Vector2(-direction * knockBackForce, knockBackForce);
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

    public void KnockBack() {
        knockBackCounter = knockBackLength;
        isHurt = true;
    }

    public bool IsInvulnerable() {
        return isHurt;
    }
}
