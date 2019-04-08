.. figure:: ../_static/index/Cover.jpg
    :align: center

Main Character
==============

The original character concept and **exploded view** has been shown below, all the character postures have been created by manipulating the various parts of the body and limbs.

.. |girl_idol| image:: ../_static/graphic_design/girl.png
    :align: middle

.. |exploded_view| image:: ../_static/graphic_design/exploded_view.png
    :align: middle

+-------------+-----------------+
| Girl Idol   | Exploded View   |   
+-------------+-----------------+
| |girl_idol| | |exploded_view| |
+-------------+-----------------+

In order to achieve a more natural and fluent effect, the sprites have been traced over the animations done by Richard Williams. The quality of the animation not only afftect asthetic but also player’s experience. if the animation itself wobbles a lot it will influence the way player perceive visual feedback from the character.

.. figure:: ../_static/graphic_design/running_sprites.png
    :align: center

    Sprites during Running states 

.. figure:: ../_static/graphic_design/running_sprite_reference.jpg
    :align: center

    Running Sprites Reference

The animations have been created using Unity Animation tab. All animations are appended to the Player object:

.. figure:: ../_static/graphic_design/animation_tab.jpg
    :align: center

    Unity Animation Tab

The character has 5 basic postures according to different circumstances:

- **Running**: the posture of running on the ground, if the character doesn't perform any other tasks, running will be the default posture.
- **Jump**: where the girl jumps to space, the character may continuously keep jumping in the space.
- **Fall**: the falling posture occurring right after jumping. 
- **Glide**: which is middle transition progress from the previous posture to the keeping gliding gesture.
- **Gliding**: where the girl keeps the continuous gliding gesture.

.. |running| image:: ../_static/graphic_design/5_postures/running.gif
    :align: middle

.. |jump| image:: ../_static/graphic_design/5_postures/jump.gif
    :align: middle

.. |fall| image:: ../_static/graphic_design/5_postures/fall.gif
    :align: middle

.. |glide| image:: ../_static/graphic_design/5_postures/glide.gif
    :align: middle

.. |gliding| image:: ../_static/graphic_design/5_postures/gliding.gif
    :align: middle

+-----------+-----------+-----------+-----------+-----------+
| Running   | Jump      | Fall      | Glide     | Gliding   |
+-----------+-----------+-----------+-----------+-----------+
| |Running| | |jump|    | |fall|    | |glide|   | |gliding| |
+-----------+-----------+-----------+-----------+-----------+

The transition Logic between sprite animations in the Animator has been shown below:

.. figure:: ../_static/graphic_design/animation_logic.jpg
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

