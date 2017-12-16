using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Book : MonoBehaviour {
    public Sprite [] _imgs;
    int pageNum;
    public GameObject Lbtn, Rbtn;
    public GameObject bookImg;
    //現在是一張圖兩頁 到時候可能要改
	// Use this for initialization
	void OnEnable () {
        pageNum = 0;
        displayBtn();
        displayImg();
    }

    public void changePage(int i) {
        pageNum += i;
        displayBtn();
        displayImg();
    }
    public void closeBook() {
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
