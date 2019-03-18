using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAmplitude : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    //
    public float startScale, maxScale;
    //
    public bool useBuffer;

    // ------------------------------------------------------
    // Cached References
    // ------------------------------------------------------

    //private Material material;

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {
        //material = GetComponent<MeshRenderer>().materials[0];
    }

    void Update() {
        if (useBuffer) {
            transform.localScale = new Vector3(
                (AudioHelper.amplitude * maxScale) + startScale,
                (AudioHelper.amplitude * maxScale) + startScale,
                (AudioHelper.amplitude * maxScale) + startScale);
        } else {
            transform.localScale = new Vector3(
                (AudioHelper.amplitude * maxScale) + startScale,
                (AudioHelper.amplitude * maxScale) + startScale,
                (AudioHelper.amplitude * maxScale) + startScale);
        }
    }
}