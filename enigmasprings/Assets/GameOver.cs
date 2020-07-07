using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI gameOverTextbox; 
    public TextMeshProUGUI winLoseTextbox; 
    public TextMeshProUGUI nameTextboxField; 
    public Transform nameTransformField;
    public TextMeshProUGUI Heading; 
    public TextMeshProUGUI Names; 

    public static Transform nameTransform;
    public static AudioSource click;
    public static TextMeshProUGUI nameTextbox;
    public List<SpriteRenderer> characters = new List<SpriteRenderer>();
    private SpriteRenderer sr;
  
    // Start is called before the first frame update
    void Start()
    {
        click = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        nameTextbox = nameTextboxField;
        nameTransform = nameTransformField;
        StartCoroutine("endGame");
        winLoseTextbox.text = "";

    }


    IEnumerator endGame(){
        float t = 0; // time counter

        //fade in each suspect, one at a time
        foreach (SpriteRenderer sr in  characters) {
            t = 0;
            while(t<.5f){

                t += Time.deltaTime; // time passed added to counter
                float alpha = Mathf.Lerp(0, 1,t/.5f);
                sr.color = new Color(1, 1, 1, alpha); 
                yield return null;

            }

            yield return new WaitForSeconds (.05f);
        }

        yield return new WaitUntil(()=> SuspectSelect.suspectChoice != "");
      
        t = 0; // time counter
        while(t< 2f) // limit duration to  2 seconds
        {
            t += Time.deltaTime; // time passed added to counter

            float alpha = Mathf.Lerp(0, 1,t/2f);  // calculate transparency over time 

            winLoseTextbox.color = new Color(1, 0, 0, alpha);
            gameOverTextbox.color = new Color(1, 1, 1, alpha);
            sr.color = new Color(0, 0, 0, alpha); 

            yield return null;
        }

        string typed = "";
        string txt = "You doomed an innocent to a life of misery.\nA killer runs free.";
        if (SuspectSelect.suspectChoice == "Sarah Thompson") {
            winLoseTextbox.color = new Color(0, 1, 0, 1);
            txt = "You did it! A killer is behind bars,\nand Enigma Springs is safe.";
        }

        // Adds each char to dialogue with split second pause
        foreach (char c in txt) {
            typed += c;
            yield return new WaitForSeconds(.08f);
            winLoseTextbox.text = typed;
        }

        yield return new WaitUntil(()=> Input.GetMouseButton(0));

        StartCoroutine(FadeOutText(gameOverTextbox));
        StartCoroutine(FadeOutText(winLoseTextbox));
        yield return new WaitForSeconds(1.5f);


        string[] headings = {"Programming", "Art", "Story", "Music", "Music"};

        string[] names = {
            "Lorien Cho\nGaladriel Cho\nAlessandro Martinez",
            "Luis Zuno (@ansimuz)\nLorien Cho",
            "Galadriel Cho\nAlessandro Martinez\nLorien Cho",
            "The Path of the Goblin King\nby Kevin MacLeod\nLink:" +
            "https://incompetech.filmmusic.io/\nsong/4503-the-path-of-the-goblin-king" +
            "\nLicense:http://creativecommons.org/licenses/by/4.0/",
            "Classic Horror 1 by Kevin MacLeod\nLink: " +
            "https://incompetech.filmmusic.io/\nsong/3511-classic-horror-1" +
            "\nLicense: http://creativecommons.org/licenses/by/4.0/"
        };

        for (int i =0; i < 5; i++) {
            Heading.text = headings[i];
            Names.text = names[i];
            StartCoroutine(FadeInText(Heading));
            StartCoroutine(FadeInText(Names));
            yield return new WaitForSeconds(1f);
            StartCoroutine(FadeOutText(Heading));
            StartCoroutine(FadeOutText(Names));
            yield return new WaitForSeconds(1f);



        }


    }

    IEnumerator FadeInText(TextMeshProUGUI txtbox) {
        float t = 0; // time counter

        while(t< 1f) // limit duration to  2 seconds
        {
            t += Time.deltaTime; // time passed added to counter

            float alpha = Mathf.Lerp(0, 1,t/1f);  // calculate transparency over time 

            txtbox.color = new Color(txtbox.color.r, txtbox.color.g, txtbox.color.b, alpha);
            yield return null;
        }
    }

    IEnumerator FadeOutText(TextMeshProUGUI txtbox) {
        float t  = 0;
        while(t< 1f) // limit duration to  2 seconds
        {
            t += Time.deltaTime; // time passed added to counter

            float alpha = Mathf.Lerp(1, 0, t/1f);  // calculate transparency over time 

            txtbox.color = new Color(txtbox.color.r, txtbox.color.g, txtbox.color.b, alpha);
            yield return null;
        }
    }
} 
