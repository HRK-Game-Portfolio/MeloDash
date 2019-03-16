// ---------------------------------------
// Spectrum Visualizer code by Bad Raccoon
// (C)opyRight 2017/2018 By :
// Bad Raccoon / Creepy Cat / Barking Dog 
// ---------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpectrumPartVelZ : MonoBehaviour {
	public ParticleSystem partEmitter;
	public int audioChannel = 3;
	public float audioSensibility = 0.1f;
	public float particleVelocity = 50.0f;

	void Update () {
		// Get the particle setup
		ParticleSystem.MainModule tmp = partEmitter.main;

		//ParticleSystem.SizeOverLifetimeModule partEmitterSol;
		ParticleSystem.VelocityOverLifetimeModule partEmitterVol;

		//partEmitterSol = partEmitter.sizeOverLifetime;
		partEmitterVol = partEmitter.velocityOverLifetime;

		// Apply modification to particles
		//float partEmitterScale = 5; //spects [audioChannel]*25.0f ;

		if (SpectrumKernel.spects [audioChannel] * SpectrumKernel.threshold >= audioSensibility) {

			partEmitterVol.zMultiplier = particleVelocity ;

		} else {

			partEmitterVol.zMultiplier = 0.0f;

		}

	}
}