using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BPM : MonoBehaviour {
    private Text _text;
    public BPeerM _bPeerM;

    void Start() {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        _text.text = "BPM: " + _bPeerM._bpm.ToString();
    }
}
