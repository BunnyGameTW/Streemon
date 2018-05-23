using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scenePintoo : InteractiveItem
{
    public GameObject pintooUI;
    // Use this for initialization
    private void Start()
    {
        //TODO:sprite to heart
        player = GameManager.game.Player;
    }
    private void OnMouseDown()
    {

        if (_canInteractive)
        {
            if(SaveData._data.chars[1].talkStatus == SaveData.CharsInfo.TalkStatus.missionComplete)
            {
                if (!SaveData._data.canEnterPurpleRoom)
                {
                    pintooUI.SetActive(true);
                    GameManager.game.Player.Playerstate = Player.PlayerState.read;
                }
            }
            else
            {
                //  SetTalk();
                player.Playerstate = Player.PlayerState.talk;
                
                GameManager.game.SetTalk("yellow", 119);//item talk first

                GameManager.game.Setactive(GameManager.game.TalkUI, true);
            }
        }
            
        
      
    }
}
