using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioHelper : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------


    // ------------------------------------------------------
    // Cached References
    // ------------------------------------------------------

    private AudioSource audioSource;

    // static thus can be accessed from other script
    public float[] samples512 = new float[512];
    public static float[] samples64 = new float[64]; // for generating background spectrum
    public static float[] freqBand = new float[8]; // 8 sample ranges
    public static float[] bandBuffer = new float[8];
    private float[] bufferDecrease = new float[8];

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    private void GetSpectrumAudioSource() {
        audioSource.GetSpectrumData(samples512, 0, FFTWindow.Blackman);
        //audioSource.GetSpectrumData(samples64, 0, FFTWindow.Blackman);
    }

    // divide the total hertz range into 8 frequency bands
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

            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7) {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++) {
                average += samples512[count] * (count + 1);
                count++;
            }

            average /= count;

            freqBand[i] = average * 10;
        }
    }

    // Buffer for Dropping
    void BandBuffer() {
        for (int g = 0; g < 8; ++g) {
            // if the frequency band is higher than the band buffer, the band buffer becomes the frequency band
            if (freqBand[g] > bandBuffer[g]) {
                bandBuffer[g] = freqBand[g];
                bufferDecrease[g] = 0.005f;
            }

            // if the frequency band is lower than the band buffer, the band buffer should decrease by a certain speed
            if (freqBand[g] < bandBuffer[g]) {
                bandBuffer[g] -= bufferDecrease[g];
                bufferDecrease[g] *= 1.2f;
            }
        }
    }
}