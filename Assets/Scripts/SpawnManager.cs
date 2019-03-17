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

    [SerializeField] private Vector3 spawnPosDownOb = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 spawnPosUpOb   = new Vector3(0f, 0f, 0f);
    [SerializeField] private float   speed          = -10f;

    // not repeating one function within 0.2s
    [SerializeField] private float duration = 0.2f;

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

        for (int i = 0; i < children.Length; i++) {
            var child = children[i];
            // beware to add Space.World or otherwise default will be Space.Self
            // where rotation angle of the object will be stored as well
            child.transform.Translate(Vector2.right * displacement, Space.World);
        }
    }

    // ------------------------------------------------------
    // Customised Methods
    // ------------------------------------------------------

    public void MyCallbackEventHandler(BeatDetection.EventInfo eventInfo) {
        switch (eventInfo.messageInfo) {
            case BeatDetection.EventType.Energy:
                spawnUpOb();
                break;
            case BeatDetection.EventType.HitHat:
                spawnDownOb();
                break;
            case BeatDetection.EventType.Kick:

                break;
            case BeatDetection.EventType.Snare:

                break;
        }
    }

    void spawnUpOb() {
        // instantiate the next spawn
        GameObject newSpawnUpOb;

        // random 1/2 possibility spawning each of the 2 plausible objects
        Random random = new Random();
        int randomThreshold = random.Next(1, 3); // generate a integer number between 1, 2

        if (randomThreshold == 1) {
            newSpawnUpOb = Instantiate(upObstacle1, spawnPosUpOb, Quaternion.identity);
            addChildToCurrentObject(newSpawnUpOb);
        } else if (randomThreshold == 2) {
            newSpawnUpOb = Instantiate(upObstacle2, spawnPosUpOb, Quaternion.identity);
            addChildToCurrentObject(newSpawnUpOb);
        }
    }

    void spawnDownOb() {
        // instantiate the next spawn
        GameObject newSpawnDownOb;

        newSpawnDownOb = Instantiate(downObstacle, spawnPosDownOb, Quaternion.identity);
        addChildToCurrentObject(newSpawnDownOb);
    }

    void addChildToCurrentObject(GameObject item) {
        // make the current item a child of the SpawnManager
        item.transform.parent = transform;
    }
}