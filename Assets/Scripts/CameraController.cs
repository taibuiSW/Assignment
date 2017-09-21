using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject target;
    public bool followTarget;
#if UNITY_STANDALONE
    public float distanceAheadTarget;

    private SpriteRenderer targetRenderer;
#endif



    // Use this for initialization
    void Start () {
#if UNITY_STANDALONE
        targetRenderer = target.GetComponent<SpriteRenderer>();
#endif
        followTarget = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (followTarget) {
#if UNITY_STANDALONE
            int direction = (targetRenderer.flipX ? 0 : 1);
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(target.transform.position.x + direction * distanceAheadTarget,
                    transform.position.y, transform.position.z),
                    2.5f * Time.deltaTime);       
#else
            transform.position = Vector3.Lerp(transform.position, 
                    new Vector3(target.transform.position.x,
                    transform.position.y, transform.position.z),
                    2.5f * Time.deltaTime);
#endif
        }
	}
}
