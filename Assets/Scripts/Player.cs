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

    [SerializeField] Vector2 boxCol2DSizeIdle      = new Vector2(0.4f, 2.3f);
    [SerializeField] Vector2 boxCol2DOffsetIdle    = new Vector2(0.3f, -0.4f);

    [SerializeField] Vector2 boxCol2DSizeGliding   = new Vector2(2.0f, 0.8f);
    [SerializeField] Vector2 boxCol2DOffsetGliding = new Vector2(1.0f, -0.8f);

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
    }

    void Update() {
        OnKeyInput();

        UpdateYVelocity();
        UpdateAnimationParameters();
        UpdateTag();
        UpdateBoxCollider2D();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            gameObject.tag = "Running";
        }
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

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
            boxCollider2D.size = boxCol2DSizeGliding;
            boxCollider2D.offset = boxCol2DOffsetGliding;
        } else {
            boxCollider2D.size = boxCol2DSizeIdle;
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

    // ------- Keyboard Control -------

    private void OnKeyInput() {
        if (Input.GetKeyDown(KeyCode.W)) {
            Jump();
        } else if (Input.GetKey(KeyCode.S)) {
            Glide();
        } else if (Input.GetKeyUp(KeyCode.S)) {
            GlidingUp();
        }
    }

    // ------- Motion Sensor Control -------


}