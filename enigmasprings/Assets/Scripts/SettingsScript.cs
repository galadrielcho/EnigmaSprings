using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public Camera cam;
    private AudioSource audioSource;

    public GameObject player;
    public string gameObjectName;
    public GameObject[] hideOrShow;
    private int choice = -1;
    private SpriteRenderer[] spriteRenderers = new SpriteRenderer[8];
    void Start() {
        audioSource = cam.GetComponent<AudioSource>();
        for (int i = 3; i < 11; i++) {
            spriteRenderers[i-3] = hideOrShow[i].GetComponent<SpriteRenderer>();
        }

    }
    void FixedUpdate()
    {
        // Check for touches and then detect if raycast hit collider and if the touched object is itself
        if (Input.touchCount > 0 && GameManagerScript.hitInfo.collider != null && GameManagerScript.touchedObject == gameObjectName) {
            // Settings button or 'x' button
            if ((gameObjectName == "settings" && !hideOrShow[0].activeSelf)|| (gameObjectName == "x" && hideOrShow[0].activeSelf)) {
                // Hide or show bg, options, and title for settings screen
                for (int i = 0; i < hideOrShow.Length; i++) {
                    hideOrShow[i].SetActive(!hideOrShow[i].activeSelf);
                    GameManagerScript.speaking = hideOrShow[i].activeSelf;
                }
            } else {
                // Mute sound button or sound on button + Color change of selected button
                 if (gameObjectName == "soundmute"){
                    choice = 0;
                    audioSource.mute = true;
                    spriteRenderers[0].color = new Color(.6823f, .5568f, .5568f, 1);
                } else if (gameObjectName == "soundon") {
                    choice = 1;
                    audioSource.mute = false;
                    spriteRenderers[1].color = new Color(.6823f, .5568f, .5568f, 1);
                } 
                // Changing dialogue speed + Colors change of selected button
                else if (gameObjectName == "dialoguespeed1") {
                    choice = 2;
                    DialogueBox.speakingSpeed = .07f;
                    spriteRenderers[2].color = new Color(.6823f, .5568f, .5568f, 1);

                } else if (gameObjectName == "dialoguespeed2") {
                    choice = 3;
                    DialogueBox.speakingSpeed = .04f;
                    spriteRenderers[3].color = new Color(.6823f, .5568f, .5568f, 1);
                } else if (gameObjectName == "dialoguespeed3") {
                    choice = 4;
                    DialogueBox.speakingSpeed = .01f;
                    spriteRenderers[4].color = new Color(.6823f, .5568f, .5568f, 1);
                }
                // Changing Walking Speed + Change color of button selected
                else if (gameObjectName == "walkingspeed1") {
                    choice = 5;
                    PlayerManager.speed = 4;
                    spriteRenderers[5].color = new Color(.6823f, .5568f, .5568f, 1);
                } else if (gameObjectName == "walkingspeed2") {
                    choice = 6;
                    PlayerManager.speed = 7;
                    spriteRenderers[6].color = new Color(.6823f, .5568f, .5568f, 1);
                } else if (gameObjectName == "walkingspeed3") {
                    choice = 7;
                    PlayerManager.speed = 10;
                    spriteRenderers[7].color = new Color(.6823f, .5568f, .5568f, 1);
                }}
                if (choice >= 0 && choice <= 1) {
                    for (int i = 0; i <= 1; i ++) {
                        if (i != choice)
                            spriteRenderers[i].color = Color.white;
                    }
                } else if (choice >= 2 && choice <= 4) {
                    for (int i = 2; i <= 4; i ++) {
                        if (i != choice)
                            spriteRenderers[i].color = Color.white;
                    }
                } else if (choice >= 5 && choice <= 7) {
                    for (int i = 5; i <= 7; i ++) {
                        if (i != choice)
                            spriteRenderers[i].color = Color.white;
                    }
                }
            }
        }

}