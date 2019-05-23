using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCredits : MonoBehaviour 
{
    public GameObject quitButton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(true);
    }
}
