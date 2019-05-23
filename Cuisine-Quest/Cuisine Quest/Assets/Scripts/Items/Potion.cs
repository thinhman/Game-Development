using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item 
{
    public int potionPower = 1;

    public void Consume(CiscoTesting player)
    {
        player.GetComponent<HealthSystem>().heal(potionPower);
    }
}
