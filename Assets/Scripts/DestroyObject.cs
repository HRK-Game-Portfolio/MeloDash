using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    [SerializeField] private float destroyXPos = -18f;

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    private PlayerHealth playerHealth;

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {

    }

    void Update() {
        DestroyHierarchy();
    }

    // Beware that:
    // - one of the two objects has to contain a 2d rigidbody for the trigger to work
    // - in this case both need in order to make the Small Fish Box Collider work
    // - didn't want gravity to effect my player so I had set to Kinematic instead of Dynamic
    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject);
        Debug.Log(collision.gameObject.name);
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