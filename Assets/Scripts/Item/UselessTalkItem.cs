using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessTalkItem : InteractiveItem {
    public int talkNum;
    int tmpNum;
	// Use this for initialization
	void Start () {
        tmpNum = talkNum;
        player = GameManager.game.Player;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseDown()
    {

        if (_canInteractive)
        {
            if (!SaveData._data.playerHasBook && itemName != "book")
            {
                SetTalk();
            }
            else
            {
                if (canTalk)
                {
                    SetTalk();
                }
            }
        }
    }
    public void SetTalk()
    {
        player.Playerstate = Player.PlayerState.talk;
   
        //load talk data   
       
        if (SaveData._data.tutorialEnd)
        {

            if (!SaveData._data.playerHasBook)
            {
                talkNum = 87;
            }
            else talkNum = tmpNum;

        }        
            GameManager.game.SetTalk("yellow", talkNum); //player talk first
            GameManager.game.Setactive(GameManager.game.TalkUI, true);
        

    }
}
