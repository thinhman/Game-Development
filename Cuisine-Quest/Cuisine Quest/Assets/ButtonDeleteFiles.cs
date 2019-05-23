using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDeleteFiles : MonoBehaviour
{
	private SaveSystem saveSystem;
	public Button button;
	
	// Use this for initialization
	void Start ()
	{
		saveSystem = FindObjectOfType<SaveSystem>();
		button.onClick.AddListener(() => saveSystem.NewGame());
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
