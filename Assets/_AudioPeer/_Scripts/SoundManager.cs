using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public int _bankSize;
    private List<AudioSource> _soundClip;

    void Start()
    {
        _soundClip = new List<AudioSource>();
        for (int i = 0; i < _bankSize; i++) {
            GameObject soundInstance = new GameObject("sound");
            soundInstance.AddComponent<AudioSource>();
            soundInstance.transform.parent = this.transform; // append to the current object
            _soundClip.Add(soundInstance.GetComponent<AudioSource>());
        }
    }

    void Update()
    {
        
    }

    public void PlaySound(AudioClip clip, float volume) {
        for (int i = 0; i < _soundClip.Count; i++) {
            if (!_soundClip[i].isPlaying) {
                _soundClip[i].clip = clip;
                _soundClip[i].volume = volume;
                _soundClip[i].Play();
                return;
            }
        }

        GameObject soundInstance = new GameObject("sound");
        soundInstance.AddComponent<AudioSource>();
        soundInstance.transform.parent = this.transform; // append to the current object
        soundInstance.GetComponent<AudioSource>().clip = clip;
        soundInstance.GetComponent<AudioSource>().volume = volume;
        soundInstance.GetComponent<AudioSource>().Play();
        _soundClip.Add(soundInstance.GetComponent<AudioSource>());
    }

    
}
