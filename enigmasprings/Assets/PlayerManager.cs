using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
    public float speed = 3f;
    private SpriteRenderer sR;
    public Sprite[] heroArray;

    void Start()
    {
        sR = GetComponent<SpriteRenderer>();

    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow)  ||  Input.GetKey(KeyCode.W)) {
            sR.flipX = false;
            sR.sprite = heroArray[3];
            transform.Translate(Vector3.up * Time.deltaTime * speed);

        }
        else if (Input.GetKey(KeyCode.DownArrow)  ||  Input.GetKey(KeyCode.A)){
            sR.flipX = false;
            sR.sprite = heroArray[7];
            transform.Translate(Vector3.down * Time.deltaTime * speed);

        }
        else if (Input.GetKey(KeyCode.LeftArrow)  ||  Input.GetKey(KeyCode.S)) {
            sR.sprite = heroArray[0];
            sR.flipX = false;
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.RightArrow)  ||  Input.GetKey(KeyCode.D)) {
            sR.sprite = heroArray[1];
            sR.flipX = true;
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }
}
