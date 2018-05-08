using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContradictionPanelScript : MonoBehaviour {
    public Transform ContradictPrefab;
    public Transform ConditionText;
    public List<Conclusiontype> ContradictionList;
    List<string> Condition;
    List<Transform> ContradictionTransform;
	// Use this for initialization
	void Start () {
        ContradictionList=new List<Conclusiontype>();
        Condition = new List<string>();
        ContradictionTransform = new List<Transform>();
        UpdateContradiction();
    }
    public void UpdateContradiction()
    {
        Transform contradictiontemp;
        Transform conditiontemp;
        //clear old state
        ContradictionList.Clear();
        Condition.Clear();
        for(int i = 0; i < gameObject.transform.Find("Viewport/Content").childCount; i++)
        {
            Destroy(gameObject.transform.Find("Viewport/Content").GetChild(i).gameObject);
        }
        //scan all topic and all conclusion,form contradictionlist
        for(int i = 0; i < Cosmos.Instance().Topiclist.Length; i++)
        {
            for(int j = 0; j < Cosmos.Instance().Topiclist[i].Conclusion.Count; j++)
            {
                //if conclusion is activated and contradicted
                if(Cosmos.Instance().Topiclist[i].Conclusion[j].Activated && Cosmos.Instance().Topiclist[i].Conclusion[j].Contradicted)
                {
                    print(i+" "+j);
                    //if contradiction itself is a condition in existed contradiction 
                    //if(ContradictionList.FindIndex(x=>x.Condition.FindIndex(y=>y== Cosmos.Instance().Topiclist[i].Conclusion[j].NO) == -1) == -1)
                    if(Condition.FindIndex(x=>x== Cosmos.Instance().Topiclist[i].Conclusion[j].NO) == -1)
                    {
                        print("1 pass");
                        //add contradiction
                        ContradictionList.Add(Cosmos.Instance().Topiclist[i].Conclusion[j]);
                        //add activated objection condition into conditionlist
                        for(int k=0;k< Cosmos.Instance().Topiclist[i].Conclusion[j].Objection.Length; k++)
                        {
                            if (Cosmos.Instance().Topiclist[i].Conclusion[j].Objectionmet[k])
                            {
                                for(int l=0; l< Cosmos.Instance().Topiclist[i].Conclusion[j].Objection[k].Length; l++)
                                {
                                    Condition.Add(Cosmos.Instance().Topiclist[i].Conclusion[j].Objection[k][l]);
                                }
                            }
                        }
                    }
                }
            }
        }
        for(int i = 0; i < ContradictionList.Count; i++)
        {
            print("2 pass");
            contradictiontemp = Instantiate(ContradictPrefab, gameObject.transform.Find("Viewport/Content"));
            contradictiontemp.Find("Title").GetComponent<Text>().text = ContradictionList[i].Name;
            for(int j = 0; j < ContradictionList[i].Objection.Length; j++)
            {
                if (ContradictionList[i].Objectionmet[j])
                {
                    string conditiontexttemp = "";
                    for (int k = 0; k < ContradictionList[i].Objection[j].Length; k++)
                    {
                        conditiontexttemp = conditiontexttemp + ContradictionList[i].Objection[j][k] + "\n";
                    }
                    conditiontemp = Instantiate(ConditionText, contradictiontemp.Find("ConditionList"));
                    conditiontemp.GetComponent<Text>().text = conditiontexttemp;
                }
            }
            ContradictionTransform.Add(contradictiontemp);
        }
       print(ContradictionList.Count);
    }
	public void PanelBtnHandler()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
	// Update is called once per frame
	void Update () {

	}
}
