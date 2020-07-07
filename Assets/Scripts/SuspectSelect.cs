using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectSelect : MonoBehaviour
{

    
    static public string suspectChoice = "";
    // Start is called before the first frame update
    private SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }
    void OnMouseDown()
    {
        suspectChoice = name;
    }

    void OnMouseEnter()
    { 
        if (suspectChoice == "") {
            GameOver.click.Play();
            GameOver.nameTextbox.text = name;
            sr.color = new Color(1, 1, 1, .7f); 
        }
        
    }

    void OnMouseOver() {
        if (suspectChoice == "") {
            GameOver.nameTransform.position = Input.mousePosition;
        }
        else {
            GameOver.nameTextbox.text = "";

        }

    }

    void OnMouseExit()
    {
        sr.color = new Color(1, 1, 1, 1); 
        GameOver.nameTextbox.text = "";

    }

}
