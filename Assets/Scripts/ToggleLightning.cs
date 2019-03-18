using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLightning : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------
    [SerializeField] private GameObject lightning; // for background decoration
    [SerializeField] private GameObject blueflare; // for background decoration
    [SerializeField] private int frameIntervalLightning = 3;
    [SerializeField] private float shineDuration = 0.1f;

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {
        // Register the beat callback function
        GetComponent<BeatDetection>().CallBackFunction = MyCallbackEventHandler;
    }

    void Update() {
        Debug.Log(AudioHelper.amplitudeBuffer);

        // run this spawn function every certain frames (defined in inspector)
        if (Time.frameCount % frameIntervalLightning == 0) {
            if (AudioHelper.amplitudeBuffer > 0.5) {
                Debug.Log("Lightning on");
                LightningOn();
                BlueflareOn();
            }
        }
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    public void MyCallbackEventHandler(BeatDetection.EventInfo eventInfo) {
        switch (eventInfo.messageInfo) {
            case BeatDetection.EventType.Energy: // low freq, high amp

                break;
            case BeatDetection.EventType.HitHat: // high freq
                Debug.Log("hit hat");
                LightningOn();
                break;
            case BeatDetection.EventType.Kick:

                break;
            case BeatDetection.EventType.Snare:
                break;
        }
    }

    // active lighting object
    void LightningOn() {
        if (lightning.activeSelf) {
            return;
        }
        lightning.SetActive(true);
        Invoke("LightningOff", shineDuration);
    }

    void LightningOff() {
        lightning.SetActive(false);
    }

    // active lighting object
    void BlueflareOn() {
        if (blueflare.activeSelf)
            return;
        blueflare.SetActive(true);
        Invoke("BlueflareOff", shineDuration);
    }

    void BlueflareOff() {
        blueflare.SetActive(false);
    }
}