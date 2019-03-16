using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    

    // --------------------------------------
    // Cached References
    // --------------------------------------

    private AudioSource _audioSource;
    public float[] _samples = new float[512];

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        GetSpectrumAudioSource();
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    private void GetSpectrumAudioSource() {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }
}
