using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talk : MonoBehaviour {
	public string [] story;
	int id,subId;//sentence, word
	
	public int storySize;
	Text txt;
	bool sentenceEnd,talkEnd;
	// Use this for initialization
	void Start () {
		
		//sentenceEnd = true;

	}
	public void SetStorySize(int size){
		story=new string [size];

	}
	void OnEnable(){
		id = 0;
		txt = GetComponentInChildren<Text> ();
		txt.text = "";
		sentenceEnd = true;
		talkEnd = false;
		next ();
	}
	// Update is called once per frame
	void Update () {
		
	}
	public void next(){
        if (sentenceEnd)
            StartCoroutine(print());
        else if (talkEnd)
        {
            GameManager.game.Setactive(GameManager.game.TalkUI, false);
        }
	}
	IEnumerator print(){
		//
		subId = 0;
		if(id==0)txt.text = "";
		else txt.text+="\n";
		while(true){
			
			sentenceEnd = false;
			txt.text += story [id] [subId];//
			subId++;
			subId = Mathf.Clamp (subId, 0, story [id].Length);
			if (subId == story [id].Length) {	
				sentenceEnd = true;

			}
			if (subId > story [id].Length - 1) {
				id++;
				if (id == story.Length) {
					sentenceEnd = false;
					talkEnd = true;
					id = 0;
                  
                    break;
				}
				sentenceEnd = true;

				break;

			}
			yield return new WaitForSeconds (0.1f);
		}
	}
}
