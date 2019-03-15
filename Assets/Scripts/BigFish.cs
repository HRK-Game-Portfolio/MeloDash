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
    private Jaw jaw;

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {
        playerHealth = FindObjectOfType<PlayerHealth>();
        jaw = FindObjectOfType<Jaw>();
    }

    void Update() {
        ChangeSprites();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log(collision.gameObject.name);
        playerHealth.EatBigFish();
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    private void ChangeSprites() {
        if (transform.position.x > jaw.pivotCoordinate.x &&
            transform.position.x < 4f) {
            //Debug.Log(jaw.pivotCoordinate.x);
            // When the fish is close to the jaw but not being eaten yet
            GetComponent<SpriteRenderer>().sprite = bigFishFrightened;
        } else if (transform.position.x < jaw.pivotCoordinate.x - 1f) {
            // When the fish passed the Whale, indicating the Whale missed capturing it
            GetComponent<SpriteRenderer>().sprite = bigFishLaugh;
        }
    }
    
}