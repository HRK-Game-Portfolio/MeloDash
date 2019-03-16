// ---------------------------------------
// Spectrum Visualizer code by Bad Raccoon
// (C)opyRight 2017/2018 By :
// Bad Raccoon / Creepy Cat / Barking Dog 
// ---------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpectrumLight : MonoBehaviour {
    public int audioChannel = 4;
    public float audioSensibility = 0.15f;
    public float intensity = 3.0f;
    public float lerpTime = 2.0f;

    private Light lt;
    private float oldIntensity;

    void Start() {
        lt = GetComponent<Light>();
        oldIntensity = lt.intensity;
    }

    void Update() {
        // If i find the beat
        if (SpectrumKernel.spects[audioChannel] * SpectrumKernel.threshold >= audioSensibility) {
            lt.intensity = SpectrumKernel.spects[audioChannel] * (intensity * SpectrumKernel.threshold);
        } else {
            // Retrieve the old intensity
            oldIntensity = Mathf.Lerp(lt.intensity, 1.0f, lerpTime * Time.deltaTime);
            lt.intensity = oldIntensity;
        }
    }
}