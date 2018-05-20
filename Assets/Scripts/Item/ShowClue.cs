using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ShowClue : MonoBehaviour {
    public string name;
    public int index;
    public GameObject textUI;
    Text txt;
    public bool canShow;
	// Use this for initialization
	void Start () {
        canShow = false;
        txt = textUI.GetComponentInChildren<Text>();
        GameManager.game.Player.OnItemEndTalk += this.OnItemEndTalked;
    }
    //TODO:家條件
    void OnItemEndTalked(object sender, EventArgs args)
    {
        if (canShow)
        {
            canShow = false;
            showText();
            Invoke("closeTxt", 2.0f);
        }
    }
    public void showText()
    {
        txt.text = "獲得了" + name + "的線索 (第" + (index+1) + "頁)";
        SaveData._data.hasDiary[index] = true;
        textUI.SetActive(true);
    }
    public void closeTxt()
    {
        textUI.SetActive(false);
    }
}
