using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    private Transform barMask;
    private Transform bar;

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
        health -= collisionHealthPenalty;
    }

    public void CollisionWithBubble() {
        health -= collisionHealthPenalty;
    }
}
