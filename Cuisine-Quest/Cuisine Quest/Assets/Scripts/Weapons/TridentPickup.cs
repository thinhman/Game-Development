using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TridentPickup : MonoBehaviour {

    public Weapon PrefabTrident;
    public GameObject tridentItem;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Weapon newWeapon = Instantiate(PrefabTrident, collision.transform.position, Quaternion.identity);
            newWeapon.name = "Trident";
            collision.GetComponent<CiscoTesting>().Weapons[1].gameObject.SetActive(true);
            collision.GetComponent<CiscoTesting>().AddItem(tridentItem);
            Destroy(gameObject);
        }
    }
}
