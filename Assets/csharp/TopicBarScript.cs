using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopicBarScript : MonoBehaviour {
	public string topicID = null;
	// Use this for initialization
	void Start () {
		
	}
	public void clicked(){
		Transform topicpanel = GameObject.Find ("TopicPanel").transform;
		Transform topicpool = GameObject.Find ("TopicPool").transform;
		Vector3 rightout = GameObject.Find ("rightout").transform.position;
		if (topicpool.position == rightout) {
			topicpool.position = topicpanel.position;
			topicpanel.position = rightout;
			topicpanel.GetComponent<TopicPanelScript> ().currenttopic = null;
		} else {
			topicpanel.position = topicpool.position;
			topicpool.position = rightout;
			topicpanel.GetComponent<TopicPanelScript> ().currenttopic = topicID;
			topicpanel.GetComponent<TopicPanelScript> ().displaytopic (topicID);
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
