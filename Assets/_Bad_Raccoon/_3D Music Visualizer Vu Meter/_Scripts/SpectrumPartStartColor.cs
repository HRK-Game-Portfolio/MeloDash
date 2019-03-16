// ---------------------------------------
// Spectrum Visualizer code by Bad Raccoon
// (C)opyRight 2017/2018 By :
// Bad Raccoon / Creepy Cat / Barking Dog 
// ---------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpectrumPartStartColor : MonoBehaviour {
	public ParticleSystem partEmitter;
	public int audioChannel = 3;
	public float audioSensibility = 0.1f;
	public Color beatColor = new Color(1.0f,0.0f,0.0f);
	public Color normalColor = new Color(0.5f,0.5f,0.5f);


	void Update () {
		// Get the particle setup
		ParticleSystem.MainModule tmp = partEmitter.main;

		if (SpectrumKernel.spects [audioChannel] * SpectrumKernel.threshold >= audioSensibility) {
			tmp.startColor = beatColor;
			tmp.startSize = 2;
		} else {
			tmp.startColor = normalColor;
			tmp.startSize = 1;
		}

	}
}