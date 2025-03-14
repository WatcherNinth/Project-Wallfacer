﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public struct FileLinedatatype{
	public string FileID;
	public string LineID;
	public string Content;
    public List<TagType> Tag;
}
public struct Filedatatype{
	public string NO;
	public string Name;
	public string Key;
	public string Description;
	public string Stored;
}
public struct Topictype{
	public string NO;
	public string Name;
	public string Speaker;
	//public string[] Conclusion;
	public string[] Related;
    public string[] Depand;
    public bool[] Depandmet;
	public bool Discovered;
    public bool Interactable;
    public bool Debate;
	//Deduction Board
	public List<Evidencetype> Evidence;
	public List<Conclusiontype> Conclusion;
}
public struct Conclusiontype{
	public string NO;
	public string Name;
	public string Topic;
	public bool Discovered;
	public bool Interactable;
    public bool Assumption;
	public bool Activated;
	public bool Contradicted;
	public string[][] Support;
	public bool[] Supportmet;
	public string[][] Objection;
	public bool[] Objectionmet;
    public string[] Lit;
}
public struct Evidencetype{
	public string eviID;
	public string evitext;
	public bool Activated;
    public List<TagType> Tag;
}
public enum TagCategory
{
    Person,
    Object,
    Time,
    Place,
    Action,
    Event
};
public struct TagType
{
    public string ID;
    public string Name;
    public TagCategory Type;
}

public class Cosmos {
    private static Cosmos instance;
    public FileLinedatatype[] worddata;
    public Filedatatype[] document;
    public Topictype[] Topiclist;
    public Conclusiontype[] Conclusionlist;
    public List<Conclusiontype> ActivatedConclusion;
    public List<TagType> Taglist;
	//load data
	private Cosmos (){
        //Conclusion
        ActivatedConclusion = new List<Conclusiontype>();
        //init tag
        string filepath = "assets/resource/content.csv";
        string[] filedata = File.ReadAllLines(filepath);
        string[][] linedata = new string[filedata.Length - 1][];
        string[] temp;
        for (int i = 1; i < filedata.Length; i++)
        {
            linedata[i - 1] = filedata[i].Split(',');
        }
        Taglist = new List<TagType>();
        for (int i = 0; i < linedata.Length; i++)
        {
            TagType Tagtemp;
            Tagtemp.ID = linedata[i][0];
            Tagtemp.Name = linedata[i][1];
            Tagtemp.Type = (TagCategory)System.Enum.Parse(typeof(TagCategory), linedata[i][2]);
            Taglist.Add(Tagtemp);
        }
        //init worddata
        filepath = "assets/resource/content.csv";
		filedata = File.ReadAllLines (filepath);
		linedata=new string[filedata.Length-1][];
		for (int i = 1; i < filedata.Length; i++) {
			linedata [i - 1] = filedata [i].Split (',');
		}
		worddata=new FileLinedatatype[linedata.Length];
		for (int i = 0; i < linedata.Length; i++) {
			temp = linedata [i] [0].Split ('-');
			worddata [i].FileID = temp [0];
			worddata [i].LineID = temp [1];
			worddata [i].Content = linedata [i] [1];
            worddata [i].Tag = new List<TagType>();
            temp = linedata[i][2].Split(' ');
            for(int j = 0; j < linedata[i][2].Length; j++)
            {
                worddata[i].Tag.Add(Taglist.Find(x=> x.ID==temp[j]));
            }
		}
		//init document
		filepath = "assets/resource/documentlist.csv";
		filedata = File.ReadAllLines (filepath);
		linedata=new string[filedata.Length-1][];
		for (int i = 1; i < filedata.Length; i++) {
			linedata [i - 1] = filedata [i].Split (',');
		}
		document = new Filedatatype[linedata.Length];
		for (int i = 0; i < linedata.Length; i++) {
			document [i].NO = linedata [i] [0];
			document [i].Name = linedata [i] [1];
			document [i].Key = linedata [i] [2];
			document [i].Description = linedata [i] [3];
			document [i].Stored = linedata [i] [4];
		}
		//topic
		string topicfilepath = "assets/resource/topic.csv";
		filedata = File.ReadAllLines (topicfilepath);
		linedata=new string[filedata.Length-1][];
		for (int i = 1; i < filedata.Length; i++) {
			linedata [i - 1] = filedata [i].Split (',');
		}
		Topiclist = new Topictype[linedata.Length];
		for (int i = 0; i < linedata.Length; i++) {
			Topiclist [i].NO = linedata [i] [0];
			Topiclist [i].Name = linedata [i] [1];
			Topiclist [i].Speaker = linedata [i] [2];
			//Topiclist [i].Conclusion = linedata [i] [3].Split (' ');
			Topiclist [i].Related = linedata [i] [4].Split (' ');
			if (linedata [i] [5] == "0")
				Topiclist [i].Discovered = false;
			else
				Topiclist [i].Discovered = true;
            Topiclist[i].Depand = linedata[i][6].Split(' ');
            Topiclist[i].Depandmet = new bool[Topiclist[i].Depand.Length];
            if (Topiclist[i].Depand[0] != "0")
            {
                Topiclist[i].Interactable = false;
            }
            else Topiclist[i].Interactable = true;
            //Debate init
            if(linedata[i][6] == "1")
            {
                Topiclist[i].Debate = true;
            }
            else
            {
                Topiclist[i].Debate = false;
            }
            //Deduciton Board init
            Topiclist[i].Evidence=new List<Evidencetype>();
			Topiclist[i].Conclusion=new List<Conclusiontype>();
		}
		//Conclusion
		string conclusionfilepath = "assets/resource/conclusion.csv";
		filedata = File.ReadAllLines (conclusionfilepath);
		linedata=new string[filedata.Length-1][];
		for (int i = 1; i < filedata.Length; i++) {
			linedata [i - 1] = filedata [i].Split (',');
		}
		Conclusionlist = new Conclusiontype[linedata.Length];
		for (int i = 0; i < linedata.Length; i++) {
			Conclusionlist [i].NO = linedata [i] [0];
			Conclusionlist [i].Name = linedata [i] [1];
			Conclusionlist [i].Topic = linedata [i] [2];
			if (linedata [i] [3] == "0")
				Conclusionlist [i].Discovered = false;
			else
				Conclusionlist [i].Discovered = true;
			Conclusionlist [i].Activated = false;
			Conclusionlist [i].Interactable = false;
			Conclusionlist [i].Contradicted = false;
			temp = linedata [i] [4].Split (' ');
			Conclusionlist [i].Support = new string[temp.Length][];
			Conclusionlist [i].Supportmet = new bool[temp.Length];
			for (int j = 0; j < temp.Length; j++) {
				Conclusionlist [i].Support [j] = temp [j].Split ('&');
				Conclusionlist [i].Supportmet [j] = false;
			}
			temp = linedata [i] [5].Split (' ');
			Conclusionlist [i].Objection = new string[temp.Length][];
			Conclusionlist [i].Objectionmet = new bool[temp.Length];
			for (int j = 0; j < temp.Length; j++) {
				Conclusionlist [i].Objection [j] = temp [j].Split ('&');
				Conclusionlist [i].Objectionmet [j] = false;
			}
            Conclusionlist[i].Lit = linedata[i][6].Split(' ');
            if (linedata[i][7] == "1") Conclusionlist[i].Assumption = true;
            else Conclusionlist[i].Assumption = false ;
            for (int j = 0; j < Topiclist.Length; j++) {
				if (Conclusionlist [i].Topic == Topiclist [j].NO) {
					Topiclist [j].Conclusion.Add (Conclusionlist [i]);
				}
			}
		}
	}
	public static Cosmos Instance(){
		if (instance == null)
			instance = new Cosmos ();
		return instance;
	}
	public string searchline(string ID){
		string[] data=new string[2];
		data = ID.Split ('-');
		for (int i = 0; i < worddata.Length; i++) {
			if (worddata [i].FileID == data [0] && worddata [i].LineID == data [1]) {
				return (worddata [i].Content);
			}
		}
		return(ID+" Not Found!!!");
	}
	public string searchtopic(string ID){
		for (int i = 0; i < Topiclist.Length; i++) {
			if (Topiclist [i].NO == ID)
				return(Topiclist [i].Name);
		}
		return(ID + " Not Found!!!");
	}
}
public class LoadingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
