.. figure:: ../_static/index/Cover.jpg
    :align: center

Main Character
==============

Sprites Manipulation
--------------------

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

Animations Transitions
----------------------

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

Player Health
-------------

Sprite Manipulation
~~~~~~~~~~~~~~~~~~~

The Player Health has been shown using 3 layers of sprites:

* main sprite including Whale idle on the top
* an invisible mask on top of the bottom bar
* the bottom bar which represents the actual Health

.. image:: ../_static/graphic_design/health_bar_interface.jpg
   :align: center

The manipulation of the appearance of the health bar pursued with a way that rather than vary the size of the green bar, the size of the mask on the green bar has been varied according to the current health.

To inplement this, the cached reference of the bar and the bar mask has been defined in prior:

.. code-block:: C#

    // PlayerHealth.cs (... represents other code blocks irrelevant to the current session)

    private Transform barMask;
    private Transform bar;

    ...

    void Awake() {
        barMask = transform.Find("Green Bar Mask");
        bar     = transform.Find("Green Bar");

        ...
    }

The manipulation of of the size has been implemented using the following function:

.. code-block:: C#

    // PlayerHealth.cs (... represents other code blocks irrelevant to the current session)

    private void SetSize(float sizeNormalised) {
        barMask.localScale = new Vector3(sizeNormalised, 1f);
    }

Health Point Manipulations
~~~~~~~~~~~~~~~~~~~~~~~~~~

The Player's health starts with maximum health, and each time hit with an obstacle, the health point will be deducted. The logic has been implemented by:

* defining the consequence of health penalty if collide with an obstacle
* call it in the obstacle collision helper class

.. code-block:: C#

    // PlayerHealth.cs

    [SerializeField] private float collisionHealthPenalty = 0.1f;

    ...

    public void CollisionWithObstacle() {
        // only deduct health when the player is not invincible
        if (!invincible) {
            health -= collisionHealthPenalty;
        }
    }

.. code-block:: C#

    // ObstacleCollisionHelper.cs

    private PlayerHealth playerHealth;
    private Player player;

    void Start() {
        playerHealth = FindObjectOfType<PlayerHealth>();
        player       = FindObjectOfType<Player>();
    }

    ...

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            playerHealth.CollisionWithObstacle();
        }

        //Debug.Log(collision.gameObject.name);
    }