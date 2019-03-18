using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class SpawnManager : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    [SerializeField] private GameObject upObstacle1;
    [SerializeField] private GameObject upObstacle2;
    [SerializeField] private GameObject downObstacle;
    [SerializeField] private GameObject bubble;

    [SerializeField] private Vector3 spawnPosDownOb = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 spawnPosUpOb   = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 spawnPosBubble = new Vector3(0f, 0f, 0f);
    [SerializeField] private float   speed          = -10f;

    // not repeating one function within certain frames
    [SerializeField] private int frameIntervalUpOb   = 1;
    [SerializeField] private int frameIntervalDownOb = 1;

    // spare the player some reaction time by destroying too packed obstacles
    [SerializeField] private float jumpReactionDistance = 12f;

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {
        // Register the beat callback function
        GetComponent<BeatDetection>().CallBackFunction = MyCallbackEventHandler;
    }

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

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

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
                break;
        }
    }

    // ------------------------------------------------------
    // Spawn Objects
    // ------------------------------------------------------

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

    void addChildToCurrentObject(GameObject item) {
        // make the current item a child of the SpawnManager
        item.transform.parent = transform;
    }
}