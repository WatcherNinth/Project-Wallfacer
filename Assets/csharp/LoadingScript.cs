﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public struct FileLinedatatype{
	public string FileID;
	public string LineID;
	public string Content;
}
public struct Filedatatype{
	public string NO;
	public string Name;
	public string Key;
	public string Description;
	public string Stored;
}
public struct topictype{
	public string NO;
	public string Name;
	public string Speaker;
	public string[] Conclusion;
	public string[] Related;
	public bool Discovered;
	//Deduction Board
	public List<evidencetype> Evidence;
	public List<conclusiontype> ConclusionList;
}
public struct conclusiontype{
	public string NO;
	public string Name;
	public string Topic;
	public bool Discovered;
	public bool Activated;
	public bool Contradicted;
	public string[][] Support;
	public string[][] Objection;
}
public struct evidencetype{
	public string eviID;
	public string evitext;
	public bool Activated;
}


public class cosmos{
	private static cosmos instance;
	public FileLinedatatype[] worddata;
	public Filedatatype[] document;
	public topictype[] Topiclist;
	public conclusiontype[] Conclusionlist;
	public int test=0;
	//load data
	private cosmos (){
		//ArrayList list = new ArrayList ();
		//init worddata
		string filepath = "assets/resource/content.csv";
		string[] filedata = File.ReadAllLines (filepath);
		string[][] linedata=new string[filedata.Length-1][];
		string[] temp;
		for (int i = 1; i < filedata.Length; i++) {
			linedata [i - 1] = filedata [i].Split (',');
		}
		worddata=new FileLinedatatype[linedata.Length];
		for (int i = 0; i < linedata.Length; i++) {
			temp = linedata [i] [0].Split ('-');
			worddata [i].FileID = temp [0];
			worddata [i].LineID = temp [1];
			worddata [i].Content = linedata [i] [1];
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
		Topiclist = new topictype[linedata.Length];
		for (int i = 0; i < linedata.Length; i++) {
			Topiclist [i].NO = linedata [i] [0];
			Topiclist [i].Name = linedata [i] [1];
			Topiclist [i].Speaker = linedata [i] [2];
			Topiclist [i].Conclusion = linedata [i] [3].Split (' ');
			Topiclist [i].Related = linedata [i] [4].Split (' ');
			if (linedata [i] [5] == "0")
				Topiclist [i].Discovered = false;
			else
				Topiclist [i].Discovered = true;
			//Deduciton Board init
			Topiclist[i].Evidence=new List<evidencetype>();
			Topiclist[i].ConclusionList=new List<conclusiontype>();
		}
		//Conclusion
		string conclusionfilepath = "assets/resource/conclusion.csv";
		filedata = File.ReadAllLines (conclusionfilepath);
		linedata=new string[filedata.Length-1][];
		for (int i = 1; i < filedata.Length; i++) {
			linedata [i - 1] = filedata [i].Split (',');
		}
		Conclusionlist = new conclusiontype[linedata.Length];
		for (int i = 0; i < linedata.Length; i++) {
			Conclusionlist [i].NO = linedata [i] [0];
			Conclusionlist [i].Name = linedata [i] [1];
			Conclusionlist [i].Topic = linedata [i] [2];
			if (linedata [i] [3] == "0")
				Conclusionlist [i].Discovered = false;
			else
				Conclusionlist [i].Discovered = true;
			Conclusionlist [i].Activated = false;
			Conclusionlist [i].Contradicted = false;
			temp = linedata [i] [4].Split (' ');
			Conclusionlist [i].Support = new string[temp.Length][];
			for (int j = 0; j < temp.Length; j++) {
				Conclusionlist [i].Support [j] = temp [j].Split ('&');
			}
			temp = linedata [i] [5].Split (' ');
			Conclusionlist [i].Objection = new string[temp.Length][];
			for (int j = 0; j < temp.Length; j++) {
				Conclusionlist [i].Objection [j] = temp [j].Split ('&');
			}
			for (int j = 0; j < Topiclist.Length; j++) {
				if (Conclusionlist [i].Topic == Topiclist [j].NO) {
					Topiclist [j].ConclusionList.Add (Conclusionlist [i]);
				}
			}
		}
	}
	public static cosmos Instance(){
		if (instance == null)
			instance = new cosmos ();
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