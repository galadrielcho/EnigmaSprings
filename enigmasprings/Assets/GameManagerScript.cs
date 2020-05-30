using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static int day=1;
    public TextMeshProUGUI field_dialogue; 
    public TextMeshProUGUI field_speaker; 
    public GameObject field_textbox;
    public static TextMeshProUGUI dialogue; 
    public static TextMeshProUGUI speaker; 
    public static GameObject textbox;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = field_dialogue;
        speaker = field_speaker;
        textbox = field_textbox;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
