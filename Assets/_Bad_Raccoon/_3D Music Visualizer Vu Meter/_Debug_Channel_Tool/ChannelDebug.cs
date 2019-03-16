// ---------------------------------------
// Spectrum Visualizer code by Bad Raccoon
// (C)opyRight 2017/2018 By :
// Bad Raccoon / Creepy Cat / Barking Dog 
// ---------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChannelDebug : MonoBehaviour {
	public GameObject theObject;
	public int audioChannel = 4;
	public float audioSensibility = 0.15f;
	public float scalefactor = 2.0f;
	public float lerpTime = 5.0f;

	private int currentChannel = 4;
	private Vector3 oldLocalScale;

	void Start(){
		oldLocalScale = theObject.transform.localScale;
		currentChannel = audioChannel;
	}
		
	void Update () {
		// New scale from the beat
		if (SpectrumKernel.spects[audioChannel] * SpectrumKernel.threshold >= audioSensibility) {
			theObject.transform.localScale = new Vector3 (scalefactor, scalefactor, scalefactor);
		}

		// Smooth retrieve scale
		theObject.transform.localScale = Vector3.Lerp(theObject.transform.localScale, oldLocalScale , lerpTime * Time.deltaTime);
	}

	void OnGUI() {
		// Draw title
		GUI.Box(new Rect(10, 10, Screen.width-20, 25), "USE THIS TOOL TO SETUP THE COMPONENTS VARS : AUDIO CHANNEL / AUDIO SENSIBILITY (Usually, only channels 0 => 20 are useful)");

		// Draw the buttons
		for(int i = 0; i < 40; i++){

			if (currentChannel == i) {
				GUI.backgroundColor = Color.red;
			} else {
				GUI.backgroundColor = Color.white;
			}
				
			if (GUI.Button (new Rect (10+ (i * (Screen.width/40)+2), Screen.height-100 , (Screen.width/40), 25), ""+i)) {
				currentChannel = i;
				audioChannel = i;
			}
		}

		// Draw the slider
		GUI.backgroundColor = Color.black;
		audioSensibility = GUI.HorizontalSlider(new Rect(15, Screen.height-60, Screen.width-30, 40), audioSensibility, 0.0F, 1.0F);

		// Draw the helper line (i know it's crappy...)
		GUI.backgroundColor = Color.white;
		GUI.Button(new Rect(10, Screen.height+6-(Screen.height/4)-(audioSensibility*(Screen.height/1.1f)), Screen.width-20, 0), "");

		// Draw infos
		GUI.backgroundColor = Color.black;
		GUI.Box(new Rect(10, Screen.height-30, Screen.width-20, 25), "AUDIO CHANNEL = "+currentChannel+" /  AROUND AUDIO SENSIBILITY VALUE = "+audioSensibility);

	}
}
