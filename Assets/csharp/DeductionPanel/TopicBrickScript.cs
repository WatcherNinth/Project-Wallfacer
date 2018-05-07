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
	string topicID;

	private Transform evipanel;
	private Transform conpanel;
	private topictype topic;
	private List<Transform> evilist;
    private List<Transform> conlist;
	// Use this for initialization
	void Awake () {
		evipanel=Instantiate(EvidencePanel,gameObject.transform.Find("EviPoint"));
		conpanel=Instantiate(ConclusionPanel,gameObject.transform.Find("ConPoint"));
		evilist = new List<Transform> ();
        conlist = new List<Transform>();
        topicID = gameObject.name;
		Deactivate();
		pulldata();
        brickset();
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
				temp.GetComponent<Text> ().text = Cosmos.Instance ().searchline (topic.Evidence [i].eviID);
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
        gameObject.transform.Find("EvidenceNum").GetComponent<Text>().text = topic.Evidence.Count.ToString();
    }
	void eviclickhandler(Transform evi,bool On){
		Evidencetype temp;
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
        conlist.Clear();
		for (int i = 0; i < conpanel.childCount; i++) {
			Destroy (conpanel.GetChild (i).gameObject);
		}

		for (int i = 0; i < topic.Conclusion.Count; i++) {
			if (topic.Conclusion [i].Discovered) {
				temp = Instantiate (ConclusionText, conpanel);
				temp.GetComponent<Text> ().text = topic.Conclusion [i].Name;
				//activated
				if (topic.Conclusion [i].Activated)
					temp.GetComponent<Toggle> ().isOn = true; 
				else
					temp.GetComponent<Toggle> ().isOn = false;
				//interactive
				if (topic.Conclusion [i].Interactable)
					temp.GetComponent<Toggle> ().interactable = true;
				else
                    //debug
					temp.GetComponent<Toggle> ().interactable = false;
				//contracted
				if (topic.Conclusion [i].Contradicted)
					temp.Find ("Contradict").gameObject.SetActive (true);
				else
					temp.Find ("Contradict").gameObject.SetActive (false);
				temp.SetSiblingIndex(i);
                conlist.Add(temp);
			}
		}
        foreach(Transform item in conlist)
        {
            item.GetComponent<Toggle>().onValueChanged.AddListener(ifselect => { conclickhandler(item, ifselect); });
        }
	}
    void conclickhandler(Transform con, bool On)
    {
        Conclusiontype temp;
        int target = con.GetSiblingIndex();
        print(target + " " + On);
        temp = topic.Conclusion[target];
        if (On)
        {
            temp.Activated = true;
        }
        else
        {
            temp.Activated = false;
        }
        topic.Conclusion[target] = temp;
        consettlement();
        pushdata();
    }
    void consettlement(){
		//bool changed=false;
		bool flag=false;
		Conclusiontype temp;
		for (int i = 0; i < topic.Conclusion.Count; i++) {
			temp = topic.Conclusion [i];
			//update supportmet and interactable
			if (topic.Conclusion [i].Support.Length != 0) { 
				for (int j = 0; j < temp.Support.Length; j++) {
					for (int k = 0; k < temp.Support [j].Length; k++) {
						temp.Supportmet [j] = true;
						flag = true;
						if (searchcondition (temp.Support [j] [k]) == false) {
							temp.Supportmet [j] = false;
							flag = false;
							break;
						}
					}
                    print(topicID + " conclusion " + i + " Scondition " + j + " " + flag);

				}
			}
			flag = false;
			for(int j=0;j<topic.Conclusion[i].Supportmet.Length;j++){
				if (topic.Conclusion [i].Supportmet [j]) {
					flag = true;
					break;
				}
			}
			//handle discover
			if (flag && topic.Conclusion [i].Discovered == false)
				temp.Discovered = true;
            brickset();
            //handle interactable
			temp.Interactable = flag;
            if (flag) Cosmos.Instance().ActivatedConclusion.Add(temp);
            else Cosmos.Instance().ActivatedConclusion.RemoveAll(x => x.NO == temp.NO);
            //activated conclusion failed to met the support
            if (temp.Activated && flag == false) {
				temp.Activated = false;
                Cosmos.Instance().ActivatedConclusion.RemoveAll(x => x.NO == temp.NO);
			}
			//update objectionmet and contradicted
			if (topic.Conclusion [i].Objection.Length != 0) {
				for (int j = 0; j < temp.Objection.Length; j++) {
					for (int k = 0; k < temp.Objection [j].Length; k++) {
						temp.Objectionmet [j] = true;
						flag = true;
						if (searchcondition (temp.Objection [j] [k]) == false) {
							temp.Objectionmet [j] = false;
							flag = false;
							break;
						}
					}
                    print(topicID + " conclusion " + i + " Ocondition " + j + " " + flag);
				}
			}
			flag = false;
			for(int j=0;j<topic.Conclusion[i].Objectionmet.Length;j++){
				if (topic.Conclusion [i].Objectionmet [j]) {
					flag = true;
					break;
				}
			}
			temp.Contradicted = flag;
			//push back temp
			topic.Conclusion [i] = temp;
            //calculate contradiction
            GameObject.Find("DeductBoardPanel/ContradictPanel").GetComponent<ContradictionPanelScript>().UpdateContradiction();
		}
		updatecon ();
	}
	bool searchcondition(string id){
		for (int i = 0; i < topic.Evidence.Count; i++) {
            if (topic.Evidence[i].eviID == id && topic.Evidence[i].Activated) return true;
		}
        if (Cosmos.Instance().ActivatedConclusion.FindIndex(x => x.NO == id) != -1) return true;
		return false;
	}
	void pulldata(){
		for (int i = 0; i < Cosmos.Instance ().Topiclist.Length; i++) {
			if (Cosmos.Instance ().Topiclist [i].NO == topicID) {
				topic = Cosmos.Instance ().Topiclist [i];
				break;
			}
		}
	}
	void pushdata(){
		for (int i = 0; i < Cosmos.Instance ().Topiclist.Length; i++) {
			if (Cosmos.Instance ().Topiclist [i].NO == topicID) {
				Cosmos.Instance ().Topiclist [i] = topic;
				break;
			}
		}
	}
    void brickset()
    {
        string temp;
        int discovered=0;
        for(int i = 0; i < topic.Conclusion.Count; i++)
        {
            if (topic.Conclusion[i].Discovered) discovered++;
        }
        temp = discovered.ToString() + "/" + topic.Conclusion.Count.ToString();
        gameObject.transform.Find("ConclusionNum").GetComponent<Text>().text = temp;
    }
	// Update is called once per frame
	void Update () {
        
    }
}
