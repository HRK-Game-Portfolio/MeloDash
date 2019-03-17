using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPM : MonoBehaviour {
    public float bpm;
    private float beatInterval, beatTimer, beatIntervalD8, beatTimerD8;
    public static bool beatFull, beatD8;
    public static int beatCountFull, beatcountD8;

    public float[] tapTime = new float[4];
    public static int tap;
    public static bool customBeat;

    // ------------------------------------------------------
    // Cached References
    // ------------------------------------------------------

    private static BPM BPMInstance;

    // Make sure only one BPeerM class
    // If there are multiple instances, the program will destroy all others buy keep the last one
    void Awake() {
        if (BPMInstance != null && BPMInstance != this) {
            Destroy(this.gameObject);
        } else {
            BPMInstance = this;
            // Prevent from being destroyed because other scripts will reference to this script
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start() { }

    void Update() {
        BeatDetection();
        Tapping();
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    void Tapping() {
        if (Input.GetKeyUp(KeyCode.F1)) {
            customBeat = true;
            tap = 0;
        }

        if (customBeat) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (tap < 4) {
                    tapTime[tap] = Time.realtimeSinceStartup;
                    tap++;
                }
                // get the average time in between of all the different taps
                if (tap == 4) {
                    float averageTime =
                        ((tapTime[1] - tapTime[0]) +
                         (tapTime[2] - tapTime[1]) +
                         (tapTime[3] - tapTime[2])) / 3;
                    bpm = (float)System.Math.Round((double)60 / averageTime, 2);
                    tap = 0;
                    // reset beat timer
                    beatTimer = 0;
                    beatTimerD8 = 0;
                    beatCountFull = 0;
                    beatcountD8 = 0;
                    customBeat = false;
                }
            }
        }
    }

    void BeatDetection() {
        // full beat count
        beatFull = false;
        beatInterval = 60 / bpm;
        beatTimer += Time.deltaTime;
        // check if the beat timer is above or equal to beat interval
        if (beatTimer >= beatInterval) {
            beatTimer -= beatInterval;
            beatFull = true;
            beatCountFull++;
            //Debug.Log("Full");
        }

        // divided beat count
        beatD8 = false;
        beatIntervalD8 = beatInterval / 8;
        beatTimerD8 += Time.deltaTime;
        if (beatTimerD8 >= beatIntervalD8) {
            beatTimerD8 -= beatIntervalD8;
            beatD8 = true;
            beatcountD8++;
            //Debug.Log("D8");
        }
    }
}
