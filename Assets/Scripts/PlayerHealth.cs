using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    [SerializeField] private float health             = 1f;
    [SerializeField] private float healthMax          = 1f;
    [SerializeField] private float healthMin          = 0f;
    [SerializeField] private float healthWarn         = 0.3f;
    [SerializeField] private float healthDecreaseRate = 0.001f;

    private Transform barMask;
    private Transform bar;

    void Awake() {
        barMask = transform.Find("Green Bar Mask");
        bar = transform.Find("Green Bar");
    }

    void Start() {

    }

    void Update() {
        ConstantHealthIncrease();
        SetSize(health);

        // under 30% health, start warning
        if (health < healthWarn) {
            SetColour(Color.red);

            //if ((health * 100f) / 3 == 0) {
            //    SetColour(Color.white);
            //} else {
            //    SetColour(Color.red);
            //}
        }

        if (health > healthMax) {
            health = healthMax;
        }
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    // ----- Bar Appearance Manipulation -----

    private void SetSize(float sizeNormalised) {
        barMask.localScale = new Vector3(sizeNormalised, 1f);
    }

    private void SetColour(Color colour) {
        bar.GetComponent<SpriteRenderer>().color = colour;
    }

    // ----- Health Manipulations -----

    private void ConstantHealthIncrease() {
        if (health < healthMax) {
            health += healthDecreaseRate;
        }
    }

    // ----- Eaten Behaviour -----

    public void CollisionWithObstacle() {
        health -= 0.4f;
    }
}
