using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungenessBody : HealthSystem {

    public DungenessHealth DH;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void takeDamage(int damage)
    {
        DH.takeDamage(damage);
        DH.GetComponent<Dungeness>().BodyHit(damage);
    }

}
