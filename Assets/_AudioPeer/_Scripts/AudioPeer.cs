using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    

    // ------------------------------------------------------
    // Cached References
    // ------------------------------------------------------

    private AudioSource _audioSource;
    // static thus can be accessed from other script
    public static float[] _samples = new float[512];
    public static float[] _freqBand = new float[8]; // 8 sample ranges

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    private void GetSpectrumAudioSource() {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void MakeFrequencyBands() {
        /*
         * 22050 / 512 = 43 hertz per sample
         *
         * sub-bass:        20-60
           bass:            60-250
           low: mid-range   250-500
           mid-range:       500-2000
           upper mid-range: 2000-4000
           high:            4000-6000
           brilliance:      6000-20000
         *
         *
         *
         *
         *
         *
         *
         */
    }
}
