using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------
    public int _band;
    public float _startScale, _scaleMutiplier;

    public bool _useBuffer;

    // ------------------------------------------------------
    // Cached References
    // ------------------------------------------------------

    private Material _material;

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {
        _material = GetComponent<MeshRenderer>().materials[0];
    }

    void Update() {
        if (_useBuffer) {
            transform.localScale = new Vector3(
                transform.localScale.x,
                (AudioPeer._bandBuffer[_band] * _scaleMutiplier) + _startScale,
                transform.localScale.z);
            Color _color = new Color(
                // _audioBandBuffer will be between 0 and 1
                AudioPeer._audioBandBuffer[_band],
                AudioPeer._audioBandBuffer[_band],
                AudioPeer._audioBandBuffer[_band]);
            _material.SetColor("_EmissionColor", _color);
        } else {
            transform.localScale = new Vector3(
                transform.localScale.x,
                (AudioPeer._freqBand[_band] * _scaleMutiplier) + _startScale,
                transform.localScale.z);
            Color _color = new Color(
                AudioPeer._audioBand[_band],
                AudioPeer._audioBand[_band],
                AudioPeer._audioBand[_band]);
            _material.SetColor("_EmissionColor", _color);
        }
    }
}