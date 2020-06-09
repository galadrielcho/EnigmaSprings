using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewDay : MonoBehaviour
{

    public Transform player;
    public SpriteRenderer night;
    public TextMeshProUGUI IntroText;
    public TextMeshProUGUI DayText; 
    private float t = 0;
    
    // Update is called once per frame
    void Start() {
        night.enabled = true; //Makes the start screen black.

        string txt = "Welcome to Enigma Springs,\na quiet mining town. After the mine owner,\n" +
        " Richard Hawthorne, was found murdered,\nunrest has risen among the townspeople.\n" +
        " Talk to them as Detective Eman, and\n gather clues!" +
        " After 5 days, you must\n decide the culprit. Good luck!"; //store intro text


        StartCoroutine(Type(txt, IntroText, true)); //Intro text is typed on screen
        // txt = what is typed. IntroText = the textbox used true = tells function part of intro
    }

    void Update()
    {
        // trying to make sure you can only change days at inn door.
        // later - maybe make it dependent on speaker name instead.
        if(GameManagerScript.speaking && Input.GetKeyDown("y") && Vector3.Distance(player.position, transform.position) < 2) {
            GameManagerScript.ClearTextbox();
            StartCoroutine("FadeIn"); //screen turns black
        }
    }

    // txt: text to be typed 
    // txtbox: textbox to edit
    // fade: whether black bg disappears or not
    IEnumerator Type(string txt, TextMeshProUGUI txtbox, bool fade) {
        string typed = "";

        // Adds each char to dialogue with split second pause
        foreach (char c in txt) {
            typed += c;
            yield return new WaitForSeconds(.08f);
            txtbox.text = typed;
        }


        if (fade) {
            // Black background disappears
            yield return new WaitForSeconds(3f);
            StartCoroutine("FadeOut", true);
        }


    }

    // Black background fades in over screen
    IEnumerator FadeIn() {
        t = 0; // time counter

        while(t< 2f) // limit duration to  2 seconds
        {
            t += Time.deltaTime; // time passed added to counter

            float alpha = Mathf.Lerp(0, 1,t/2f);  // calculate transparency over time 
            night.color = new Color(0, 0, 0, alpha); 
            yield return null;
        }

        StartCoroutine("FadeOut", false); // fade out 


    }

    // Black background fades out over screen
    // intro - whether to fade out intro text or not
    IEnumerator FadeOut(bool intro) {
        GameManagerScript.speaking = false; 
        GameManagerScript.day += 1; //Changed to next day

        t = 0; //Reset counter


        while(t< 2f) {
            t += Time.deltaTime;

            float alpha = Mathf.Lerp(1,0,t/2f);
            night.color = new Color(0, 0, 0, alpha);

            if (intro) { //fade out intro text as well
                IntroText.color = new Color(1, 1, 1, alpha);
            }
    
            yield return null;
        }

        //Type out Day text
        StartCoroutine(Type("Day " + GameManagerScript.day, DayText, false));

    }
} 

