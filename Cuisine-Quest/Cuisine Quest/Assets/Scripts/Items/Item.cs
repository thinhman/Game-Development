using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Inventory,
    Weapon,
    Ability
}

[System.Serializable]
public class Item : SpawnObject
{
    public string Name = "Unnamed";
    public ItemType Type;

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        Item item = obj as Item;

        return this.Name == item.Name;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.GetComponent<CiscoTesting>().AddItem(gameObject);
        }
    }
}
