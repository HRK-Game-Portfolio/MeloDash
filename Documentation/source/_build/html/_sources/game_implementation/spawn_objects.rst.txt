.. figure:: ../_static/index/Cover.jpg
    :align: center

Objects Spawn & Properties
==========================

Objects Spawn
-------------

Communication with Beat Detection Server
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

The object spawn takes place in the ``SpawnManager`` class with the logical conditions that have been satisfied in ``BeatDetection`` class. The two classes communicate utilising the event handler:

.. code-block:: C#

    // SpawnManager.cs ("..." represents other code blocks irrelevant to the current session)

    ...

    public void MyCallbackEventHandler(BeatDetection.EventInfo eventInfo) {
        switch (eventInfo.messageInfo) {
            case BeatDetection.EventType.Energy: // low freq, high amp
                spawnUpOb();
                break;
            case BeatDetection.EventType.HitHat: // high freq
                spawnDownOb();
                break;
            case BeatDetection.EventType.Kick:
                spawnBubble();
                break;
            case BeatDetection.EventType.Snare:
                spawnLightning();
                break;
        }
    }

.. code-block:: C#

    // BeatDetection.cs ("..." represents other code blocks irrelevant to the current session)

    public enum EventType {
        Energy,
        Kick,
        Snare,
        HitHat
    }

    public class EventInfo {
        public EventType messageInfo;
        public BeatDetection sender;
    }

    public delegate void CallbackEventHandler(EventInfo eventInfo);

    public CallbackEventHandler CallBackFunction;

    ...

    void SendEvent(EventType theEvent) {
        if (CallBackFunction != null) {
            EventInfo myEvent = new EventInfo();
            myEvent.sender = this;
            myEvent.messageInfo = theEvent;
            CallBackFunction(myEvent);
        }
    }

Obastacles
~~~~~~~~~~

There are 3 Obastacle types with two upper and one down:

.. |down_ob| image:: ../_static/graphic_design/obstacles/DownObstacle.png
    :align: middle

.. |up_ob1| image:: ../_static/graphic_design/obstacles/UpObstacle1.png
    :align: middle

.. |up_ob2| image:: ../_static/graphic_design/obstacles/UpObstacle2.png
    :align: middle

+---------------+---------------+---------------+
| Down Obstacle | Up Obstacle 1 | Up Obstacle 2 |
+---------------+---------------+---------------+
| |down_ob|     | |up_ob1|      | |up_ob2|      |
+---------------+---------------+---------------+

Each spawned by the following functions:

.. code-block:: C#

    // SpawnManager.cs ("..." represents other code blocks irrelevant to the current session)

    // spawning up obstacles
    void spawnUpOb() {
        // instantiate the next spawn
        GameObject newSpawnUpOb;

        // random 1/2 possibility spawning each of the 2 plausible objects
        Random random = new Random();
        int randomThreshold = random.Next(1, 3); // generate a integer number between 1, 2

        // run this spawn function every certain frames (defined in inspector)
        if (Time.frameCount % frameIntervalUpOb == 0) {
            if (randomThreshold == 1) {
                newSpawnUpOb = Instantiate(upObstacle1, spawnPosUpOb, Quaternion.identity);
                addChildToCurrentObject(newSpawnUpOb);
            } else if (randomThreshold == 2) {
                newSpawnUpOb = Instantiate(upObstacle2, spawnPosUpOb, Quaternion.identity);
                addChildToCurrentObject(newSpawnUpOb);
            }
        }
    }

    // spawning down obstacles
    void spawnDownOb() {
        // instantiate the next spawn
        GameObject newSpawnDownOb;

        // run this spawn function every certain frames (defined in inspector)
        if (Time.frameCount % frameIntervalDownOb == 0) {
            newSpawnDownOb = Instantiate(downObstacle, spawnPosDownOb, Quaternion.identity);
            addChildToCurrentObject(newSpawnDownOb);
        }
    }

.. note:: Two up obstacles each has 1/2 chance of being spawned by using the random function

Bubbles Shield System
~~~~~~~~~~~~~~~~~~~~~

Shields will be generated if 5 bubbles have collected as mentioned previously:

.. figure:: ../_static/index/shield_feature.jpg
    :align: center

The 4 sprites of the shield has been shown below:

.. |shield1| image:: ../_static/graphic_design/shields/Shield1.png
    :align: middle
    :scale: 7%

.. |shield2| image:: ../_static/graphic_design/shields/Shield2.png
    :align: middle
    :scale: 7%

.. |shield3| image:: ../_static/graphic_design/shields/Shield3.png
    :align: middle
    :scale: 7%

.. |shield4| image:: ../_static/graphic_design/shields/Shield4.png
    :align: middle
    :scale: 7%

+-----------------------------------------+
| |shield1| |shield2| |shield3| |shield4| |
+-----------------------------------------+

.. code-block:: C#

    // Player.cs (... represents other code blocks irrelevant to the current session)

    ...

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
        if (transform.childCount > 0) {
            var shieldInstance = transform.GetChild(0).gameObject;
            if (shieldInstance != null) {
                Destroy(shieldInstance);
            }
        }
    }

The sprite of the bubbles is:

.. figure:: ../_static/graphic_design/Bubble.png
    :align: center
    :scale: 20% 

The bubbles have been spawned by the following functions:

.. code-block:: C#

    // spawning bubbles
    void spawnBubble() {
        // instantiate the next spawn
        GameObject newSpawnBubble;

        // random 1/2 possibility spawning at one of the two plausible heights
        Random random = new Random();
        int randomThreshold = random.Next(1, 3); // generate a integer number between 1, 2

        // run this spawn function every certain frames (defined in inspector)
        if (Time.frameCount % frameIntervalDownOb == 0) {
            if (randomThreshold == 1) {
                newSpawnBubble = Instantiate(bubble, spawnPosBubble, Quaternion.identity);
                addChildToCurrentObject(newSpawnBubble);
            } else if (randomThreshold == 2) {
                newSpawnBubble = Instantiate(
                    bubble, 
                    new Vector3(
                        spawnPosBubble.x, 
                        spawnPosBubble.y - 4, 
                        spawnPosBubble.z), 
                    Quaternion.identity);
                addChildToCurrentObject(newSpawnBubble);
            }
        }
    }

.. note:: bubbles are generated in 2 various altitudes each has 1/2 chance

