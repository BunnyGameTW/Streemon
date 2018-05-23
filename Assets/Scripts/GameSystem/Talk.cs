using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

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
        if (nextName != "0" && nextParagraph.ToString() != "0")//1.special, 2.normal 
        {
            setSpecialTalkBehavior();
        }
        else //1.talk end 2.set talk num
        {
            setTalkBehavior();
            GameManager.game.Player.Playerstate = Player.PlayerState.idle;
            //         
            GameManager.game.Player.OnItemEndTalked();
            GameManager.game.Setactive(GameManager.game.TalkUI, false);
        }
        if (nextName == "yellow" && nextParagraph == 11)//給淡淡 //TODO:掉地上
        {
            GameObject.Find("bird").GetComponent<Animator>().SetTrigger("Egg");
            GameManager.game.Player.AddHoldItem("diamond");
            GameManager.game.Player.OnItemChanged();
        }
        if (nextName == "yellow" && nextParagraph == 43)//給他看蜥蜴
        {
          
            GameObject.Find("blue").GetComponent<Animator>().SetTrigger("Paint");   
            SaveData._data.curtainIsOpen = true;
            GameObject.Find("curtain").SetActive(false);
            GameObject.Find("paint").GetComponent<InteractiveItem>().interactiveDistance = 4;
        }


    }
    void setSpecialTalkBehavior()
    {
        if (nextName == "tutorialStart" && nextParagraph.ToString() == "999") GameManager.game.changeSceneWithFade("SmainFake");
        else if (nextName == "gotoRoom" && nextParagraph.ToString() == "999")
        {
            SaveData._data.tutorialEnd = true;          
            GameManager.game.changeSceneWithFade("SblueRoom");
        }      
        else if (nextName == "birdEndTalkRandom")
        {
            int _talkNum = UnityEngine.Random.Range(0, 2) + 16;//bird16 or bird17            
            GameManager.game.SetTalk("bird", _talkNum);
            reset();
        }
        else if (nextName == "endingEnd" && nextParagraph.ToString() == "999")
        {
            //play end animation
            GetComponent<Animator>().SetBool("isTalkEnd", true);          
        }
        else if(nextName == "purpleEnd" && nextParagraph.ToString() == "999")//set ending appear and restart
        {
            GameObject.Find("BGimg").GetComponent<PurpleEnding>().SetEnding();
        }
        else //normal
        {
            GameManager.game.SetTalk(nextName, nextParagraph);
            reset();
        }
    }
    void setTalkBehavior()
    {
        SaveData.CharsInfo _charInfo = new SaveData.CharsInfo();
        for (int i = 0; i < SaveData._data.chars.Length; i++)
        {
            if (nextName.ToString().Contains(SaveData._data.chars[i].name))
            {
                _charInfo = SaveData._data.getCharInfo(SaveData._data.chars[i].name);
                Debug.Log(_charInfo.name);
            }
           
        }
        if (nextName.ToString().Contains("FirstTalkEnd"))  _charInfo.talkStatus = SaveData.CharsInfo.TalkStatus.canDoMission; 
        else if (nextName.ToString().Contains("End"))  _charInfo.talkStatus = SaveData.CharsInfo.TalkStatus.missionComplete; 
        if (nextName == "birdFirstTalkEnd")
        {            
            _charInfo.talkNum = 6;
            _charInfo.charTalkFirst = true;
            SaveData._data.ending = 1;
        }
        else if (nextName == "birdrandom")
        {      
            _charInfo.talkNum = UnityEngine.Random.Range(0, 2) + 6; //6 or 7        
        }
        else if (nextName == "birdEnd")
        {
            _charInfo.talkNum = 14;
        }
        
        else if (nextName == "girlFirstTalkEnd")
        {      
            _charInfo.talkNum = 30;
            _charInfo.charTalkFirst = false;
            SaveData._data.ending = 3;//暗影的襲擊

        }
        else if (nextName == "girlTalkEnd" || nextName == "girlEndTalkRandom")
        {
            if (UnityEngine.Random.Range(0, 2) == 0) _charInfo.talkNum = 23;
            else _charInfo.talkNum = 26;
        }
        else if (nextName == "blueFirstTalkEnd")
        {         
            _charInfo.talkNum = 19;
            _charInfo.charTalkFirst = true;
            SaveData._data.ending = 4;//小湖的死亡
        }
        else if (nextName == "blueRandom" || nextName == "blueTalkEnd")
        {
            _charInfo.talkNum = UnityEngine.Random.Range(0, 2) + 11;
            _charInfo.charTalkFirst = true;
            if(nextName == "blueTalkEnd") {
                SaveData._data.mainDoorIsLock = true;
                GameManager.game.LockMainDoor();
            }
        }
        else if (nextName == "blueMidTalkEnd")
        {
            _charInfo.talkNum = 65;
            _charInfo.charTalkFirst = false;

        }
        else if(nextName == "pizzaTalkEnd")
        {
            Pizza _p = GameObject.Find("pizza").GetComponent<Pizza>();
            _p.hasPermission = true;            
        }
        //save char info
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

            if (SceneManager.GetActiveScene().name != "Sout") SoundManager.sound.playOne(talkSE);
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
                    if (SceneManager.GetActiveScene().name != "Sout") SoundManager.sound.stopSE();
                    break;
				}
				sentenceEnd = true;

				break;

			}
			yield return new WaitForSeconds (SaveData._data.playSpeed);
		}
	}
    //bad method TODO: improved
    void setEnding()
    {
        FindObjectOfType<Ending>().canEnd = true;
    }
}
