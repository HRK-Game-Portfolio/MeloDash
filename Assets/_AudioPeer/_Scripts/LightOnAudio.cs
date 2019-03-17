using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightOnAudio : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    public int _band;
    public float _minIntensity, _maxIntensity;

    // ------------------------------------------------------
    // Cached References
    // ------------------------------------------------------

    private Light _light;

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {
        _light = GetComponent<Light>();
    }

    void Update() {
        _light.intensity = 
            (AudioPeer._audioBandBuffer[_band] * (_maxIntensity - _minIntensity)) + _minIntensity; // initially min intensity
    }
}
