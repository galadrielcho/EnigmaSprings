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

    private bool isTouching = false;

    void Start()
    {
        sR = GetComponent<SpriteRenderer>();

    }

    void FixedUpdate()
    {

        
        if (Input.GetKey(KeyCode.UpArrow)  ||  Input.GetKey(KeyCode.W)) {
        sR.flipX = false;
        animator.SetInteger("direction", 1);
        transform.Translate(Vector3.up * Time.deltaTime * speed);

    }       
        else if (Input.GetKey(KeyCode.DownArrow)  ||  Input.GetKey(KeyCode.S)){
            sR.flipX = false;
            animator.SetInteger("direction", 2);
            transform.Translate(Vector3.down * Time.deltaTime * speed);

        }
        else if (Input.GetKey(KeyCode.LeftArrow)  ||  Input.GetKey(KeyCode.A)) {
            animator.SetInteger("direction", 3);
            sR.flipX = false;
            transform.Translate(Vector3.left * Time.deltaTime * speed); 
            }

        else if (Input.GetKey(KeyCode.RightArrow)  ||  Input.GetKey(KeyCode.D)) {
            animator.SetInteger("direction", 3);
            sR.flipX = true;
            transform.Translate(Vector3.right * Time.deltaTime * speed); 

        }
        else {
            animator.SetInteger("direction", 0);
            sR.sprite = heroArray[animator.GetInteger("direction")];
        }
        
    }
}
