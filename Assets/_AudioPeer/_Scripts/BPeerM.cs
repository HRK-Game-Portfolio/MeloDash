using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPeerM : MonoBehaviour {

    public float _bpm;
    private float _beatInterval, _beatTimer, _beatIntervalD8, _beatTimerD8;
    public static bool _beatFull, _beatD8;
    public static int _beatCountFull, _beatcountD8;

    // ------------------------------------------------------
    // Cached References
    // ------------------------------------------------------

    private static BPeerM _BPeerMInstance;

    // Make sure only one BPeerM class
    // If there are multiple instances, the program will destroy all others buy keep the last one
    void Awake() {
        if (_BPeerMInstance != null && _BPeerMInstance != this) {
            Destroy(this.gameObject);
        } else {
            _BPeerMInstance = this;
            // Prevent from being destroyed because other scripts will reference to this script
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start() {

    }

    void Update() {
        BeatDetection();
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    void BeatDetection() {
        // full beat count
        _beatFull = false;
        _beatInterval = 60 / _bpm;
        _beatTimer += Time.deltaTime;
        // check if the beat timer is above or equal to beat interval
        if (_beatTimer >= _beatInterval) {
            _beatTimer -= _beatInterval;
            _beatFull = true;
            _beatCountFull++;
            Debug.Log("Full");
        }

        // divided beat count
        _beatD8 = false;
        _beatIntervalD8 = _beatInterval / 8;
        _beatTimerD8 += Time.deltaTime;
        if (_beatTimerD8 >= _beatIntervalD8) {
            _beatTimerD8 -= _beatIntervalD8;
            _beatD8 = true;
            _beatcountD8++;
            Debug.Log("D8");
        }
    }
}
