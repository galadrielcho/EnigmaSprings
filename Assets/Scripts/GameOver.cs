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
    public TextMeshProUGUI instruction; 
    public TextMeshProUGUI instruction2; 
    public GameObject yesno; 
    public GameObject confirm;
    public static Transform nameTransform;
    public static AudioSource click;
    public static TextMeshProUGUI nameTextbox;
    public List<SpriteRenderer> characters = new List<SpriteRenderer>();
    private SpriteRenderer sr;
    private bool freeze= true;

    public static RaycastHit2D hitInfo;
    private static string tappedObject ="no";
    public Dictionary<string, int> d = new Dictionary<string, int>{
        {"Tommy Carter", 0},  
        {"Sheriff Neil Baker", 1},  
        {"Clayton Hawthorne", 2},  
        {"Sarah Thompson", 3},
        {"Mayor Roy Roberts", 4}  
    };

    private Vector3 touchPosWorld;
  
    // Start is called before the first frame update
    void Start()
    {
        yesno.SetActive(false);
        confirm.SetActive(false);
        click = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        nameTextbox = nameTextboxField;
        nameTransform = nameTransformField;
        StartCoroutine("endGame");
        winLoseTextbox.text = "";

    }

    
    void Update() {
        // Fire raycast at touch position
        if (Input.touchCount > 0 && TouchPhase.Ended == Input.GetTouch(0).phase) {
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosWorld2D= new Vector2(touchPosWorld.x, touchPosWorld.y);
            hitInfo = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (tappedObject != "yes" && tappedObject != "no" && !freeze|| tappedObject=="no"&&freeze) {
                characters[d[tappedObject]].color = new Color(1, 1, 1, 1); 
            }
            tappedObject = hitInfo.collider.gameObject.name;
            if (tappedObject != "yes" && tappedObject != "no" && !freeze) {
                nameTextbox.text = tappedObject;
                nameTransform.position = hitInfo.transform.position;
                nameTransform.Translate(Vector3.down * 1.5f);
                nameTransform.Translate(Vector3.left * 2f);

                characters[d[tappedObject]].color = new Color(1, 1, 1, .7f); 
            }

            

        }

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
        freeze=false;
        while (tappedObject!= "yes"){
            yield return new WaitUntil(()=> tappedObject != "yes" && tappedObject != "no");
            click.Play();
            freeze = true;
            yesno.SetActive(true);
            confirm.SetActive(true);
            yield return new WaitUntil(()=> tappedObject == "yes" || tappedObject == "no");
            click.Play();   
            freeze= false;
            nameTextbox.text="";
            confirm.SetActive(false);
            yesno.SetActive(false);


        }
        nameTextbox.text="";
        yesno.SetActive(false);
        freeze=true;
        confirm.SetActive(false);
        t = 0; // time counter
        while(t< 2f) // limit duration to  2 seconds
        {
            t += Time.deltaTime; // time passed added to counter

            float alpha = Mathf.Lerp(0, 1,t/2f);  // calculate transparency over time 
            instruction.color = new Color(1, 1, 1, 1-alpha);
            winLoseTextbox.color = new Color(1, 0, 0, alpha);
            gameOverTextbox.color = new Color(1, 1, 1, alpha);
            sr.color = new Color(0, 0, 0, alpha); 

            yield return null;
        }
        


        string typed = "";
        string txt = "You doomed an innocent to a life of misery.\nA killer runs free.";
        if (nameTextbox.text == "Sarah Thompson") {
            winLoseTextbox.color = new Color(0, 1, 0, 1);
            txt = "You did it! A killer is behind bars,\nand Enigma Springs is safe.";
        }

        // Adds each char to dialogue with split second pause
        foreach (char c in txt) {
            typed += c;
            yield return new WaitForSeconds(.08f);
            winLoseTextbox.text = typed;
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeInText(instruction2));

        yield return new WaitUntil(()=> Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended);

        StartCoroutine(FadeOutText(instruction2));
        StartCoroutine(FadeOutText(gameOverTextbox));        
        StartCoroutine(FadeOutText(winLoseTextbox));
        yield return new WaitForSeconds(1.5f);


        string[] headings = {"Programming", "Art", "Story", "Music", "Music"};

        string[] names = {
            "Lorien Cho\nGaladriel Cho",
            "Luis Zuno (@ansimuz)\nLorien Cho",
            "Galadriel Cho\nLorien Cho",
            "The Path of the Goblin King\nby Kevin MacLeod\nLink:" +
            "https://incompetech.filmmusic.io\n/song/4503-the-path-of-the-goblin-king" +
            "\nLicense:http://creativecommons.org/licenses/by/4.0/",
            "Classic Horror 1 by Kevin MacLeod\nLink: " +
            "https://incompetech.filmmusic.io/\nsong/3511-classic-horror-1" +
            "\nLicense:http://creativecommons.org\n/licenses/by/4.0/",
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
        yield return new WaitForSeconds(1f);

        Names.text="Thank you for playing.";
        StartCoroutine(FadeInText(Names));
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeOutText(Names));


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
