﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{

    public string speaker;
    [TextArea(8,3)]
    public string[] dialogues = new string[5];
    public Transform player;
    public GameObject popup;
    private float dist;
    public static Coroutine co;
    private bool stop = false;
    public GameObject interactor;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ProximityCheck");
    }

    // Can/will be replaced by whatever code we use to show that something has interacted with the player.
    void Speak(){

        // Checks to make sure no one else is already talking.
        if (!GameManagerScript.speaking)
        {          
            GameManagerScript.speaking = true; 

            GameManagerScript.textbox.SetActive(true); // show box
            GameManagerScript.speaker.text = this.speaker; // show name in box
            co = StartCoroutine(TypeDialogue()); 

            }
    }
    void Update()
    {
        
        dist = Vector3.Distance(player.position, transform.position);
        if(Input.touchCount >= 1 || Input.anyKeyDown) stop = true;


    }

    IEnumerator ProximityCheck() {
        while (true){
            if (dist < 2 && (GameManagerScript.hitInfo.collider.name == "selector")) {
                Speak();
            }
            else if (dist < 2)
            {
                popup.transform.position = transform.position;
                popup.transform.Translate(Vector3.up);
                popup.SetActive(true);
                interactor.SetActive(true);

                while (dist < 2) {
                    if (dist < 2 && (GameManagerScript.hitInfo.collider.name == "selector")) {
                        Speak();
                        popup.SetActive(false);
                        interactor.SetActive(false);

                    }
                    yield return null;
                }
                interactor.SetActive(false);
                popup.SetActive(false);
            }
                yield return null;

        }
    }

    // Controls how the typing effect is created to show dialogue.
    IEnumerator TypeDialogue() {
        stop = false;
        string txt = "";
        GameManagerScript.dialogue.text = txt;

        foreach(char c in dialogues[GameManagerScript.day - 1]) {
  
            // The $ is used as a newbox (like newline) character.
            // If $ - clear box
            if (c == '$') {
                yield return new WaitUntil(() => Input.touchCount >= 1|| Input.GetKeyDown("space")); // Wait for mouseclick on screen
                stop=false;
                txt = "";
                GameManagerScript.dialogue.text = txt;
            }
            else {     
                if (c != ' ' && !stop)
                    yield return new WaitForSeconds(.07f); 
                txt += c;
                GameManagerScript.dialogue.text = txt;
                // add char to textbox.
            }
        }

        //Wait for mouseclick on screen to end.
        yield return new WaitUntil(() =>Input.touchCount >= 1 || Input.GetKeyDown("space"));

        // Clears everything. 
        GameManagerScript.speaker.text = "";
        GameManagerScript.dialogue.text = "";
        GameManagerScript.textbox.SetActive(false);
        GameManagerScript.speaking = false;

    }
}
