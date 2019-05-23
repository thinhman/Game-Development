using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    public GameObject HealthBarObject;
    public Transform Needle;
    private HealthSystem health;
    private int maxHealth;
    private bool displayBar = false;
	// Use this for initialization
	void Start () {
        health = GetComponent<HealthSystem>();
        maxHealth = health.maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if(!displayBar && health.currentHealth < maxHealth){
            displayBar = true;
            HealthBarObject.SetActive(true);
        }else if(!displayBar){
            HealthBarObject.SetActive(false);
        }

        if(displayBar){
            float percentage = (float)health.currentHealth / (float) health.maxHealth;
            Needle.localScale = new Vector3(percentage, Needle.localScale.y, Needle.localScale.z);
        }
	}
}
