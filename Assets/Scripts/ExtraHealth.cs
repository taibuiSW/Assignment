using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHealth : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            LevelMgr levelMgr = FindObjectOfType<LevelMgr>();
            levelMgr.GetExtraHeart();
            levelMgr.soundEffects.healthPickup.Play();
            Destroy(gameObject);
        }
    }
}
