using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedKnife : Item
{
    int knifeCount = 0;

    int[] dropRates = {100, 70, 50, 25, 10};


    private void Start()
    {
        foreach(int rate in dropRates)
        {
            float rand = Random.Range(0.0f, (100f / rate));
            if (rand <= 1.0f)
            {
                knifeCount++;
            }
        }
        
    }

    

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.GetComponent<CiscoTesting>().GetComponentInChildren<Knife>(true).KnifeCount += knifeCount;
            collision.GetComponent<CiscoTesting>().AddItem(gameObject);
            GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().CurrentArea.RemoveObj(gameObject);
            //Destroy(gameObject);
        }
        
    }


}
