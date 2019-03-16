// ---------------------------------------
// Spectrum Visualizer code by Bad Raccoon
// (C)opyRight 2017/2018 By :
// Bad Raccoon / Creepy Cat / Barking Dog 
// ---------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpectrumSize : MonoBehaviour {
	public GameObject theObject;
	public int audioChannel = 4;
	public float audioSensibility = 0.15f;
	public float scaleFactorX = 4.0f;
	public float scaleFactorY = 4.0f;
	public float scaleFactorZ = 4.0f;
	public float lerpTime = 5.0f;

	private Vector3 oldLocalScale;

	void Start(){
		oldLocalScale = theObject.transform.localScale;
	}

	void Update () {

		// If i find the beat
		if (SpectrumKernel.spects [audioChannel] * SpectrumKernel.threshold >= audioSensibility) {
			//theObject.transform.Rotate ((Vector3.forward * Random.Range(-180.0f, 180.0f)) * Time.deltaTime);
			theObject.transform.localScale = new Vector3 (scaleFactorX, scaleFactorY, scaleFactorZ);
		} else {

			// Retrieve the old position smoothly
			theObject.transform.localScale = Vector3.Lerp (theObject.transform.localScale, oldLocalScale, lerpTime * Time.deltaTime);	
		}
	}
}
