using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFish : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    [SerializeField] private Sprite bigFishDefault;
    [SerializeField] private Sprite bigFishFrightened;
    [SerializeField] private Sprite bigFishLaugh;

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
        player = FindObjectOfType<Player>();
    }

    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log(collision.gameObject.name);
        playerHealth.EatBigFish();
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------


    
}