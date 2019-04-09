.. figure:: ../_static/index/Cover.jpg
    :align: center

Objects Spawn & Properties
==========================

Communication with Beat Detection Server
----------------------------------------

The object spawn takes place in the ``SpawnManager`` class with the logical conditions that have been satisfied in ``BeatDetection`` class. The two classes communicate utilising the event handler:

.. code-block:: C#

    // SpawnManager.cs ("..." represents other code blocks irrelevant to the current session)

    ...

    void Start() {
        // Register the beat callback function
        GetComponent<BeatDetection>().CallBackFunction = MyCallbackEventHandler;
    }

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

Obstacles
---------

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
---------------------

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

Constant Leftward Movement
--------------------------

The constant leftward movement of the objects pursue with the following logic:

1. when a new object has been spawned, append it to the current spawn manager parent object
2. in each iteration of ``Update()`` function being called, loop through all the current children of the parent spawn manager object in a for-loop 
3. apply a left-ward vector to every single child in the loop

.. note:: since the child objects of spawn manager could be distroyed due being eaten by the Whale or self-destructed outside the boundary of the screen, the number of items within the spawn manager is varying thus need a agile and flexible approach on a dynamic array instance of collection of all children objects.

.. code-block:: C#

    // SpawnSeaGullManager.cs (... represents other code blocks irrelevant to the current session)

    ...

    void Update() {
        float displacement = Time.deltaTime * speed;

        // store all children under Spawn Manager in an array
        Transform[] children = transform.Cast<Transform>().ToArray();

        // ------- obstacles moving towards left -------
        // mind that the moving functionality has to be implemented before destroying redundant objects
        // or otherwise the array length will be changed before moving all the objects
        for (int i = 0; i < children.Length; i++) {
            // beware to add Space.World or otherwise default will be Space.Self
            // where rotation angle of the object will be stored as well
            children[i].transform.Translate(Vector2.right * displacement, Space.World);
        }

        ...
    }

The append of child happend during the creation of each object:

.. code-block:: C#

    // SpawnSeaGullManager.cs (... represents other code blocks irrelevant to the current session)

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

    void addChildToCurrentObject(GameObject item) {
        // make the current item a child of the SpawnManager
        item.transform.parent = transform;
    }

Destroy Objects
---------------

If the object spawned hasn't been eaten, it will continue to move left-wards and stack in the spawn manager parent object, which will consume plenty of computer memory and thus harmful for the program.

Therefore, all object will be destroyed if they are outside the left boundary of the screen to save the computational power.

.. code-block:: C#

    // DestroyObject.cs (... represents other code blocks irrelevant to the current session)

    [SerializeField] private float destroyXPos = -18f;

    ...

    void Update() {
        DestroyHierarchy();
    }

    public void DestroyHierarchy() {
        //Debug.Log(gameObject.transform.position.x);
        if (gameObject.transform.position.x < destroyXPos) {
            Destroy(gameObject);
        }
    }

Prevent Packed Obstacles
------------------------

.. attention:: To make the game playable, the minimal inetrval between obstacles are equal to half of character’s jump distance (12/2). Any obstacles generated within that distance will be deleted from the list.

.. code-block:: C#

    void Update() {
        
        ...

        // ------- prevent obstacles from spawning too close to each other -----
        if (children.Length >= 2) {
            var lastChild       = children[children.Length - 1].gameObject;
            var lastSecondChild = children[children.Length - 2].gameObject;

            string lastChildName       = lastChild.name;
            string lastSecondChildName = lastSecondChild.name;

            float lastChildXPos       = children[children.Length - 1].transform.position.x;
            float lastSecondChildXPos = children[children.Length - 2].transform.position.x;

            //Debug.Log(lastSecondChildName);
            //Debug.Log(lastChildName);

            /*
             * if the last obstacle spawned is to close to the last second obstacle spawned,
             * destroy the last one to prevent obstacles from spawning too close to each other
             * which left impossible situation for the player to mitigate
             */
            if (lastChildName == lastSecondChildName && lastChildName == "DownObstacle") {
                if (lastChildXPos - lastSecondChildXPos < jumpReactionDistance) {
                    Destroy(lastChild);
                }
            } else {
                if (lastChildXPos - lastSecondChildXPos < jumpReactionDistance / 2) {
                    Destroy(lastChild);
                }
            }
        }
    }