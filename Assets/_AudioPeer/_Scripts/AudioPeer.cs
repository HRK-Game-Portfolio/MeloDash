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
    public static float[] _samples    = new float[512];
    public static float[] _freqBand   = new float[8]; // 8 sample ranges
    public static float[] _bandBuffer = new float[8];
    float[] _bufferDecrease = new float[8];

    // to get the highest value of amp in each range
    float[] _freqBandHighest = new float[8];
    // value we create between 0 and 1
    public static float[] _audioBand       = new float[8];
    public static float[] _audioBandBuffer = new float[8];

    // add all the different frequency bands together into variable called Amplitude
    public static float _Amplitude, _AmplitudeBuffer;
    private float _AmplitudeHighest;

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
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
         * bass:            60-250
         * low: mid-range   250-500
         * mid-range:       500-2000
         * upper mid-range: 2000-4000
         * high:            4000-6000
         * brilliance:      6000-20000
         *
         * 0: 2   = 86
         * 1: 4   = 172   --> 87    - 258
         * 2: 8   = 344   --> 259   - 602
         * 3: 16  = 688   --> 603   - 1290
         * 4: 32  = 1376  --> 1291  - 2666
         * 5: 64  = 2752  --> 2667  - 5418
         * 6: 128 = 5504  --> 5419  - 10922
         * 7: 256 = 11008 --> 10923 - 21930
         * 510
         */

        int count = 0;

        for (int i = 0; i < 8; i++) {
            float average = 0;

            int sampleCount = (int) Mathf.Pow(2, i) * 2;

            if (i == 7) {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++) {
                average += _samples[count] * (count + 1);
                count++;
            }

            average /= count;

            _freqBand[i] = average * 10;
        }
    }

    // Buffer for Dropping
    void BandBuffer() {
        for (int g = 0; g < 8; ++g) {
            // if the frequency band is higher than the band buffer, the band buffer becomes the frequency band
            if (_freqBand[g] > _bandBuffer[g]) {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.005f;
            }

            // if the frequency band is lower than the band buffer, the band buffer should decrease by a certain speed
            if (_freqBand[g] < _bandBuffer[g]) {
                _bandBuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.2f;
            }
        }
    }

    // get value between 0 and 1, we need to divide our current value by the highest value has been played
    void CreateAudioBands() {
        for (int i = 0; i < 8; i++) {
            if (_freqBand[i] > _freqBandHighest[i]) {
                _freqBandHighest[i] = _freqBand[i];
            }

            _audioBand[i]       = (_freqBand[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
        }
    }

    // get the average amplitude into one value
    void GetAmplitude() {
        // apply temporary float variable to the amplitude
        float _CurrentAmplitude = 0;
        float _CurrentAmplitudeBuffer = 0;

        // inside for loop, apply all the different audio bands to the amplitude
        for (int i = 0; i < 8; i++) {
            _CurrentAmplitude += _audioBand[i];
            _CurrentAmplitudeBuffer += _audioBandBuffer[i];
        }

        // check if the current amplitude is higher than the highest amplitude
        if (_CurrentAmplitude > _AmplitudeHighest) {
            _AmplitudeHighest = _CurrentAmplitude;
        }

        // normalise the amplitude be dividing the current amplitude by the highest
        // obtain the amplitude of all the bands together
        _Amplitude       = _CurrentAmplitude / _AmplitudeHighest;
        _AmplitudeBuffer = _CurrentAmplitudeBuffer / _AmplitudeHighest;
    }
}