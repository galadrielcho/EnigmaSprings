using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public GameObject bg;
    public GameObject options;
    public GameObject title;
    public string gameObjectName;
    void FixedUpdate()
    {
        // Check for touches and then detect if raycast hit collider and if the touched object is itself
        if (Input.touchCount > 0 && GameManagerScript.hitInfo.collider != null && GameManagerScript.touchedObject == gameObjectName) {
            if ((gameObjectName == "settings" && !bg.activeSelf)|| (gameObjectName == "widebackground" && bg.activeSelf)) {
                // Hide or show bg, options, and title for settings screen
                bg.SetActive(!bg.activeSelf);
                options.SetActive(!options.activeSelf);
                title.SetActive(!title.activeSelf);
            }
        }
    }
}