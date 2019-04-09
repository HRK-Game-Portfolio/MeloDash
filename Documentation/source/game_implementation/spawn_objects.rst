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

