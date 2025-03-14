﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentPanelScript : MonoBehaviour {
	public Transform ContentPrefab;
	public string currentselect;
    public string currentdisplay;
	// Use this for initialization
	void Start () {
		
	}
	public void formcontent(string NO){
		ArrayList list = new ArrayList ();
		Transform data;
        currentdisplay = NO;
		for (int i = 0; i < Cosmos.Instance().worddata.Length; i++) {
			if (Cosmos.Instance().worddata[i].FileID == NO) list.Add (Cosmos.Instance().worddata[i]);
		}

		FileLinedatatype[] display = (FileLinedatatype[])list.ToArray (typeof(FileLinedatatype));
		//display element
		clearpool();
		for (int i = 0; i < display.Length; i++) {
			data=Instantiate (ContentPrefab,gameObject.transform);
			data.GetComponent<Text> ().text = display [i].Content;
			data.Find ("SelectText").GetComponent<Text>().text = display [i].Content;
			data.GetComponent<Toggle> ().group = gameObject.transform.GetComponent<ToggleGroup>();
			data.GetComponent<ContentTextScript>().LineID = display [i].FileID + "-" + display [i].LineID;
		}
	}
    public void returnBtnHandler()
    {
        if (currentdisplay.IndexOf("P") == -1)
        {
            returnsearch();
        }
        else
        {
            returncharacter();
        }
    }
	public void returnsearch(){
		Transform search = GameObject.Find ("SearchPanel").transform;
		search.position = GameObject.Find ("DocumentPanel").transform.position;
		GameObject.Find ("DocumentPanel").transform.position = GameObject.Find ("leftout").transform.position;
        search.gameObject.SetActive(true);
		search.Find ("Searchbar").GetComponent<InputField> ().text = "";
		search.Find ("Searchpool").GetComponent<SearchPool> ().clearpool ();
	}
    public void returncharacter()
    {
        Transform character= GameObject.Find("CharacterPanel").transform;
        character.position = GameObject.Find("DocumentPanel").transform.position;
        GameObject.Find("DocumentPanel").transform.position = GameObject.Find("leftout").transform.position;
        character.gameObject.SetActive(true);
    }
	public void clearpool(){
		if (gameObject.transform.childCount != 0) {
			for (int i = 0; i < gameObject.transform.childCount; i++) {
				Destroy (gameObject.transform.GetChild (i).gameObject);
			}
		}
	}
	// Update is called once per frame
	void Update () {
	}
}
