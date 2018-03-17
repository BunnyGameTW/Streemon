﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Book : MonoBehaviour {
    public Sprite [] _imgs;
    int pageNum;
    public GameObject Lbtn, Rbtn;
    public GameObject bookImg;
    bool[] hasDiary = new bool[5];//default false
    public Sprite noDiaryImg;                
    private void Start()
    {
      
    }
    void OnEnable () {
        hasDiary = SaveData._data.getDiaryInfo();
        SoundManager.sound.playOne(SoundManager.sound.uise.click[0]);
        pageNum = 0;
        displayBtn();
        displayImg();
    }

    public void changePage(int i) {
        pageNum += i;
        SoundManager.sound.playOne(SoundManager.sound.playerse.pick);
        displayBtn();
        displayImg();
    }
    public void closeBook() {
        SoundManager.sound.playOne(SoundManager.sound.uise.click[0]);
        GameManager.game.Player.Playerstate = Player.PlayerState.idle;
        GameManager.game.Setactive(this.gameObject, false);
    }
    void displayBtn() {
        GameManager.game.Setactive(Lbtn, true);
        GameManager.game.Setactive(Rbtn, true);
        if (pageNum == 0) GameManager.game.Setactive(Lbtn, false);
        else if (pageNum == _imgs.Length - 1) GameManager.game.Setactive(Rbtn, false);
    }
    void displayImg() {
        //
        if(pageNum < 5) {
            if (hasDiary[pageNum]) bookImg.GetComponent<Image>().sprite = _imgs[pageNum];//顯示正常日記
            else bookImg.GetComponent<Image>().sprite = noDiaryImg;//顯示尚未取得日記的圖片
        }
        else bookImg.GetComponent<Image>().sprite = _imgs[pageNum];
    }
    public void AddDiary(int diaryIndex)//傳入第N張
    {
        hasDiary[diaryIndex] = true;
        SaveData._data.setDiaryInfo(hasDiary);

    }
}
