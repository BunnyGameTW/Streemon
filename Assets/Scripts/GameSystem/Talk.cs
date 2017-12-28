using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Talk : MonoBehaviour {
	public string [] story;
	int id,subId;//sentence, word
	public AudioClip talkSE;
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
        //if (nextName == "0" && nextParagraph.ToString() == "0")
        //{
        //    GameManager.game.Player.Playerstate = Player.PlayerState.idle;
        //    GameManager.game.Setactive(GameManager.game.TalkUI, false);
        //}
        //else
        //{
        //    setTalkBehavior();
        //}
        if (nextName != "0")
        {
            setTalkBehavior();
            
        }
        if (nextName != "0" && nextParagraph.ToString() != "0")//1.special, 2.normal 
        {
            if (nextName == "tutorialStart" && nextParagraph.ToString() == "999") GameManager.game.changeScene("SmainFake");//add paragraph number
            else if (nextName == "yellow" && nextParagraph.ToString() == "999")//特殊
            {
                GameManager.game.SetTalk(nextName, nextParagraph);
                reset();
                StartCoroutine(GameManager.game.fadeInOut(Camera.main.GetComponentInChildren<SpriteRenderer>(), 0.08f));
            }
            else
            {
                GameManager.game.SetTalk(nextName, nextParagraph);
                reset();
            }
        }
        else //1.talk end 2.set talk num
        {
            setTalkBehavior();
            GameManager.game.Player.Playerstate = Player.PlayerState.idle;
            GameManager.game.Setactive(GameManager.game.TalkUI, false);
        }
       

        if (nextName == "yellow" && nextParagraph == 11)//給淡淡
        {

            GameObject.Find("bird").GetComponent<Animator>().SetTrigger("Eat");
            GameManager.game.Player.AddHoldItem("diamond");
            GameManager.game.Player.OnItemChanged();
            
        }
    }
    void setTalkBehavior()
    {
        SaveData.CharsInfo _charInfo = new SaveData.CharsInfo();
        for (int i = 0; i < SaveData._data.chars.Length; i++)
        {
            if (nextName.ToString().Contains(SaveData._data.chars[i].name))
            {
                _charInfo.name = SaveData._data.chars[i].name;
                Debug.Log(_charInfo.name);
            }
           
        }
        if (nextName.ToString().Contains("FirstTalkEnd")) { _charInfo.talkStatus = SaveData.CharsInfo.TalkStatus.canDoMission; }
        else if (nextName.ToString().Contains("End")) { _charInfo.talkStatus = SaveData.CharsInfo.TalkStatus.missionComplete; }
        if (nextName == "birdFirstTalkEnd")
        {
            _charInfo.talkNum = 6;
        //    _charInfo.talkStatus = SaveData.CharsInfo.TalkStatus.canDoMission;
        //TODO:箝制任務限制，拿錯對話
        }
        else if (nextName == "birdrandom")
        {      
            _charInfo.talkNum = UnityEngine.Random.Range(0, 2) + 6;         
        }
        else if (nextName == "birdEnd")
        {

            _charInfo.talkNum = 14;
        //    _charInfo.talkStatus = SaveData.CharsInfo.TalkStatus.missionComplete;
        }
        else if (nextName == "girlFirstTalkEnd")
        {
          
            _charInfo.talkNum = 30;
         //   _charInfo.talkStatus = SaveData.CharsInfo.TalkStatus.canDoMission;


        }
        else if (nextName == "girlTalkEnd")
        {         
            _charInfo.talkNum = 31;      
        }
        else if (nextName == "blueFirstTalkEnd")
        {         
            _charInfo.talkNum = 11;   
           }
        else if (nextName == "blueRandom")
        {
            _charInfo.talkNum = UnityEngine.Random.Range(0, 2) + 11;
         }
        else if (nextName == "blueTalkEnd")
        {          
            _charInfo.talkNum = 18;          
        }
        SaveData._data.setCharInfo(_charInfo.name, _charInfo);
    }
    private void Awake()
    {
        txt = GetComponentInChildren<Text>();
    }
    void OnEnable(){
        
        reset();

    }
    public void skip() {


        nextTalk();
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
        {

            SoundManager.sound.playOne(talkSE);
            StartCoroutine(print());
        }
        else if (talkEnd)
        {
           
            nextTalk();
        }
	}
	IEnumerator print(){
		
		subId = 0;  
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
                    SoundManager.sound.stopSE();
                    break;
				}
				sentenceEnd = true;

				break;

			}
			yield return new WaitForSeconds (0.07f);
		}
	}
}
