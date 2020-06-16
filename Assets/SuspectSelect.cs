using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspectSelect : MonoBehaviour
{

    // Start is called before the first frame update
    private SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }
    void OnMouseDown()
    {
        GameManagerScript.suspectChoice = name;
    }

    void OnMouseEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
        sr.color = new Color(1, 1, 1, .9f); 
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        sr.color = new Color(1, 1, 1, 1); 
    }

}
