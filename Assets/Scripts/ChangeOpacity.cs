using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOpacity : MonoBehaviour {

    // change the transparency for game objects
    [SerializeField] private float spriteOpacity = 1.0f;
    [SerializeField] private Color color = Color.black;

    // Start is called before the first frame update
    void Start() {
//        var opColorA = gameObject.GetComponent<Renderer>().material.color.a;
//        opColorA = spriteOpacity;
//        Debug.Log(spriteOpacity);
//
//        var color = gameObject.GetComponent<Renderer>().material.color;
//        var newColor = new Color(color.r, color.g, color.b, 0.5f);
//        gameObject.GetComponent<Renderer>().material.color = newColor;
        color.a = 0.42f;
    }

    // Update is called once per frame
    void Update() {

    }
}
