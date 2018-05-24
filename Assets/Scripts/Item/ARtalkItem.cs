using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ARtalkItem : InteractiveItem {
    public int talkNum;
    // Use this for initialization
    void Start () {
        player = GameManager.game.Player;
	}
	
	// Update is called once per frame
	void OnMouseDown () {
        if (_canInteractive)
        {
           if(canTalk) SetTalk();
        }
	}
    public void SetTalk()
    {
        player.Playerstate = Player.PlayerState.talk;
        
        GameManager.game.SetTalk(itemName, talkNum); //player talk first
        GameManager.game.Setactive(GameManager.game.TalkUI, true);
    }
}
