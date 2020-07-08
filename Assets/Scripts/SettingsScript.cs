using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public Camera cam;
    private AudioSource audioSource;
    public DialogueBox dB;
    public PlayerManager pM;

    public string gameObjectName;
    public GameObject[] hideOrShow;
    private SpriteRenderer spriteRenderer;
    private int changing = -1;
    public int type;
    void Start() {
        audioSource = cam.GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
                spriteRenderer.color = new Color(174, 142, 142);
                // Mute sound button or sound on button
                 if (gameObjectName == "soundmute"){
                    audioSource.mute = true;
                } else if (gameObjectName == "soundon") {
                    audioSource.mute = false;
                } 
                // Changing dialogue speed
                else if (gameObjectName == "dialoguespeed1") {
                    dB.speakingSpeed = .07f;
                } else if (gameObjectName == "dialoguespeed2") {
                    dB.speakingSpeed = .03f;
                } else if (gameObjectName == "dialoguespeed3") {
                    dB.speakingSpeed = .01f;
                }
                // Changing Walking Speed
                if (gameObjectName == "walkingspeed1") {
                    pM.speed = 10;
                } else if (gameObjectName == "walkingspeed2") {
                    pM.speed = 15;
                } else if (gameObjectName == "walkingspeed3") {
                    pM.speed = 20;
                }}
            }
        }

}