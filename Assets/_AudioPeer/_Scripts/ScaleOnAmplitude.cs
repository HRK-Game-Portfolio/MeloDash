using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAmplitude : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    //
    public float _startScale, _maxScale;
    //
    public bool _useBuffer;
    //
    public float _red, _green, _blue;

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
                (AudioPeer._Amplitude * _maxScale) + _startScale,
                (AudioPeer._Amplitude * _maxScale) + _startScale,
                (AudioPeer._Amplitude * _maxScale) + _startScale);
            Color _color = new Color(
                // _audioBandBuffer will be between 0 and 1
                AudioPeer._Amplitude,
                AudioPeer._Amplitude,
                AudioPeer._Amplitude);
            _material.SetColor("_EmissionColor", _color);
        } else {
            transform.localScale = new Vector3(
                (AudioPeer._AmplitudeBuffer * _maxScale) + _startScale,
                (AudioPeer._AmplitudeBuffer * _maxScale) + _startScale,
                (AudioPeer._AmplitudeBuffer * _maxScale) + _startScale);
            Color _color = new Color(
                AudioPeer._AmplitudeBuffer,
                AudioPeer._AmplitudeBuffer,
                AudioPeer._AmplitudeBuffer);
            _material.SetColor("_EmissionColor", _color);
        }
    }
}
