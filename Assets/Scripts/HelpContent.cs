using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpContent : MonoBehaviour {
    public Image content;
    public Sprite helpOnPC;
    public Sprite helpOnMobile;

    // Use this for initialization
#if UNITY_STANDALONE
    void Start () {
        content.sprite = helpOnPC;
    }
#else
    void Start() {
        content.sprite = helpOnMobile;
    }
#endif
}
