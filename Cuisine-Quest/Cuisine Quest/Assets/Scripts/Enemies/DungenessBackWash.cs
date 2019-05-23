using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungenessBackWash : MonoBehaviour {

    public DungenessHealth DH;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(collision.transform.position.x < transform.position.x)
            {
                collision.transform.position += new Vector3(-0.2f, 0, 0);
            }
            else
            {
                collision.transform.position += new Vector3(0.2f, 0, 0);
            }
        }
    }
}
