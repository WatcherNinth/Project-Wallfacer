using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeductionBoardScript : MonoBehaviour {
	bool DeductionBoardActivated=false;
	Transform content;
	// Use this for initialization
	void Start () {
		content = gameObject.transform.GetChild (0).GetChild (0);
	}
	public void DeductionBoardBtnEvent(){
		Vector3 activedpos = new Vector3 (0,-50,0);
		Vector3 deactivepos = new Vector3 (0, -1500, 0);
		if (DeductionBoardActivated) {
			gameObject.transform.localPosition = deactivepos;
			DeductionBoardActivated = false;
		} else {
			gameObject.transform.localPosition = activedpos;
			DeductionBoardActivated = true;
			updatealltopic();
		}
	}
	void updatealltopic(){
		print (content.childCount);
		for (int i = 0; i < content.childCount; i++) {
			content.GetChild (i).GetComponent<TopicBrickScript> ().updateevi();
			content.GetChild (i).GetComponent<TopicBrickScript> ().updatecon();
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
