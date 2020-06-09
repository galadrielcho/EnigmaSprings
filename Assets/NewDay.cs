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
        night.enabled = true;
        string txt = "Welcome to Enigma Springs, a quiet mining town. After the mine owner," +
        " Richard Hawthorne, was found murdered, unrest has risen among the townspeople." +
        " Talk to them as Detective Eman, and gather clues!" +
        " After 5 days, you must decide the culprit. Good luck!";
        StartCoroutine(Type(txt, IntroText, true));
    }

    void Update()
    {
        if(GameManagerScript.speaking && Input.GetKeyDown("y") && Vector3.Distance(player.position, transform.position) < 2) {
            GameManagerScript.speaker.text = "";
            GameManagerScript.dialogue.text = "";
            GameManagerScript.textbox.SetActive(false);
            StartCoroutine("FadeIn");
        }
    }

    IEnumerator Type(string txt, TextMeshProUGUI txtbox, bool fade) {

        string typed = "";
        foreach (char c in txt) {
            txtbox.text = typed;
            yield return new WaitForSeconds(.08f);
            typed += c;
        }

        yield return new WaitForSeconds(.08f);
        txtbox.text = typed;

        if (fade) {
            yield return new WaitForSeconds(1f);
            StartCoroutine("FadeOut", true);
        }


    }

    IEnumerator FadeIn() {
        t = 0;
        while(t< 2f) {
            t += Time.deltaTime;

            float alpha = Mathf.Lerp(0, 1,t/2f);
            night.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        StartCoroutine("FadeOut", false);


    }

    IEnumerator FadeOut(bool intro) {
        GameManagerScript.speaking = false;
        GameManagerScript.day += 1;

        t = 0;
        while(t< 2f) {
            t += Time.deltaTime;

            float alpha = Mathf.Lerp(1,0,t/2f);
            night.color = new Color(0, 0, 0, alpha);
            if (intro) {
               IntroText.color = new Color(1, 1, 1, alpha);}
            yield return null;
        }
        StartCoroutine(Type("Day " + GameManagerScript.day, DayText, false));

    }
} 

