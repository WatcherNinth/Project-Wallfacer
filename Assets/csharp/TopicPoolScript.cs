using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TopicPoolScript : MonoBehaviour {
	public Transform topicprefab;
	// Use this for initialization
	void Awake () {
		displaytopic ();

	}
	public void clearpool(){
		if (gameObject.transform.childCount != 0) {
			for (int i = 0; i < gameObject.transform.childCount; i++) {
				Destroy (gameObject.transform.GetChild (i).gameObject);
			}
		}
	}
	public void displaytopic(){
		clearpool ();
		Transform data;
		for (int i = 0; i < Cosmos.Instance().Topiclist.Length; i++) {
			if (Cosmos.Instance().Topiclist [i].Discovered == true) {
				data = Instantiate (topicprefab, gameObject.transform);
				data.Find ("Text").GetComponent<Text> ().text = Cosmos.Instance().Topiclist [i].Name;
				data.GetComponent<TopicBarScript> ().topicID = Cosmos.Instance().Topiclist [i].NO;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
