using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBgdSpectrum : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    [SerializeField] Vector3 firstBlockPos = new Vector3(0f, 0f, 0f);
    public float maxScale;

    // ------------------------------------------------------
    // Cached References
    // ------------------------------------------------------

    public GameObject block;
    GameObject[] blockArray = new GameObject[8];

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {
        for (int i = 0; i < blockArray.Length; i++) {
            GameObject instanceBlock = (GameObject)Instantiate(block);
            instanceBlock.transform.position = this.transform.position;
            instanceBlock.transform.parent = this.transform;
            instanceBlock.name = "InstanceBlock" + i;

            instanceBlock.transform.position = new Vector3(
                firstBlockPos.x + (0.5f * i),
                firstBlockPos.y,
                firstBlockPos.z);
            blockArray[i] = instanceBlock;
        }
    }

    void Update() {
        for (int i = 0; i < blockArray.Length; i++) {
            if (block != null) {
                Debug.Log(blockArray[i].transform.localScale);
                blockArray[i].transform.localScale = new Vector2(
                    0.9f,
                    AudioHelper.bandBuffer[i] * maxScale + 2);
            }
        }
    }
}
