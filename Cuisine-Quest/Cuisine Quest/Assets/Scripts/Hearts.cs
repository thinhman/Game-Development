using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hearts : MonoBehaviour {

    public Sprite[] HeartSprites;
    public Image HeartUI;
    private CiscoTesting Player;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<CiscoTesting>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        HeartUI.sprite = HeartSprites[Player.health.getCurrentHealth()];
	}
}
