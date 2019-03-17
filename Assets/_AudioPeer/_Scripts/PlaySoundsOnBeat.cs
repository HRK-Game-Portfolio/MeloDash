using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundsOnBeat : MonoBehaviour {
    public SoundManager _SoundManager;
    public AudioClip _tap, _tick;
    public AudioClip[] _strum;
    private int _randomstrum;

    void Start() {

    }

    // for every 2 beats, we want the strumming pattern to change to a different chord
    void Update() {
        if (BPeerM._beatFull) {
            _SoundManager.PlaySound(_tap, 1);
            if (BPeerM._beatCountFull % 2 == 0) {
                _randomstrum = Random.Range(0, _strum.Length);
            }
        }

        if (BPeerM._beatD8 && BPeerM._beatcountD8 % 2 == 0) {
            _SoundManager.PlaySound(_tick, 0.1f);
        }

        if (BPeerM._beatD8 && (BPeerM._beatcountD8 % 8 == 2 || BPeerM._beatcountD8 % 8 == 4)) {
            _SoundManager.PlaySound(_strum[_randomstrum], 1);
        }
    }
}
