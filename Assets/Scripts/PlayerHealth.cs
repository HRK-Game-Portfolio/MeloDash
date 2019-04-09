using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    [SerializeField] private float health                 = 1f;
    [SerializeField] private float healthMax              = 1f;
    [SerializeField] private float healthMin              = 0f;
    [SerializeField] private float healthWarn             = 0.3f;
    [SerializeField] private float healthIncreaseRate     = 0.001f;
    [SerializeField] private float collisionHealthPenalty = 0.1f;

    // bubble collection and invincibility
    // collect 5 bubbles to become temporarily invincible
    private float bubbleCount = 0;
    // public to be accessed by Player class to add shield
    public static bool invincible = false; //TODO
    [SerializeField] private float invincibleDuration = 5.0f;

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    private Transform barMask;
    private Transform bar;

    private SceneLoader sceneLoader;

    void Awake() {
        barMask = transform.Find("Health Bar Mask");
        bar     = transform.Find("Health Bar");
    }

    void Start() {

    }

    void Update() {
        ConstantHealthIncrease();
        SetSize(health);

        if (health > healthMax) {
            health = healthMax;
        }

        if (health <= healthMin) {
            //sceneLoader.LoadStartScene();
            SceneManager.LoadScene("Start");
        }
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    // ----- Bar Appearance Manipulation -----

    private void SetSize(float sizeNormalised) {
        barMask.localScale = new Vector3(sizeNormalised, 1.0f);
    }
    
    // ----- Health Manipulations -----

    private void ConstantHealthIncrease() {
        if (health < healthMax) {
            health += healthIncreaseRate;
        }
    }

    // ----- Collision Behaviour -----

    public void CollisionWithObstacle() {
        // only deduct health when the player is not invincible
        if (!invincible) {
            health -= collisionHealthPenalty;
        }
    }

    // collect 5 bubbles to become temporarily invincible
    public void CollisionWithBubble() {
        if (bubbleCount < 4) {
            bubbleCount++;
        } else {
            EnterInvincibleMode();
            bubbleCount = 0;
        }
    }

    void EnterInvincibleMode() {
        invincible = true;
        Invoke("ExitInvincibleMode", invincibleDuration);
    }

    void ExitInvincibleMode() {
        invincible = false;
    }
}