using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    [SerializeField] private float destroyXPos = -20f;

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {

    }

    void Update() {
        DestroyHierarchy();
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    public void DestroyHierarchy() {
        //Debug.Log(gameObject.transform.position.x);
        if (gameObject.transform.position.x < destroyXPos) {
            Destroy(gameObject);
        }
    }

}