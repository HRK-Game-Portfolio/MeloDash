using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO.Ports; // communication with arduino

public class Player : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    private float jumpForce = 300f;

    // ------------------------------------------------------
    // Cached Reference
    // ------------------------------------------------------

    private Animator    animator;
    private Rigidbody2D rigidbody2D;

    enum State {
        Running,
        Jumping,
        Falling,
        Gliding,
    }

    private State state;

    ///////////////
    // Main Loop //
    ///////////////

    void Awake() {
        animator    = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start() {
        // initialise the state with Running
        state = State.Running;
    }

    void Update() {
        Jump();
        UpdateAnimationParameters();

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            state = State.Running;
        }
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    private void UpdateAnimationParameters() {
        float yVelocity    = rigidbody2D.velocity.y;
        float yVelocityAbs = Mathf.Abs(rigidbody2D.velocity.y);

        animator.SetFloat("VerticalVelocity", yVelocity);
        animator.SetFloat("VerticalAbsoluteVelocity", yVelocityAbs);
    }

    // ----- Keyboard Control -----

    private void Jump() {
        if (Input.GetKeyDown(KeyCode.W)) {
            //Debug.Log("Jump");
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            state = State.Jumping;
        }
    }

    // ----- Motion Sensor Control -----


}