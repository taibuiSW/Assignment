using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject target;
	public float distanceAheadTarget;
    public bool followTarget;

    private SpriteRenderer targetRenderer;
    

	// Use this for initialization
	void Start () {
		targetRenderer = target.GetComponent<SpriteRenderer>();
        followTarget = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (followTarget) {
            int direction = (targetRenderer.flipX ? 0 : 1);

            transform.position = Vector3.Lerp(transform.position,
                new Vector3(target.transform.position.x + direction * distanceAheadTarget,
                    transform.position.y, transform.position.z),
                    2.5f * Time.deltaTime);
        }
	}
}
