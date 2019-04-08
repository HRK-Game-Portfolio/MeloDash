The background consist of a static purple skybox and a fast moving ground earth:

.. |bgd_pink| image:: _static/graphic_design/background/background_pink.jpg
    :align: middle

.. |earth| image:: _static/graphic_design/background/earth.png
    :align: middle

+---------------------------+------------------------------------------------+
| Sky Box Purple Background | Earth                                          |   
+---------------------------+------------------------------------------------+
| |bgd_pink|                | |earth|                                        |
+---------------------------+------------------------------------------------+

Two layers of contours of ruins decorated the far scene of the interface: 

.. |ruins_closer| image:: _static/graphic_design/background/ruins_closer.png
    :align: middle

.. |ruins_further| image:: _static/graphic_design/background/ruins_further.png
    :align: middle

+-----------------+
| Ruins Closer    |
+-----------------+
| |ruins_closer|  | 
+-----------------+
| Ruins Further   |
+-----------------+
| |ruins_further| | 
+-----------------+

In order to convey the effect that girl is running towards right whilst its relative x-position to the screen boundary maintains, functions need to be defined to let the various objects such as earth and ruins scroll to the left at different speeds which also engaged a parallel effect between further and closer objects.

.. code-block:: C#

    [SerializeField] private float scrollSpeed = -4f;
    [SerializeField] private int resetX = -32;

    void Start() {
        // override the start position to its initial sprite position
        startPos = transform.position;
    }

    void Update() {
        xPos = transform.position.x;
        yPos = transform.position.y;

        float displacement = Time.deltaTime * scrollSpeed;
        transform.Translate(Vector2.right * displacement);

        // when the center of Wave scrolls to one screen width to the left of the original center,
        // reset the X of the Wave entity to it's original starting position
        if (xPos < resetX) {
            transform.position = new Vector3(startPos.x, yPos, startPos.z);
        }

        ...
    }
