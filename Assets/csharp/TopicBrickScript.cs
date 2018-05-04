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
	private List<Transform> evilist;
	// Use this for initialization
	void Awake () {
		evipanel=Instantiate(EvidencePanel,gameObject.transform.Find("EviPoint"));
		conpanel=Instantiate(ConclusionPanel,gameObject.transform.Find("ConPoint"));
		evilist = new List<Transform> ();
		Deactivate();
		pulldata();
		gameObject.transform.Find ("Title").GetComponent<Text> ().text = topic.Name;
	}
	public void Activate(){
		evipanel.gameObject.SetActive (true);
		conpanel.gameObject.SetActive (true);
		//two panel need to be consistent,set up list to save prefab,and change it dynamicly.
		updateevi();
		updatecon();
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
	public void updateevi(){
		Transform temp;
		evilist.Clear ();
		for (int i = 0; i < evipanel.childCount; i++) {
			Destroy (evipanel.GetChild (i).gameObject);
		}

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
				temp.name=i.ToString();
				temp.SetSiblingIndex (i);
				evilist.Add (temp);
			}
			foreach (Transform item in evilist) {
				item.GetComponent<Toggle> ().onValueChanged.AddListener (ifselect => {eviclickhandler (item, ifselect);});
			}
		}
	}
	void eviclickhandler(Transform evi,bool On){
		evidencetype temp;
		int target=evi.GetSiblingIndex();
		print (target+" "+On);
		temp = topic.Evidence [target];
		if (On) {
			temp.Activated = true;
		} else {
			temp.Activated = false;
		}
		topic.Evidence [target] = temp;
		consettlement ();
		pushdata ();
	}
	public void updatecon(){
		Transform temp;
		for (int i = 0; i < conpanel.childCount; i++) {
			Destroy (conpanel.GetChild (i).gameObject);
		}

		for (int i = 0; i < topic.ConclusionList.Count; i++) {
			if (topic.ConclusionList [i].Discovered) {
				temp = Instantiate (ConclusionText, conpanel);
				temp.GetComponent<Text> ().text = topic.ConclusionList [i].Name;
				//activated
				if (topic.ConclusionList [i].Activated)
					temp.GetComponent<Toggle> ().isOn = true; 
				else
					temp.GetComponent<Toggle> ().isOn = false;
				//interactive
				if (topic.ConclusionList [i].Interactedable)
					temp.GetComponent<Toggle> ().interactable = true;
				else
					temp.GetComponent<Toggle> ().interactable = false;
				//contracted
				if (topic.ConclusionList [i].Contradicted)
					temp.Find ("Contradict").gameObject.SetActive (true);
				else
					temp.Find ("Contradict").gameObject.SetActive (false);
				temp.SetSiblingIndex(i);
			}
		}

	}
	void consettlement(){
		//bool changed=false;
		bool flag=false;
		conclusiontype temp;
		for (int i = 0; i < topic.ConclusionList.Count; i++) {
			temp = topic.ConclusionList [i];
			//update supportmet and interactable
			if (topic.ConclusionList [i].Support.Length != 0) { 
				for (int j = 0; j < temp.Support.Length; j++) {
					for (int k = 0; k < temp.Support [j].Length; k++) {
						temp.Supportmet [j] = true;
						flag = true;
						if (foundevi (temp.Support [j] [k]) == false) {
							temp.Supportmet [j] = false;
							flag = false;
							break;
						}
					}
					print (topicID + " conclusion" + i + " Scondition" + j + flag);

				}
			}
			flag = false;
			for(int j=0;j<topic.ConclusionList[i].Supportmet.Length;j++){
				if (topic.ConclusionList [i].Supportmet [j]) {
					flag = true;
					break;
				}
			}
			//handle discover
			if (flag && topic.ConclusionList [i].Discovered == false)
				temp.Discovered = true;
			temp.Interactedable = flag;
			//activated conclusion failed to met the support
			if (temp.Activated && flag == false) {
				temp.Activated = false;
			}
			//update objectionmet and contradicted
			if (topic.ConclusionList [i].Objection.Length != 0) {
				for (int j = 0; j < temp.Objection.Length; j++) {
					for (int k = 0; k < temp.Objection [j].Length; k++) {
						temp.Objectionmet [j] = true;
						flag = true;
						if (foundevi (temp.Objection [j] [k]) == false) {
							temp.Objectionmet [j] = false;
							flag = false;
							break;
						}
					}
					print (topicID + " conclusion" + i + " Ocondition" + j + flag);
				}
			}
			flag = false;
			for(int j=0;j<topic.ConclusionList[i].Objectionmet.Length;j++){
				if (topic.ConclusionList [i].Objectionmet [j]) {
					flag = true;
					break;
				}
			}
			temp.Contradicted = flag;
			//push back temp
			topic.ConclusionList [i] = temp;
		}
		updatecon ();
	}
	bool foundevi(string id){
		for (int i = 0; i < topic.Evidence.Count; i++) {
			if (topic.Evidence [i].eviID == id && topic.Evidence [i].Activated)
				return true;
		}
		return false;
	}
	void pulldata(){
		for (int i = 0; i < cosmos.Instance ().Topiclist.Length; i++) {
			if (cosmos.Instance ().Topiclist [i].NO == topicID) {
				topic = cosmos.Instance ().Topiclist [i];
				break;
			}
		}
	}
	void pushdata(){
		for (int i = 0; i < cosmos.Instance ().Topiclist.Length; i++) {
			if (cosmos.Instance ().Topiclist [i].NO == topicID) {
				cosmos.Instance ().Topiclist [i] = topic;
				break;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
