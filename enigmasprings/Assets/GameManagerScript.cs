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

    public static string suspectChoice = "";
 
    // Used to make sure no more than one person is using the textbox at a time.
    public static bool speaking = false;
 
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
}

 

