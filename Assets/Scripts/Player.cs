using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO.Ports; // communication with arduino

public class Player : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    [SerializeField] private float jumpForce = 300f;

    [SerializeField] Vector2 boxCol2DSizeIdle      = new Vector2(0.4f, 3.1f);
    [SerializeField] Vector2 boxCol2DOffsetIdle    = new Vector2(0.3f, -0.4f);

    [SerializeField] Vector2 boxCol2DSizeGliding   = new Vector2(2.8f, 1.5f);
    [SerializeField] Vector2 boxCol2DOffsetGliding = new Vector2(1.5f, -0.75f);

    [SerializeField] private GameObject shield;
    [SerializeField] private Vector3 shieldPos;
    // determine whether a shield could be added to prevent overlapping shield
    private bool shieldAddable = true; 

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    private Animator      animator;
    private Rigidbody2D   rigidbody2D;
    private BoxCollider2D boxCollider2D;

    float yVelocity;
    float yVelocityAbs;

    ///////////////
    // Main Loop //
    ///////////////

    void Awake() {
        animator      = GetComponent<Animator>();
        rigidbody2D   = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Start() {
        // initialise the state with Running
        gameObject.tag = "Running";
        // initialise the box collider properties corresponding to running state
        boxCollider2D.size   = boxCol2DSizeIdle;
        boxCollider2D.offset = boxCol2DOffsetIdle;
    }

    void Update() {
        OnKeyInput();

        UpdateYVelocity();
        UpdateAnimationParameters();
        UpdateTag();
        UpdateBoxCollider2D();

        if (PlayerHealth.invincible) {
            Debug.Log("Enter Invincible Mode");
            SpawnShield();
        } else {
            // if there is a shield existing and the player is not invincible, destroy the shield object

                DestroyShield();


            shieldAddable = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            gameObject.tag = "Running";
        }
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    // ------- Update Properties -------

    private void UpdateYVelocity() {
        yVelocity    = rigidbody2D.velocity.y;
        yVelocityAbs = Mathf.Abs(rigidbody2D.velocity.y);
    }

    private void UpdateTag() {
        if (gameObject.tag != "Gliding") {
            if (yVelocityAbs < 0.01) {
                gameObject.tag = "Running";
            } else if (yVelocity > 0.01) {
                gameObject.tag = "Jumping";
            } else if (yVelocity < -0.01) {
                gameObject.tag = "Falling";
            }
        }
    }

    private void UpdateAnimationParameters() {
        animator.SetFloat("VerticalVelocity", yVelocity);
        animator.SetFloat("VerticalAbsoluteVelocity", yVelocityAbs);

        if (gameObject.tag == "Gliding") {
            animator.SetBool("IsGliding", true);
        } else {
            animator.SetBool("IsGliding", false);
        }
    }

    private void UpdateBoxCollider2D() {
        if (gameObject.tag == "Gliding") {
            boxCollider2D.size   = boxCol2DSizeGliding;
            boxCollider2D.offset = boxCol2DOffsetGliding;
        } else {
            boxCollider2D.size   = boxCol2DSizeIdle;
            boxCollider2D.offset = boxCol2DOffsetIdle;
        }
    }

    // ------- Define Character Action -------

    private void Jump() {
        rigidbody2D.AddForce(new Vector2(0f, jumpForce));
    }

    private void Glide() {
        gameObject.tag = "Gliding";
    }

    private void GlidingUp() {
        gameObject.tag = "Running";
    }

    // ------- Spawn Shield -------

    void SpawnShield() {
        if (shieldAddable) {
            // instantiate the next spawn
            GameObject newSpawnShield;

            // always update shield position relative to the Player
            shieldPos = new Vector3(
                transform.position.x - 1.12f,
                transform.position.y - 0.07f,
                transform.position.z);

            // run this spawn function every certain frames (defined in inspector)
            newSpawnShield = Instantiate(shield, shieldPos, Quaternion.identity);

            // make the current item a child of the SpawnManager
            newSpawnShield.transform.parent = transform;

            // prevent shield overlapping
            shieldAddable = false;
        }
    }

    void DestroyShield() {
        var shieldInstance = gameObject.transform.Find("Shield").gameObject;

        if (shieldInstance != null) {
            Destroy(shieldInstance);
        }
    }

    // ------- Keyboard Control -------

    private void OnKeyInput() {
        if (Input.GetKeyDown(KeyCode.W)) {
            Jump();
        } else if (Input.GetKey(KeyCode.S)) {
            // GetKey instead of GetKeyDown to detection key holdings
            Glide();
        } else if (Input.GetKeyUp(KeyCode.S)) {
            GlidingUp();
        }
    }

    // ------- Motion Sensor Control -------


}