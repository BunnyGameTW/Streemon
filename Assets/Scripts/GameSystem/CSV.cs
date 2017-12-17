using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class CSV {
	static CSV csv;
	public List<string[]> arrayData;

	public static CSV GetInstance()
    {
		if (csv == null) {
			csv = new CSV ();
		}
		return csv;
	}
	private CSV(){
		arrayData = new List<string[]> ();
	}
	public string getString(int row,int col){
		return arrayData [row] [col];
	}
	public void loadFile(string path, string fileName){
		arrayData.Clear ();
		StreamReader sr = null;
		try{
			sr = File.OpenText(path+"//"+fileName);
			Debug.Log("file found!");
		}catch{
			Debug.Log ("file lost");
			return;
		}
		string line;
		while ((line = sr.ReadLine ()) != null) {
			arrayData.Add (line.Split(','));
		}
		sr.Close ();
		sr.Dispose ();
	}

}
