using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float speed = 3f;
    void Start()
    {
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow)  ||  Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        else if (Input.GetKey(KeyCode.DownArrow)  ||  Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        else if (Input.GetKey(KeyCode.LeftArrow)  ||  Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        else if (Input.GetKey(KeyCode.RightArrow)  ||  Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}
