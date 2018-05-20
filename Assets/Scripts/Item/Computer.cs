using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Computer : MonoBehaviour {
    public int passNum;
    int num;
    bool isCorrect;
    public Sprite[] imgs;
    Sprite passwordImg;
    public event EventHandler OnButtonClick;
    public bool canChange;
    // Use this for initialization
    void Start () {
        num = 0;
        changeImg();
        canChange = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void updateNum(int i)
    {
        num += i;
        if (num > imgs.Length - 1) num = 0;
        else if (num < 0) num = imgs.Length - 1;
        if(canChange)changeImg();
        if (num == passNum) isCorrect = true;
        else isCorrect = false;
        OnButtonClick(this, EventArgs.Empty);//分發事件
    }
    void changeImg()
    {
        GetComponentInChildren<Image>().sprite = imgs[num];
    }
    public bool IsCorrect
    {
        get { return isCorrect; } 
    }
}
