using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AniBehavoir : MonoBehaviour {
    Animator ani;
    InteractiveItem _item;
	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
        _item = GetComponent<InteractiveItem>();
        _item.OnItemTalked += this.OnItemTalked;//監聽
        GameManager.game.Player.OnItemEndTalk += this.OnItemEndTalked;
    }

    void OnItemTalked(object sender, EventArgs args)
    {
        ani.SetBool("isTalk", true);
    }
    void OnItemEndTalked(object sender, EventArgs args)
    {
        ani.SetBool("isTalk", false);
    }
}
