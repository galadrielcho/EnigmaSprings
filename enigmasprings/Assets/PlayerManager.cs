using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    public float speed = 10f;
    private SpriteRenderer sR;
    public Sprite[] heroArray;
    public Camera mainCamera;
    public Animator animator;
    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
   

    }


    void FixedUpdate()
    {

        if (!GameManagerScript.speaking) {
            animator.speed = 1;
            // Player moves and looks up if up arrow or 'w' key is hit
            if (Input.GetKey(KeyCode.UpArrow)  ||  Input.GetKey(KeyCode.W)) {
                    sR.flipX = false;
                    animator.SetInteger("direction", 1);
                    transform.Translate(Vector3.up * Time.deltaTime * speed);
                }
            // Player moves and looks down if down arrow or 's' key is hit   
            else if (Input.GetKey(KeyCode.DownArrow)  ||  Input.GetKey(KeyCode.S)){
                    sR.flipX = false;
                    animator.SetInteger("direction", 2);
                    transform.Translate(Vector3.down * Time.deltaTime * speed);
                }
            // Player moves and looks left if left arrow or 'a' key is hit   
            else if (Input.GetKey(KeyCode.LeftArrow)  ||  Input.GetKey(KeyCode.A)) {
                    sR.flipX = false;
                    animator.SetInteger("direction", 3);
                    transform.Translate(Vector3.left * Time.deltaTime * speed); 
                    }
            // Player moves and looks right if right arrow or 'd' key is hit   
            else if (Input.GetKey(KeyCode.RightArrow)  ||  Input.GetKey(KeyCode.D)) {
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
            

        } else {
            animator.speed = 0;
        }
        
    }
}
