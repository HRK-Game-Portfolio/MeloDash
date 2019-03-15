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

    [SerializeField] private GameObject smallFish;
    [SerializeField] private GameObject bigFish;
    [SerializeField] private GameObject trash;

    [SerializeField] private float   spawnInterval = 2f;
    [SerializeField] private Vector3 spawnPos      = new Vector3(0f, 0f, 0f);
    [SerializeField] private float   speed         = -10f;

    ///////////////
    // Main Loop //
    ///////////////

    void Start() {
        // trigger spawning new object, starting from 2s, with frequency of once each 2s
        InvokeRepeating("spawnObject", 2.0f, 2.0f);
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

    private void spawnObject() {
        // instantiate the next spawn
        GameObject newSpawn;

        // random 1/3 possibility spawning each of the 3 plausible objects
        Random random = new Random();
        int randomThreshold = random.Next(1, 4); // generate a integer number between 1, 2, 3

        if (randomThreshold == 1) {
            newSpawn = Instantiate(
                smallFish,
                spawnPos,
                Quaternion.identity);
            addChildToCurrentObject(newSpawn);
        } else if (randomThreshold == 2) {
            newSpawn = Instantiate(
                bigFish,
                spawnPos,
                Quaternion.identity);
            addChildToCurrentObject(newSpawn);
        } else if (randomThreshold == 3) {
            newSpawn = Instantiate(
                trash,
                spawnPos,
                Quaternion.Euler(0, 0, -20f)); // beware the trash spawn has rotation angle
            addChildToCurrentObject(newSpawn);
        }
    }

    void addChildToCurrentObject(GameObject item) {
        // make the current item a child of the SpawnManager
        item.transform.parent = transform;
    }
}