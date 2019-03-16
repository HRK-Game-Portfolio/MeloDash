using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHelper : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------


    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    private PlayerHealth playerHealth;
    private Player player;

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {
        playerHealth = FindObjectOfType<PlayerHealth>();
        player       = FindObjectOfType<Player>();
    }

    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            playerHealth.CollisionWithObstacle();
        }

        //Debug.Log(collision.gameObject.name);
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------


    
}