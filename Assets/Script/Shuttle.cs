using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuttle : MonoBehaviour {
    public float speed;
    public Transform start;
    public Transform end;

    private Vector3 target;

    // Use this for initialization
    void Start () {
        target = end.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position == start.position) {
            target = end.position;
        } else if (transform.position == end.position) {
            target = start.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
	}
}
