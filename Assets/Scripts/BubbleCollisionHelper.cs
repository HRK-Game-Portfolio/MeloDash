using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollisionHelper : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    private PlayerHealth playerHealth;
    private Player       player;

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {
        playerHealth = FindObjectOfType<PlayerHealth>();
        player       = FindObjectOfType<Player>();
    }

    void Update() { }

    // Beware that:
    // - one of the two objects has to contain a 2d rigidbody for the trigger to work
    // - didn't want gravity to affect the obstacles thus set to Kinematic instead of Dynamic
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            playerHealth.CollisionWithBubble();

            // Bubble disappear after being collected by the player
            Destroy(gameObject);
        }    
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------
}