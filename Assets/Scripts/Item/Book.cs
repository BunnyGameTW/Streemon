using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Book : MonoBehaviour {
    public Sprite [] _imgs;
    int pageNum;
    public GameObject Lbtn, Rbtn;
    public GameObject bookImg;
	// Use this for initialization
	void OnEnable () {
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
        bookImg.GetComponent<Image>().sprite = _imgs[pageNum];
    }
}
