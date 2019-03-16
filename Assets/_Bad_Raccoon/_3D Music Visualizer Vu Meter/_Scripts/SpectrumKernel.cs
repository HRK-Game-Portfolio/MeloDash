// ---------------------------------------
// Spectrum Visualizer code by Bad Raccoon
// (C)opyRight 2017/2018 By :
// Bad Raccoon / Creepy Cat / Barking Dog 
// ---------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumKernel : MonoBehaviour {
	public static float[] spects;
	public static float threshold = 3.0f;

	// Init the arrays to the awake else this damn unity generate object relation problem... Stupid shit, wasting my time...
	void Awake () {
		spects = new float[1024];
	}

	// Allow to check only one time the channels
	void Update () {
		AudioListener.GetSpectrumData (spects, 0, FFTWindow.BlackmanHarris);
	}
}
