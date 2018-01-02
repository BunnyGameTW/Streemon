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
		//StreamReader sr = null;
        TextAsset txt = null;

        try
        {
            txt = (TextAsset)Resources.Load(fileName);
            //sr = File.OpenText(path+"\\"+fileName);
			Debug.Log("file found!");
		}catch{
			Debug.Log ("file lost");
			return;
		}
		string line;
        line = txt.text;
        string []  strLine = line.Split('\n');
        //foreach ( string lines in strLine)
        //{
        //    Debug.Log(lines);
        //}
        for(int i = 0; i < strLine.Length; i++)
        {
            arrayData.Add(strLine[i].Split(','));
        }
       
        //while ((line = sr.ReadLine ()) != null) {
           
        //	arrayData.Add (line.Split(','));
        //////    Debug.Log(line.Split(',')[0]);
        ////}
        //sr.Close();
        //sr.Dispose();
    }

}
