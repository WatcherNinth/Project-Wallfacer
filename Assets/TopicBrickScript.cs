using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopicBrickScript : MonoBehaviour {
	public Transform EvidencePanel;
	public Transform EvidenceText;
	public Transform ConclusionPanel;
	public Transform ConclusionText;
	public bool Activated;
	public string topicID;

	private Transform evipanel;
	private Transform conpanel;
	private topictype topic;
	// Use this for initialization
	void Awake () {
		evipanel=Instantiate(EvidencePanel,gameObject.transform.Find("EviPoint"));
		conpanel=Instantiate(ConclusionPanel,gameObject.transform.Find("ConPoint"));
		pulldata();
		gameObject.transform.Find ("Title").GetComponent<Text> ().text = topic.Name;
	}
	public void Activate(){
		Transform temp;
		evipanel.gameObject.SetActive (true);
		conpanel.gameObject.SetActive (true);



		//two panel need to be consistent,set up list to save prefab,and change it dynamicly.



		if (topic.Evidence.Count == 0) {
			//if no evidence
		}
		else{
			for (int i = 0; i < topic.Evidence.Count; i++) {
				temp = Instantiate (EvidenceText, evipanel);
				temp.GetComponent<Text> ().text = cosmos.Instance ().searchline (topic.Evidence [i].eviID);
				temp.Find("Text").GetComponent<Text> ().text = temp.GetComponent<Text> ().text;
				if (topic.Evidence [i].Activated) {
					temp.GetComponent<Toggle> ().isOn = true;
				}
			}
		}
		for (int i = 0; i < topic.ConclusionList.Count; i++) {
			if (topic.ConclusionList [i].Discovered) {
				temp = Instantiate (ConclusionText, conpanel);
				temp.GetComponent<Text> ().text = topic.ConclusionList [i].Name;
				if (topic.ConclusionList [i].Activated)
					temp.GetComponent<Toggle> ().isOn = true;
				else
					temp.GetComponent<Toggle> ().isOn = false;
				if (topic.ConclusionList [i].Contradicted)
					temp.Find ("Contradict").gameObject.SetActive (true);
				else
					temp.Find ("Contradict").gameObject.SetActive (false);
			}
		}
	}
	public void Deactivate(){
		evipanel.gameObject.SetActive (false);
		conpanel.gameObject.SetActive (false);
	}
	public void clickhandler(){
		if (Activated) {
			Activated = false;
			Deactivate ();
		} else {
			Activated = true;
			Activate ();
		}
	}
	void pulldata(){
		for (int i = 0; i < cosmos.Instance ().Topiclist.Length; i++) {
			if (cosmos.Instance ().Topiclist [i].NO == topicID) {
				topic = cosmos.Instance ().Topiclist [i];
			}
		}
	}
	void pushdata(){
		for (int i = 0; i < cosmos.Instance ().Topiclist.Length; i++) {
			if (cosmos.Instance ().Topiclist [i].NO == topicID) {
				cosmos.Instance ().Topiclist [i] = topic;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
