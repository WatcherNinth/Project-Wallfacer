﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentTextScript: MonoBehaviour {
	public string LineID;
	// Use this for initialization
	void Start () {
		
	}
	public void clicked(){
		string current;
		if (gameObject.transform.GetComponent<Toggle> ().isOn) {
			current = LineID;
		}  
		else {
			current = null;
		}
		print(current);
		gameObject.transform.parent.GetComponent<DocumentPanelScript> ().currentselect = current;
	}
	// Update is called once per frame
	void Update () {
		
	}
}