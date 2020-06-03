using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityChat : MonoBehaviour
{
    public Transform player;
    public GameObject uiCanv;



    void Update()
    {

        float dist = Vector3.Distance(player.position, transform.position);

        if (dist < 2)
        {

            uiCanv.SetActive(true);

        }
        else
        {
            uiCanv.SetActive(false);
        }



    }
}
