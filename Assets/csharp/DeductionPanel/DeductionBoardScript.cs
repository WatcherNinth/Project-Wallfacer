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
    public void globalconsettlement()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            content.GetChild(i).GetComponent<TopicBrickScript>().consettlement();
        }
    }
    float scale = 1;
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0 && mousein)
        {
            scale += Input.GetAxis("Mouse ScrollWheel");
            if (scale < 0.4) scale = 0.4f;
            if (scale > 2) scale = 2;
            gameObject.transform.Find("Viewport/Content").localScale = new Vector3(1 * scale, 1 * scale, 1 * scale);
        }
    }
    bool mousein = false;
    private void OnMouseOver()
    {
        mousein = true;
       // print(mousein);
    }
    private void OnMouseExit()
    {
        mousein = false;
       // print(mousein);
    }
}
