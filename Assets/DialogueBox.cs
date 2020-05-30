using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{

    public string speaker;
    [TextArea(8,3)]
    public string[] dialogues = new string[5];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Can/will be replaced by whatever code we use to show that something has interacted with the player.
    void OnMouseDown(){

        // Checks to make sure no one else is already talking.
        if (!GameManagerScript.speaking)
        {          
            GameManagerScript.speaking = true; 

            GameManagerScript.textbox.SetActive(true); // show box
            GameManagerScript.speaker.text = this.speaker; // show name in box

            StartCoroutine("TypeDialogue"); 

            }



    }

    // Controls how the typing effect is created to show dialogue.
    IEnumerator TypeDialogue() {
        string txt = "";
        GameManagerScript.dialogue.text = txt;

        foreach(char c in dialogues[GameManagerScript.day - 1]) {
  
            // The $ is used as a newbox (like newline) character.
            // If $ - clear box
            if (c == '$') {
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // Wait for mouseclick on screen
                txt = "";
                GameManagerScript.dialogue.text = txt;
            }
            else {     
                if (c != ' ')
                    yield return new WaitForSeconds(.05f); 
                txt += c;
                GameManagerScript.dialogue.text = txt;
                // add char to textbox.
            }
        }

        //Wait for mouseclick on screen to end.
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        // Clears everything. 
        GameManagerScript.speaker.text = "";
        GameManagerScript.dialogue.text = "";
        GameManagerScript.textbox.SetActive(false);
        GameManagerScript.speaking = false;

    }
}
