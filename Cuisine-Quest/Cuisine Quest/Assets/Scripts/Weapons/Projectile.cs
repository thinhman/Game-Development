using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {
    public int Damage = 1;
    public GameObject Mesh;

    public void SetLayer(int WeaponLayer, int OrderInLayer) {
        gameObject.layer = WeaponLayer;
        Mesh.GetComponent<SpriteRenderer>().sortingOrder = OrderInLayer;
    }

    public void DoDamage(Collider2D collision)
    {
        HealthSystem health = collision.GetComponent<HealthSystem>();
        if (health)
        {
            health.takeDamage(Damage);
        }
        else
        {
            Debug.Log("Missing HealthSystem");
        }

        if (collision.CompareTag("BulletE"))
        {
            Destroy(collision.gameObject);
        }
    }
    
}
