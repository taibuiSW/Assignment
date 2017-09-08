using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMgr : MonoBehaviour {
	public float timeTillRespawn;

	private PlayerController playerCtrl;

	// Use this for initialization
	void Start () {
		playerCtrl = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Respawn() {
		StartCoroutine ("RespawnPause");
	}

	private IEnumerator RespawnPause() {
		playerCtrl.gameObject.SetActive (false);
		yield return new WaitForSeconds (timeTillRespawn);
		playerCtrl.transform.position = playerCtrl.GetRespawnPosition ();
		//playerCtrl.GetComponent<SpriteRenderer> ().flipX = false;
		playerCtrl.gameObject.SetActive (true);
	}
}
