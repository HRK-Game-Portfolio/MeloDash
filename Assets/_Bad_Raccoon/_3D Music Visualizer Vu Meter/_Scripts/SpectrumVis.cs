// ---------------------------------------
// Spectrum Visualizer code by Bad Raccoon
// (C)opyRight 2017/2018 By :
// Bad Raccoon / Creepy Cat / Barking Dog 
// ---------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpectrumVis : MonoBehaviour {
    public GameObject[] cubes;
    public Color barColor;
    public float sizePower = 20;

    public enum axisStrech {
        dx,
        dy,
        dz,
        dyAndDz,
        all
    };

    public axisStrech stretchAxis = axisStrech.dy;

    public enum channelColour {
        red,
        green,
        blue,
        all
    };

    public channelColour currentChannel = channelColour.red;

    private float currentRed;
    private float currentGreen;
    private float currentBlue;

    public float colorPower = 12;

    void Start() {
        currentRed = barColor.r;
        currentGreen = barColor.g;
        currentBlue = barColor.b;
    }

    void Update() {
        for (int i = 0; i < cubes.Length; i++) {
            // Save the old size
            Vector3 previousScale = cubes[i].transform.localScale;

            float powerPulse = SpectrumKernel.spects[i] * sizePower;

            // The new size
            if (stretchAxis == axisStrech.dx) {
                previousScale.x = Mathf.Lerp(previousScale.x, powerPulse, Time.deltaTime * sizePower);
            }

            if (stretchAxis == axisStrech.dy) {
                previousScale.y = Mathf.Lerp(previousScale.y, powerPulse, Time.deltaTime * sizePower);
            }

            if (stretchAxis == axisStrech.dz) {
                previousScale.z = Mathf.Lerp(previousScale.z, powerPulse, Time.deltaTime * sizePower);
            }

            if (stretchAxis == axisStrech.dyAndDz) {
                previousScale.y = Mathf.Lerp(previousScale.y, powerPulse, Time.deltaTime * sizePower);
                previousScale.z = Mathf.Lerp(previousScale.z, powerPulse, Time.deltaTime * sizePower);
            }

            if (stretchAxis == axisStrech.all) {
                previousScale.x = Mathf.Lerp(previousScale.x, powerPulse, Time.deltaTime * sizePower);
                previousScale.y = Mathf.Lerp(previousScale.y, powerPulse, Time.deltaTime * sizePower);
                previousScale.z = Mathf.Lerp(previousScale.z, powerPulse, Time.deltaTime * sizePower);
            }

            // Reset size
            cubes[i].transform.localScale = previousScale;

            // Colour change
            float colorPulse = SpectrumKernel.spects[i] * colorPower;

            if (currentChannel == channelColour.red) {
                barColor.r = currentRed + colorPulse;
            }

            if (currentChannel == channelColour.green) {
                barColor.g = currentGreen + colorPulse;
            }

            if (currentChannel == channelColour.blue) {
                barColor.b = currentBlue + colorPulse;
            }

            if (currentChannel == channelColour.all) {
                barColor.b = currentBlue + (colorPulse);
                barColor.g = currentGreen + (colorPulse);
                barColor.r = currentRed + (colorPulse);
            }

            // For standar shader
            cubes[i].GetComponent<Renderer>().material.color = barColor;

            // For particle blend shader
            cubes[i].GetComponent<Renderer>().material.SetColor("_TintColor", barColor);
        }
    }
}