using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewDay : MonoBehaviour
{

    public Transform player;
    public SpriteRenderer night;
    public TextMeshProUGUI DayText; 

    // Update is called once per frame

    void Update()
    {
        if(GameManagerScript.speaking && Input.GetKeyDown("y") && Vector3.Distance(player.position, transform.position) < 2) {
            StartCoroutine("FadeInOut");
        }
    }


    IEnumerator FadeInOut() {

        float t = 0;
        Color spriteColor =  night.material.color;
        while(t< 2f) {
            t += Time.deltaTime;

            float alpha = Mathf.Lerp(0, 1,t/2f);
            night.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        GameManagerScript.day += 1;
        DayText.text = "Day " + GameManagerScript.day;
        
        t = 0;
        while(t< 2f) {
            t += Time.deltaTime;

            float alpha = Mathf.Lerp(1,0,t/2f);
            night.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null;
        }

    }
} 

