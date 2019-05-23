using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnife : Projectile {
    private bool hitObject = false;
	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().CurrentArea.AddObj(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Area" && collision.tag != "Water" && collision.tag != "Weapon" && collision.tag != "Item"  && collision.tag != "Player" && !collision.CompareTag("BulletE"))
        {
            Debug.Log(collision.name);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.parent = collision.transform;
            hitObject = true;
            DoDamage(collision);

            GetComponent<Animator>().Play("KnifeHit");
        }

        if(collision.tag == "Player" && hitObject)
        {
            collision.GetComponent<CiscoTesting>().Weapons[2].GetComponent<Knife>().KnifeCount += 1;
            Destroy(gameObject);
        }
    }
}
