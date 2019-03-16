using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate512Cubes : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    public float _maxScale;

    // ------------------------------------------------------
    // Cached References
    // ------------------------------------------------------

    public GameObject _sampleCubePrefab;
    GameObject[] _sampleCube = new GameObject[512];

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {
        for (int i = 0; i < 512; i++) {
            GameObject _instancesampleCube = (GameObject)Instantiate(_sampleCubePrefab);
            _instancesampleCube.transform.position = this.transform.position;
            _instancesampleCube.transform.parent = this.transform;
            _instancesampleCube.name = "SampleCube" + i;
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0); // 360 / 512
            _instancesampleCube.transform.position = Vector3.forward * 100;
            _sampleCube[i] = _instancesampleCube;
        }
    }

    void Update() {
        for (int i = 0; i < 512; i++) {
            if (_sampleCube != null) {
                _sampleCube[i].transform.localScale = new Vector3(10, (AudioPeer._samples[i] * _maxScale) + 2, 10); // starting size of 2
            }
        }
    }
}
