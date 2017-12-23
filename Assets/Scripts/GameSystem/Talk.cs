using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Talk : MonoBehaviour {
	public string [] story;
	int id,subId;//sentence, word
	
	public int storySize;
	Text txt;
	bool sentenceEnd,talkEnd;
    [Serializable]
    public  struct  CharsImg
    {
        public string name;//要和CSV檔裡面第一個參數名字一樣
        public  Sprite bg;
        public Sprite icon;
    }
    public CharsImg[] chars;
    string nextName;
    int nextParagraph;
    // Use this for initialization
    public int CheckCharsNum(string _name) {

        for (int i = 0; i < chars.Length; i++)
        {
            if (_name == chars[i].name)
            {
                return i;
               
            }
        }
        return 0;

    }
    public void SetStorySize(int size){
		story = new string [size];

	}
    public void SetNextTalk(string name, int paragraph) {
        nextName = name;
        nextParagraph = paragraph;
    }
    public void nextTalk() {
        if (nextName == "0" && nextParagraph.ToString() == "0")
        {
            GameManager.game.Player.Playerstate = Player.PlayerState.idle;
            GameManager.game.Setactive(GameManager.game.TalkUI, false);
        }
        else if (nextName == "tutorialEnd")
        {
            GameManager.game.changeScene("SmainFake");
        }
        else if (nextName == "yellow" && nextParagraph.ToString() == "999") {
            GameManager.game.SetTalk(nextName, nextParagraph);
            reset();
            StartCoroutine(GameManager.game.fadeInOut(Camera.main.GetComponentInChildren<SpriteRenderer>(), 0.08f));           
        }
        else if(nextName == "greenFirstTalkEnd")
        {
           //TODO:
        }
        else
        {
            GameManager.game.SetTalk(nextName, nextParagraph);
            reset();
        }
    }
    private void Awake()
    {
        txt = GetComponentInChildren<Text>();
    }
    void OnEnable(){
        
        reset();

    }
    public void reset()
    {
        id = 0;
        txt.text = "";
        sentenceEnd = true;
        talkEnd = false;
        next();
    }
    public void SetCharsBG(int i)
    {
        GetComponentsInChildren<Image>()[0].sprite = chars[i].bg;
        GetComponentsInChildren<Image>()[1].sprite = chars[i].icon;
    }
	public void next(){
        if (sentenceEnd)
            StartCoroutine(print());
        else if (talkEnd)
        {
            nextTalk();
        }
	}
	IEnumerator print(){
		//
		subId = 0;
        //if(id==0)txt.text = "";
        //else txt.text+="\n";
        txt.text = "";
        while (true){
			
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
