using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SearchPool : MonoBehaviour {

	public Filedatatype[] display;

	public Transform databar;
	// Use this for initialization
	void Start () {
		
	}
	public void SearchWord(string word){
		//GameObject data;
		ArrayList list = new ArrayList ();
		Transform data;
		//illegal input fliter
		if(word.Length==0) return;
		//clear searchpool if needed
		clearpool();
		//setup display array
		for(int i=0;i<Cosmos.Instance().document.Length;i++) {
			if (Cosmos.Instance().document [i].Name.IndexOf (word) != -1 && Cosmos.Instance().document[i].Stored == "0") {
				list.Add (Cosmos.Instance().document [i]);
			}
		}
		Filedatatype[] display = (Filedatatype[])list.ToArray (typeof(Filedatatype));
		//if no result
		if (display.Length == 0) {
			print ("no result!");
			return;
		}
		//display element
		for (int i = 0; i < display.Length; i++) {
			data=Instantiate (databar,gameObject.transform);
			//change the text
			data.Find("Name").GetComponent<Text>().text=display [i].Name;
			data.Find("Description").GetComponent<Text>().text=display [i].Description;
			data.GetComponent<DataBar> ().NO = display [i].NO;
			data.GetComponent<DataBar> ().title = display [i].Name;
		}

	}
	public void clearpool(){
		if (gameObject.transform.childCount != 0) {
			for (int i = 0; i < gameObject.transform.childCount; i++)
				Destroy (gameObject.transform.GetChild (i).gameObject);
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
