using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI gameOverTextbox; 
    public TextMeshProUGUI winLoseTextbox; 
    

    private SpriteRenderer sr;
  
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine("endGame");
        winLoseTextbox.text = "";

    }

    IEnumerator endGame(){
        yield return new WaitUntil(()=> SuspectSelect.suspectChoice != "");
        float t = 0; // time counter

        while(t< 3f) // limit duration to  2 seconds
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
        if (SuspectSelect.suspectChoice == "SarahThompson") {
            winLoseTextbox.color = new Color(0, 1, 0, 1);
            txt = "You did it! A killer is behind bars,\nand Enigma Springs is safe.";
        }
        // Adds each char to dialogue with split second pause
        foreach (char c in txt) {
            typed += c;
            yield return new WaitForSeconds(.08f);
            winLoseTextbox.text = typed;
        }
    }
} 
