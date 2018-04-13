using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopicPanelScript : MonoBehaviour {

	public Transform evidenceprefab;
	public List<evitype> evilist = new List<evitype> ();
	public string currenttopic = null;
	public string currentselect;
	// Use this for initialization
	void Start () {
		
	}
	public void addevi(string topicID,string eviID){
		evitype temp=new evitype();
		for (int i = 0; i < evilist.Count; i++) {
			if (evilist [i].topicID == topicID) {
				if (evilist [i].evidence.Find (x => x == eviID) == null) {
					evilist [i].evidence.Add (eviID);
				}
				displaytopic (topicID);
				return;
			}
		}
		temp.topicID = topicID;
		temp.topicName = cosmos.Instance().searchtopic(topicID);
		temp.evidence = new List<string> ();
		temp.evidence.Add (eviID);
		evilist.Add (temp);
		displaytopic (topicID);
	}
	public void displaytopic(string topicID){
		Transform data;
		string content;
		clearpool ();
		gameObject.transform.Find ("TitleTopicBar/Text").GetComponent<Text>().text =  cosmos.Instance().searchtopic (topicID);
		int target = evilist.FindIndex (x =>  x.topicID == topicID);
		if (target != -1 && evilist[target].evidence.Count != 0) {
			for (int i = 0; i < evilist [target].evidence.Count; i++) {
				data = Instantiate (evidenceprefab, gameObject.transform.Find ("EvidencePool/LayoutController"));
				content = cosmos.Instance().searchline (evilist [target].evidence [i]);
				data.GetComponent<EvidenceTextScript> ().LineID = evilist [target].evidence [i];
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
		if (currentselect != null) {
			for (int i = 0; i < layout.childCount; i++) {
				if (layout.GetChild (i).GetComponent<Toggle> ().isOn) {
					Destroy (layout.GetChild (i).gameObject);
				}
			}
			evilist.Find (x => x.topicID == currenttopic).evidence.RemoveAll (y => y == currentselect);
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
