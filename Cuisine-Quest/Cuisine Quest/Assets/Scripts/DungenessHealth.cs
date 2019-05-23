using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungenessHealth : HealthSystem {

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public override void takeDamage(int damage)
    {
        Debug.Log("Dungeness Health: " + base.getCurrentHealth());
        base.takeDamage(damage);
    }

    
}
