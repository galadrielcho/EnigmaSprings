using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityChat : MonoBehaviour
{
    public Transform player;
    public GameObject uiCanv;



    void Update()
    {
        GameObject.FindGameObjectWithTag("Player");

        float dist = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);

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
