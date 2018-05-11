using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBar : MonoBehaviour {
	public string NO;
	public string title;
	// Use this for initialization
	void Start () {
		
	}
	public void clicked(){
		Transform DP = GameObject.Find ("DocumentPanel").transform;
		DP.Find("Title").GetComponent<Text> ().text = title;
		print (NO);
		DP.position = gameObject.transform.parent.parent.position;
		gameObject.transform.parent.parent.position = GameObject.Find ("leftout").transform.position;
		DP.Find("ContentPanel/LayoutController").GetComponent<DocumentPanelScript> ().formcontent (NO);
	}
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {

        }
	}
}
