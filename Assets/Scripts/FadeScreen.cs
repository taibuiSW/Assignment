using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour {
    public float fadeTime = 1f;

    private Image blackImage;

	// Use this for initialization
	void Start () {
        blackImage = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        blackImage.CrossFadeAlpha(0f, fadeTime, false);
        if (blackImage.canvasRenderer.GetAlpha() < 0.02f) {
            gameObject.SetActive(false);
        }
    }
}
