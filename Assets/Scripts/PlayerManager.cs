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
    private Vector3 touchPosWorld;
    private GameObject touchedObject;

    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
   

    }


    void FixedUpdate()
    {


        if (!GameManagerScript.speaking && Input.touchCount > 0 ) {
            animator.speed = 1;

            // Fire raycast at touch position
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosWorld2D= new Vector2(touchPosWorld.x, touchPosWorld.y);
            RaycastHit2D hitInfo = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);


            // detect if raycast hit a movement control button collider
            if (hitInfo.collider != null){    
                touchedObject = hitInfo.transform.gameObject;

                // Player moves and looks up if up arrow or 'w' key is hit
                if (touchedObject.name == "up"  ||  Input.GetKey(KeyCode.W)) {
                        sR.flipX = false;
                        animator.SetInteger("direction", 1);
                        transform.Translate(Vector3.up * Time.deltaTime * speed);
                    }
                // Player moves and looks down if down arrow or 's' key is hit   
                else if (touchedObject.name == "down"  ||  Input.GetKey(KeyCode.S)){
                        sR.flipX = false;
                        animator.SetInteger("direction", 2);
                        transform.Translate(Vector3.down * Time.deltaTime * speed);
                    }
                // Player moves and looks left if left arrow or 'a' key is hit   
                else if (touchedObject.name == "left"  ||  Input.GetKey(KeyCode.A)) {
                        sR.flipX = false;
                        animator.SetInteger("direction", 3);
                        transform.Translate(Vector3.left * Time.deltaTime * speed); 
                        }
                // Player moves and looks right if right arrow or 'd' key is hit   
                else if (touchedObject.name == "right"  ||  Input.GetKey(KeyCode.D)) {
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
