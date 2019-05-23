using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungenessClaws : HealthSystem {
    public DungenessHealth DH;

    public override void takeDamage(int damage)
    {
        DH.GetComponent<Dungeness>().ClawHit(damage);
    }

    

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && DH.GetComponent<Dungeness>().CurrentBehavior == Dungeness.Behaviors.Attack)
        {
            Debug.Log("Player Hit");
            collision.gameObject.GetComponent<CiscoTesting>().health.takeDamage(DH.gameObject.GetComponent<Dungeness>().ClawAttackPower);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && DH.GetComponent<Dungeness>().CurrentBehavior != Dungeness.Behaviors.Attack)
        {
            collision.transform.position += new Vector3(0, 0.2f, 0);
        }
    }

}
