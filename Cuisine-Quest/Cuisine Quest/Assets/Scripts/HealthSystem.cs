using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour 
{
    public int currentHealth;
    public int maxHealth = 3; //whatever amount
    // Use this for initialization
    void Start ()
    {
        currentHealth = maxHealth;
	}
	
    public void Die()
    {
        //removes enemy from game
        //can leave here or move to enemy class
    }

    public bool isAlive()
    {
        return (currentHealth > 0);
    }

    public virtual void takeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(hitFeedback());
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void heal(int amount)
    {
        if(currentHealth + amount > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public void setMaxHealth(int Max)
    {
        maxHealth = Max;
    }

    public void knockBack()
    {
        //add code here or move to proper class
    }

    public virtual IEnumerator hitFeedback()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
