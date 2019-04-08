The transition Logic between sprite animations in the Animator has been shown below:

.. figure:: _static/graphic_design/animation_logic.jpg
    :align: center

    Animation Transition Logic

The conditions that triggers the transitions are:

* The Jump-Fall System:
    - **Running** to **Jumping**: ``VerticalVelocity`` :math:`>` ``0.01``
    - **Jumping** to **Falling**: ``VerticalVelocity`` :math:`<` ``-0.01``
    - **Falling** to **Running**: ``VerticalAbsoluteVelocity`` :math:`<` ``0.01``

* The Glide-Stand System:
    - **Running** to **Glide**: ``IsGliding`` == ``true``
    - **Glide** to **Gliding**: no condition, enters directly and keep in the continuous loop of **Gliding**
    - **Gliding** to **Running**: ``IsGliding`` == ``false``

The manipulation of the above properties in the script is shown below:

.. code-block:: C#

    // ------- Update Properties -------

    private void UpdateYVelocity() {
        yVelocity    = rigidbody2D.velocity.y;
        yVelocityAbs = Mathf.Abs(rigidbody2D.velocity.y);
    }

    ...

    private void UpdateAnimationParameters() {
        animator.SetFloat("VerticalVelocity", yVelocity);
        animator.SetFloat("VerticalAbsoluteVelocity", yVelocityAbs);

        if (gameObject.tag == "Gliding") {
            animator.SetBool("IsGliding", true);
        } else {
            animator.SetBool("IsGliding", false);
        }
    }





The

.. |bgd_pink| image:: _static/graphic_design/background/background_pink.jpg
    :align: middle

.. |earth| image:: _static/graphic_design/background/earth.png
    :align: middle

+---------------------------+------------------------------------------------+
| Sky Box Purple Background | Earth                                          |   
+---------------------------+------------------------------------------------+
| |bgd_pink|                | |earth|                                        |
+---------------------------+------------------------------------------------+

