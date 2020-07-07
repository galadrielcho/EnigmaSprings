using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class NewDay : MonoBehaviour
{

    public Transform player;
    public SpriteRenderer night;
    public TextMeshProUGUI SystemText;
    public TextMeshProUGUI DayText;
    public AudioSource music;
    public TextMeshProUGUI skip;
    public GameObject movementController;

    private float t = 0;
    private bool stop = false;
    
    // Update is called once per frame
    void Start() {
        skip.text = "tap anywhere to skip";
        night.enabled = true; //Makes the start screen black.

        string txt = "Welcome to Enigma Springs,\na quiet mining town. After the mine owner,\n" +
        " Richard Hawthorne, was found murdered,\nunrest has risen among the townspeople.\n" +
        " Talk to them as Detective Cooper, and\n gather clues!" +
        " After 5 days, you must\n decide the culprit. Good luck!"; //store intro text

    
        StartCoroutine(Type(txt, SystemText, true)); //Intro text is typed on screen
        // txt = what is typed. SystemText = the textbox used true = tells function part of intro
    }

    void Update()
    {
        // trying to make sure you can only change days at inn door.
        // later - maybe make it dependent on speaker name instead.
        if(GameManagerScript.speaking && Input.GetKeyDown("y") && Vector3.Distance(player.position, transform.position) < 2) {
            GameManagerScript.ClearTextbox();
            StartCoroutine("FadeIn"); //screen turns black
            StopCoroutine(DialogueBox.co);

        }
         if(Input.touchCount >= 1 || Input.anyKeyDown) stop = true;

    }

    // txt: text to be typed 
    // txtbox: textbox to edit
    // fade: whether black bg disappears or not
    IEnumerator Type(string txt, TextMeshProUGUI txtbox, bool fade) {
        stop=false;
        string typed = "";
        // Adds each char to dialogue with split second pause
        foreach (char c in txt) {

            typed += c;
            if (!stop) yield return new WaitForSeconds(.08f);
            txtbox.text = typed;
        }


        // text and/or related black bg fade
        if (fade) {
            yield return new WaitForSeconds(1f);
            StartCoroutine("FadeOut", true);
        }

        stop = false;
        skip.text="";


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

        GameManagerScript.speaking = false; 
        GameManagerScript.day += 1; //Changed to next day
        
        if (GameManagerScript.day == 6)
        {
            string lastdaytext = "Your time in Enigma Springs has come to an end.\n    You must pick the killer.\n             Choose wisely,\nThe town is counting on you...";
            SystemText.text = "";
            SystemText.color = new Color(1, 1, 1, 1);
            StartCoroutine(Type("Day " + GameManagerScript.day, DayText, false));
            StartCoroutine(Type(lastdaytext, SystemText, true));
        }
        else {
            StartCoroutine("FadeOut", false); // fade out 

        }
    }

    // Black background fades out over screen
    // intro - whether to fade out systemtext or not
    IEnumerator FadeOut(bool systemtext) {
        t = 0; //Reset counter
        while(t< 2f) {
            t += Time.deltaTime;


            float alpha = Mathf.Lerp(1,0,t/2f);
            if (systemtext){
                SystemText.color = new Color(1, 1, 1, alpha); // Intro system text fades out
            }
            if (GameManagerScript.day != 6) { //If it's the last day, prevent the black bg from fading out
                night.color = new Color(0, 0, 0, alpha);
            }    
            yield return null;
        }
        if (GameManagerScript.day == 6) {

            t = 0; 
            while(t< 2f) {
                t += Time.deltaTime;
                music.volume = Mathf.Lerp(.239f, 0, t/2f);
                yield return null;
            }

            float alpha = Mathf.Lerp(1,0,t/2f);
            SceneManager.LoadScene("KillerSelect");
        }
        else {
            StartCoroutine(Type("Day " + GameManagerScript.day, DayText, false));

        }

    }
}


