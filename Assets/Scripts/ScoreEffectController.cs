using UnityEngine;
using UnityEngine.UI;

public class ScoreEffectController : MonoBehaviour {
    public float vaporizingDistance;
    public float vaporizingSpeed;
    public Text scoreText;

    private Vector3 targetPos;

	// Use this for initialization
	void Start () {
        targetPos = transform.position + new Vector3(0f, vaporizingDistance, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, targetPos,
                    vaporizingSpeed * Time.deltaTime);
        if (transform.position == targetPos) {
            Destroy(gameObject);
        }
	}
}
