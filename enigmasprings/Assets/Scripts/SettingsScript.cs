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
                }
            } else {
                // Mute sound button or sound on button + Colors of button
                 if (gameObjectName == "soundmute"){
                    audioSource.mute = true;
                    spriteRenderers[0].color = new Color(174, 142, 142);
                } else if (gameObjectName == "soundon") {
                    audioSource.mute = false;
                    spriteRenderers[1].color = new Color(174, 142, 142);
                } 
                // Changing dialogue speed + Colors of button
                else if (gameObjectName == "dialoguespeed1") {
                    DialogueBox.speakingSpeed = .07f;
                    spriteRenderers[2].color = new Color(174, 142, 142);

                } else if (gameObjectName == "dialoguespeed2") {
                    DialogueBox.speakingSpeed = .04f;
                    spriteRenderers[3].color = new Color(174, 142, 142);
                } else if (gameObjectName == "dialoguespeed3") {
                    DialogueBox.speakingSpeed = .01f;
                    spriteRenderers[4].color = new Color(174, 142, 142);
                }
                // Changing Walking Speed + Colors of button
                else if (gameObjectName == "walkingspeed1") {
                    PlayerManager.speed = 4;
                    spriteRenderers[5].color = new Color(174, 142, 142);
                } else if (gameObjectName == "walkingspeed2") {
                    PlayerManager.speed = 7;
                    spriteRenderers[6].color = new Color(174, 142, 142);
                } else if (gameObjectName == "walkingspeed3") {
                    PlayerManager.speed = 10;
                    spriteRenderers[7].color = new Color(174, 142, 142);
                }}
            }
        }

}