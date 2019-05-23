using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbstract : SpawnObject
{
    [SerializeField]
    protected HealthSystem health;
    public int speed;
    public droppedItem[] drops;
    float percent = 100.0f;

    public abstract void Move();

    public abstract void Attack();

    public void Drop()
    {
        foreach (droppedItem drop in drops)
        {
            float rand = Random.Range(0.0f, (percent / drop.dropRate));
            if (rand <= 1.0f)
            {
                Instantiate(drop.Item, transform.position, drop.Item.transform.rotation);
            }
        }
    }
}

[System.Serializable]
public class droppedItem
{
    public GameObject Item;
    [Range(0,100)]
    public int dropRate;
}

