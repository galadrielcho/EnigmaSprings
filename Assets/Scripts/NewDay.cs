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
    public GameObject popup;
    public GameObject interactor;
    public GameObject nextDay;

    private float dist;
    private float t;
    private bool stop;
    private bool delay;
    public static bool intro;
    
    // Update is called once per frame
    void Start() {
        intro = true;
        SystemText.text="";
        delay = true;
        night.enabled = true; //Makes the start screen black.
        nextDay.SetActive(false);
        stop = false;
        t= 0;
        if (!PlayerPrefs.HasKey("intro")){

            skip.text = "tap anywhere to skip";

            string txt = "Welcome to Enigma Springs,\na quiet mining town. After the mine owner,\n" +
            " Richard Hawthorne, was found murdered,\nunrest has risen among the townspeople.\n" +
            " Talk to them as Detective Cooper, and\n gather clues!" +
            " Go to the inn when you are\n finished talking to others for the day.\n" +
            " After 5 days, you must\n decide the culprit. Good luck!"; //store intro text

        
            StartCoroutine(Type(txt, SystemText, true)); //Intro text is typed on screen
            // txt = what is typed. SystemText = the textbox used true = tells function part of intro
            PlayerPrefs.SetInt("intro", 1);
            PlayerPrefs.Save();
            StartCoroutine("Delay");
        }
        else {
            StartCoroutine(FadeOut(false));
        }
        
        if (!PlayerPrefs.HasKey("KillerSelect")) StartCoroutine("ProximityCheck");
    }

    void Update()
    {
        dist = Vector3.Distance(player.position, transform.position);
        // trying to make sure you can only change days at inn door.
        // later - maybe make it dependent on speaker name instead.
        if(GameManagerScript.speaking && Input.GetKeyDown("y") && Vector3.Distance(player.position, transform.position) < 2) {
            GameManagerScript.ClearTextbox();
            StartCoroutine("FadeIn"); //screen turns black
            StopCoroutine(DialogueBox.co);

        }
        if(Input.touchCount >= 1 && !delay) stop = true;

    }

    IEnumerator Delay() {
        yield return new WaitForSeconds(.5f);
        delay = false;
    }
    IEnumerator ProximityCheck() {
        while (true){
            if (dist < 2 && (GameManagerScript.getTappedObject() == "interact")) {
                StartCoroutine("Ask");
            }
            else if (dist < 2)
            {
                popup.transform.position = transform.position;
                popup.transform.Translate(Vector3.up);
                popup.SetActive(true);
                interactor.SetActive(true);

                while (dist < 2) {
                    if (dist < 2 && (GameManagerScript.getTappedObject() == "interact")) {
                        StartCoroutine("Ask");
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

    IEnumerator Ask(){
        GameManagerScript.speaking = true;
        nextDay.SetActive(true);
        yield return new WaitUntil(() => (GameManagerScript.tappedObject == "YesStart" || GameManagerScript.tappedObject=="NoStart"));
        nextDay.SetActive(false);
        if (GameManagerScript.getTappedObject() == "YesStart") {
            StartCoroutine("FadeIn"); //screen turns black

        }
        else {
            GameManagerScript.speaking = false;
        }
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

        GameManagerScript.day += 1; //Changed to next day
        nextDay.SetActive(false);
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
        intro = false;
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
        GameManagerScript.speaking =false;

    }
}


