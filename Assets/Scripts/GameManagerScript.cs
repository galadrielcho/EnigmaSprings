using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
 
public class GameManagerScript : MonoBehaviour
{
    public static int day=1;
        
    
    // Static variables cannot be assigned in inspector - so we have dummy variables here.
    public TextMeshProUGUI field_dialogue; 
    public TextMeshProUGUI field_speaker; 
    public GameObject field_textbox;
 
    public static TextMeshProUGUI dialogue; 
    public static TextMeshProUGUI speaker; 
    public static GameObject textbox;
    public static RaycastHit2D hitInfo;
    public static string suspectChoice = "";
    public static string touchedObject ="";
    public static string tappedObject ="";

 
    // Used to make sure no more than one person is using the textbox at a time.
    public static bool speaking = false;

    private Vector3 touchPosWorld;

 
    // Start is called before the first frame update
    void Start()
    {
        // Dummy variables are replaced
        dialogue = field_dialogue;
        speaker = field_speaker;
        textbox = field_textbox;
    }
 
    public static void ClearTextbox() {
 
        // Clear textbox
        speaker.text = "";
        dialogue.text = "";
        textbox.SetActive(false);
    }

    void Update() {
        // Fire raycast at touch position
        if (Input.touchCount > 0) {
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosWorld2D= new Vector2(touchPosWorld.x, touchPosWorld.y);
            hitInfo = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (TouchPhase.Ended == Input.GetTouch(0).phase) {
                tappedObject = hitInfo.collider.name;

            }
            else {
                touchedObject = hitInfo.collider.name;
            }

        }

    }

    public static string getTappedObject() {
        string tO = tappedObject;
        tappedObject = "";
        return tO;
    }
}

 

