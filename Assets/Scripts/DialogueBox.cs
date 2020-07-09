using System.Collections;
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
    public static float speakingSpeed;
    private bool stall;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ProximityCheck");
    }
    void Awake() {

        speakingSpeed = PlayerPrefs.GetFloat("speakingSpeed", .07f);

    }
    void OnApplicationPause(bool pauseState){
        if (pauseState) {
            PlayerPrefs.SetFloat("speakingSpeed", speakingSpeed);
            PlayerPrefs.Save();
        }

    }

    // Can/will be replaced by whatever code we use to show that something has interacted with the player.S
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
        if(!stall && GameManagerScript.speaking && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
            stop = true;
            stall = true;
            StartCoroutine("StallTap"); 
        }


    }

    IEnumerator StallTap() {
        yield return new WaitForSeconds(.5f);
        stall = false;

    }
    IEnumerator ProximityCheck() {
        while (true){
            if (dist < 2 && (GameManagerScript.getTappedObject() == "interact")) {
                Speak();
            }
            else if (dist < 2)
            {
                popup.transform.position = transform.position;
                popup.transform.Translate(Vector3.up);
                popup.SetActive(true);
                interactor.SetActive(true);

                while (dist < 2) {
                    if (dist < 2 && (GameManagerScript.getTappedObject() == "interact")) {
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
                yield return new WaitUntil(() => (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)); // Wait for tap on screen
                stop=false;
                txt = "";
                GameManagerScript.dialogue.text = txt;
                yield return new WaitForSeconds(.5f);
            }
            else {     
                if (c != ' ' && !stop)
                    yield return new WaitForSeconds(speakingSpeed); 
                txt += c;
                GameManagerScript.dialogue.text = txt;
                // add char to textbox.
            }
        }

        //Wait for mouseclick on screen to end.
        yield return new WaitUntil(() => (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended));

        // Clears everything. 
        GameManagerScript.speaker.text = "";
        GameManagerScript.dialogue.text = "";
        GameManagerScript.textbox.SetActive(false);
        GameManagerScript.speaking = false;

    }
}
