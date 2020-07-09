using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsScript : MonoBehaviour
{
    public Camera cam;
    private AudioSource audioSource;

    public GameObject player;
    public GameObject hideOrShow;
    private int choice;
    public SpriteRenderer[] spriteRenderers = new SpriteRenderer[8];
    private int[] selectedButtons = new int[3];

    void Start() {
        // Color in the buttons that were selected previously.
        for (int i = 0; i < 3; i++) {
            spriteRenderers[selectedButtons[i]].color = new Color(.6823f, .5568f, .5568f, 1);
        }
    }
    
    void Awake() {
        choice = -1;
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
            PlayerPrefs.SetInt("selectedButton1", (audioSource.mute ? 0 : 1));
            PlayerPrefs.SetInt("selectedButton2", (DialogueBox.speakingSpeed == .07f) ? 2 : (DialogueBox.speakingSpeed == .04f) ? 3 : 4);
            PlayerPrefs.SetInt("selectedButton3", (PlayerManager.speed == 4) ? 5 : (PlayerManager.speed == 6) ? 6 : 7);
            PlayerPrefs.Save();
        }

    }
    void FixedUpdate()
    {
        // Check for touches and then detect if raycast hit collider and if the touched object is itself
        if (Input.touchCount > 0 && GameManagerScript.hitInfo.collider != null && !NewDay.intro) {
            string gameObjectName = GameManagerScript.touchedObject;
            // Settings button or 'x' button
            if ((gameObjectName == "settings" && !hideOrShow.activeSelf)|| (gameObjectName == "x" && hideOrShow.activeSelf)) {
                // Hide or show bg, options, and title for settings screen
                hideOrShow.SetActive(!hideOrShow.activeSelf);
                GameManagerScript.speaking = hideOrShow.activeSelf;

            } else {
                // Mute sound button or sound on button 
                 if (gameObjectName == "soundmute"){
                    choice = 0;
                    audioSource.mute = true;
                } else if (gameObjectName == "soundon") {
                    choice = 1;
                    audioSource.mute = false;
                } 
                // Changing dialogue speed 
                else if (gameObjectName == "dialoguespeed1") {
                    choice = 2;
                    DialogueBox.speakingSpeed = .07f;

                } else if (gameObjectName == "dialoguespeed2") {
                    choice = 3;
                    DialogueBox.speakingSpeed = .04f;
                } else if (gameObjectName == "dialoguespeed3") {
                    choice = 4;
                    DialogueBox.speakingSpeed = .02f;
                }
                // Changing Walking Speed
                else if (gameObjectName == "walkingspeed1") {
                    choice = 5;
                    PlayerManager.speed = 4;
                } else if (gameObjectName == "walkingspeed2") {
                    choice = 6;
                    PlayerManager.speed = 6;
                } else if (gameObjectName == "walkingspeed3") {
                    choice = 7;
                    PlayerManager.speed = 8;
                }
                else if (gameObjectName == "reset") {
                    PlayerPrefs.DeleteAll();
                    choice = 0;
                    spriteRenderers[2].color = new Color(.6823f, .5568f, .5568f, 1);
                    spriteRenderers[5].color = new Color(.6823f, .5568f, .5568f, 1);
                    SceneManager.LoadScene("town");

                }
                }
                // Changes color of the button to denote that it is selected.
                spriteRenderers[choice].color = new Color(.6823f, .5568f, .5568f, 1);

                // Makes all choices except the ones selected uncolored.
                // Additionally changes selectedButtons value in slot of either sound, dialogue speed, or walking speed to value of selected button.
                
                // Sound mute or on buttons
                if (choice >= 0 && choice <= 1) {
                    for (int i = 0; i <= 1; i ++) {
                        if (i != choice)
                            spriteRenderers[i].color = Color.white;
                    }
                }
                if (gameObjectName == "reset") choice = 2;

                // Dialogue speed buttons
                if (choice >= 2 && choice <= 4) {
                    for (int i = 2; i <= 4; i ++) {
                        if (i != choice)
                            spriteRenderers[i].color = Color.white;
                    }
                }
                if (gameObjectName == "reset") choice = 5;

                // Walking speed butons
                if (choice >= 5 && choice <= 7) {
                    for (int i = 5; i <= 7; i ++) {
                        if (i != choice)
                            spriteRenderers[i].color = Color.white;
                    }
                }
            }
        }

}