﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    public static float speed;
    private SpriteRenderer sR;
    public Sprite[] heroArray;
    public Camera mainCamera;
    public Animator animator;
    private string touchedObject;

    void Start()
    {
        sR = GetComponent<SpriteRenderer>();

    }
    void Awake() {
        float x =PlayerPrefs.GetFloat("x", 0f);
        float y =PlayerPrefs.GetFloat("y", 0f);
        Vector2 v = new Vector2(x,y);
        transform.position = v;
        speed = PlayerPrefs.GetFloat("speed", 3f);

    }
    void OnApplicationPause(bool pauseState){
        if (pauseState) {
            PlayerPrefs.SetFloat("speed", speed);
            PlayerPrefs.Save();
        }

    }

    public void test() {
                float x =PlayerPrefs.GetFloat("x", 0f);
        float y =PlayerPrefs.GetFloat("y", 0f);
        Vector2 v = new Vector2(x,y);
        transform.position = v;


    }

    public void save() {
                    Debug.Log("asdf");

    PlayerPrefs.SetFloat("x", transform.position.x);
            PlayerPrefs.SetFloat("y", transform.position.y);
            PlayerPrefs.Save();
            Debug.Log("asdf");


    }
    // void onApplicationPause(bool pauseState) {
    //     if (pauseState) {
    //         PlayerPrefs.SetFloat("x", transform.position.x);
    //         PlayerPrefs.SetFloat("y", transform.position.y);
    //         PlayerPrefs.Save();
    //     }
  


    // }
    void FixedUpdate()
    {


        if (!GameManagerScript.speaking && Input.touchCount > 0 ) {
            animator.speed = 1;


            // detect if raycast hit a movement control button collider
            if (GameManagerScript.hitInfo.collider != null){    

                // Player moves and looks up if up button is touched
                if (GameManagerScript.touchedObject == "up") {
                        sR.flipX = false;
                        animator.SetInteger("direction", 1);
                        transform.Translate(Vector3.up * Time.deltaTime * speed);
                    }
                // Player moves and looks down if down button is touched 
                else if (GameManagerScript.touchedObject == "down"){
                        sR.flipX = false;
                        animator.SetInteger("direction", 2);
                        transform.Translate(Vector3.down * Time.deltaTime * speed);
                    }
                // Player moves and looks left if left button is touched
                else if (GameManagerScript.touchedObject == "left") {
                        sR.flipX = false;
                        animator.SetInteger("direction", 3);
                        transform.Translate(Vector3.left * Time.deltaTime * speed); 
                        }
                // Player moves and looks right if right button is touched
                else if (GameManagerScript.touchedObject == "right") {
                        animator.SetInteger("direction", 3);
                        sR.flipX = true;
                        transform.Translate(Vector3.right * Time.deltaTime * speed); 

                    }
                else {
                        animator.SetInteger("direction", 0);
                        int direction = animator.GetInteger("direction");
                        animator.speed = 0;
                        sR.sprite = heroArray[direction];

                    }
            }

        } else {
            animator.speed = 0;
        }
        
    }
}
