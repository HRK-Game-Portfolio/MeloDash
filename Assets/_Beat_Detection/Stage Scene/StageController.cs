using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StageController : MonoBehaviour {
    public GameObject lightning, leftfocus, rightfocus, pinkflare, blueflare, vlight;
    public GameObject startButton;
    public Sprite focuson;
    public float duration = 0.2f;
    public List<Color> focusColors = new List<Color>();
    int currentColor = 0;

    public void MyCallbackEventHandler(BeatDetection.EventInfo eventInfo) {
        switch (eventInfo.messageInfo) {
            case BeatDetection.EventType.Energy:
                RotateColor();
                PinkflareOn();
                break;
            case BeatDetection.EventType.HitHat:
                LightningOn();
                break;
            case BeatDetection.EventType.Kick:
                VLightOn();
                break;
            case BeatDetection.EventType.Snare:
                BlueflareOn();
                break;
        }
    }

    void Start() {
        //Register the beat callback function
        GetComponent<BeatDetection>().CallBackFunction = MyCallbackEventHandler;
    }

    public void GoStage() {
        startButton.SetActive(false);
        GetComponent<AudioSource>().Play();
    }

    void RotateColor() {
        currentColor++;
        currentColor %= focusColors.Count;
        leftfocus.GetComponent<Image>().color = focusColors[currentColor];
        rightfocus.GetComponent<Image>().color = focusColors[currentColor];
    }

    void LightningOn() {
        if (lightning.activeSelf)
            return;
        lightning.SetActive(true);
        Invoke("LightningOff", duration);
    }

    void LightningOff() {
        lightning.SetActive(false);
    }

    void PinkflareOn() {
        if (pinkflare.activeSelf)
            return;
        pinkflare.SetActive(true);
        Invoke("PinkflareOff", duration);
    }

    void PinkflareOff() {
        pinkflare.SetActive(false);
    }

    void BlueflareOn() {
        if (blueflare.activeSelf)
            return;
        blueflare.SetActive(true);
        Invoke("BlueflareOff", duration);
    }

    void BlueflareOff() {
        blueflare.SetActive(false);
    }

    void VLightOn() {
        if (vlight.activeSelf)
            return;
        vlight.SetActive(true);
        Invoke("VLightOff", duration);
    }

    void VLightOff() {
        vlight.SetActive(false);
    }
}