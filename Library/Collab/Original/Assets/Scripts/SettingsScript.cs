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
    public SpriteRenderer[] spriteRenderers = new SpriteRenderer[8];
    private int[] selectedButtons = new int[3];
    private Dictionary<string, int> d = new Dictionary<string, int>{
        {"soundmute", 0},  
        {"soundon", 1},  
        {"dialoguespeed1", 2},  
        {"dialoguespeed2", 3},
        {"dialoguespeed3", 4},
        {"walkingspeed1", 5},  
        {"walkingspeed2", 6},
        {"walkingspeed3", 7}   
    };
    void Start() {
        // Color in the buttons that were selected previously.
        for (int i = 0; i < 3; i++) {
            spriteRenderers[selectedButtons[i]].color = new Color(.6823f, .5568f, .5568f, 1);
        }
    }
    
    void Awake() {
        audioSource = cam.GetComponent<AudioSource>();

        // Get which buttons were selected prior to last app usage
        audioSource.mute = (PlayerPrefs.GetInt("soundmute", 0) != 0);
        selectedButtons[0] = PlayerPrefs.GetInt("selectedButton1", 1);
        selectedButtons[1] = PlayerPrefs.GetInt("selectedButton2", 2);
        selectedButtons[2] = PlayerPrefs.GetInt("selectedButton3", 5);
    }
    void OnApplicationPause(bool pauseState){
        // Save which buttons were selected.
        if (pauseState) {
            PlayerPrefs.SetInt("soundmute", ((audioSource.mute) ? 1 : 0));
            PlayerPrefs.SetInt("selectedButton1", selectedButtons[0]);
            PlayerPrefs.SetInt("selectedButton2", selectedButtons[1]);
            PlayerPrefs.SetInt("selectedButton3", selectedButtons[2]);
            PlayerPrefs.Save();
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
                    audioSource.mute = true;
                    spriteRenderers[0].color = new Color(.6823f, .5568f, .5568f, 1);
                } else if (gameObjectName == "soundon") {
                    audioSource.mute = false;
                    spriteRenderers[1].color = new Color(.6823f, .5568f, .5568f, 1);
                } 
                // Changing dialogue speed + Colors change of selected button
                else if (gameObjectName == "dialoguespeed1") {
                    DialogueBox.speakingSpeed = .07f;
                    spriteRenderers[2].color = new Color(.6823f, .5568f, .5568f, 1);

                } else if (gameObjectName == "dialoguespeed2") {
                    DialogueBox.speakingSpeed = .04f;
                    spriteRenderers[3].color = new Color(.6823f, .5568f, .5568f, 1);
                } else if (gameObjectName == "dialoguespeed3") {
                    DialogueBox.speakingSpeed = .01f;
                    spriteRenderers[4].color = new Color(.6823f, .5568f, .5568f, 1);
                }
                // Changing Walking Speed + Change color of button selected
                else if (gameObjectName == "walkingspeed1") {
                    PlayerManager.speed = 4;
                    spriteRenderers[5].color = new Color(.6823f, .5568f, .5568f, 1);
                } else if (gameObjectName == "walkingspeed2") {
                    PlayerManager.speed = 6;
                    spriteRenderers[6].color = new Color(.6823f, .5568f, .5568f, 1);
                } else if (gameObjectName == "walkingspeed3") {
                    PlayerManager.speed = 8;
                    spriteRenderers[7].color = new Color(.6823f, .5568f, .5568f, 1);
                }}
                // Makes all choices except the ones selected uncolored.
                // Additionally change the selected button in selectedButtons to choice.
                if (choice >= 0 && choice <= 1) {
                    selectedButtons[0] = choice;
                    for (int i = 0; i <= 1; i ++) {
                        if (i != choice)
                            spriteRenderers[i].color = Color.white;
                    }
                } else if (choice >= 2 && choice <= 4) {
                    selectedButtons[1] = choice;
                    for (int i = 2; i <= 4; i ++) {
                        if (i != choice)
                            spriteRenderers[i].color = Color.white;
                    }
                } else if (choice >= 5 && choice <= 7) {
                    selectedButtons[2] = choice;
                    for (int i = 5; i <= 7; i ++) {
                        if (i != choice)
                            spriteRenderers[i].color = Color.white;
                    }
                }
            }
        }

}