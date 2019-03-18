using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {
    // ------------------------------------------------------
    // Config Params
    // ------------------------------------------------------

    ///////////////
    // Main Loop //
    ///////////////

    void Start() { }

    void Update() {
        LoadGameScene();
    }

    public void LoadGameScene() {
        if (Input.GetMouseButtonDown(0)) {
            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            //RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            //if (hit.collider != null) {
            //    Debug.Log(hit.collider.gameObject.name);
            //    hit.collider.attachedRigidbody.AddForce(Vector2.up);
                Debug.Log("You have clicked the button!");
                SceneManager.LoadScene("Game");
            //}

        }
    }
}