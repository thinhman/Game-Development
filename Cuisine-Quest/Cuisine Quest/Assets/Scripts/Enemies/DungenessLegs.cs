using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungenessLegs : HealthSystem {
    public DungenessHealth DH;

    public override void takeDamage(int damage)
    {
        DH.GetComponent<Dungeness>().LegHit(damage);
    }
}
