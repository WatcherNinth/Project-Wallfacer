using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopicPanelScript : MonoBehaviour {

	public Transform evidenceprefab;
	public string currenttopic = null;
	public string currentselect;
	// Use this for initialization
	void Start () {
		
	}
	public void addevi(string topicID,string eviID){
		Evidencetype evi=new Evidencetype();
		for (int i = 0; i < Cosmos.Instance ().Topiclist.Length; i++) {
			if (Cosmos.Instance ().Topiclist [i].NO == topicID) {
				if (Cosmos.Instance ().Topiclist [i].Evidence.FindIndex (x => x.eviID == eviID) == -1) {
					evi.eviID = eviID;
					evi.Activated = false;
					Cosmos.Instance ().Topiclist [i].Evidence.Add (evi);
				}
				displaytopic (topicID);
				return;
			}
		}
	}
	public void displaytopic(string topicID){
		Transform data;
		string content;
		clearpool ();
		gameObject.transform.Find ("TitleTopicBar/Text").GetComponent<Text>().text =  Cosmos.Instance().searchtopic (topicID);
		int target=-1;
		for (int i = 0; i < Cosmos.Instance ().Topiclist.Length; i++) {
			if (Cosmos.Instance ().Topiclist [i].NO == topicID)
				target = i;
		}
		if (target != -1 && Cosmos.Instance ().Topiclist [target].Evidence.Count != 0) {
			for (int i = 0; i < Cosmos.Instance ().Topiclist [target].Evidence.Count; i++) {
				data = Instantiate (evidenceprefab, gameObject.transform.Find ("EvidencePool/LayoutController"));
				content = Cosmos.Instance().searchline (Cosmos.Instance ().Topiclist [target].Evidence [i].eviID);
				data.GetComponent<EvidenceTextScript> ().LineID = Cosmos.Instance ().Topiclist [target].Evidence [i].eviID;
				data.GetComponent<Text> ().text = content;
				data.Find ("SelectText").GetComponent<Text> ().text = content;
				data.GetComponent<Toggle> ().group = data.parent.GetComponent<ToggleGroup>();
			}
		}
	}
	public void addbuttonevent(){
		string currentevidence = GameObject.Find ("DocumentPanel/ContentPanel/LayoutController").GetComponent<DocumentPanelScript> ().currentselect;
		if (currentevidence != "" && currenttopic != "") {
			addevi (currenttopic, currentevidence);
		}
	}
	public void deletebuttonevent(){
		Transform layout = gameObject.transform.Find ("EvidencePool/LayoutController");
		int target=-1;
		if (currentselect != null) {
			for (int i = 0; i < Cosmos.Instance ().Topiclist.Length; i++) {
				if (Cosmos.Instance ().Topiclist [i].NO == currenttopic) {
					target = Cosmos.Instance ().Topiclist [i].Evidence.FindIndex (x => x.eviID == currentselect);
					if (Cosmos.Instance ().Topiclist [i].Evidence [target].Activated == true) {
						print ("Error:Current Evidence Acitvated");
						return;
					}
					Cosmos.Instance ().Topiclist [i].Evidence.RemoveAt (target);
				}
				//destory the prefab
				for (int j = 0; j < layout.childCount; j++) {
					if (layout.GetChild (j).GetComponent<Toggle> ().isOn) {
						Destroy (layout.GetChild (j).gameObject);
					}
				}

			}
		}
	}
	public void clearpool(){
		Transform evidencepool=gameObject.transform.Find("EvidencePool/LayoutController");
		if (evidencepool.childCount != 0) {
			for (int i = 0; i < evidencepool.childCount; i++) {
				
				Destroy (evidencepool.GetChild (i).gameObject);
			}
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
