using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BagUI : MonoBehaviour { 
    public Sprite[] itemSprite;
    public Sprite noneSprite;
    // Use this for initialization   
    Player player;
    List<string> itemList;
    public event EventHandler OnBookGot;

    private void Awake()
    {       
        resetDisplayItem();
        player = GameManager.game.Player;
        player.OnItemChange += this.OnItemChange;//監聽      
    }
   
    void OnItemChange(object sender, EventArgs args)
    {
        displayItem();
    }
    void resetDisplayItem()
    {
        for (int i = 0; i < GetComponentsInChildren<Image>().Length; i++)
        {
            GetComponentsInChildren<Image>()[i].sprite = noneSprite;
        }
    }
    void displayItem() {
        resetDisplayItem();
        int itemListNum = 0;
        foreach (string _item in player.HoldItems)
        {
            for (int i = 0; i < itemSprite.Length; i++)
            {             
                if (_item == itemSprite[i].name)
                {
                    GetComponentsInChildren<Image>()[itemListNum].sprite = itemSprite[i];
                    if(_item == "book") {
                    //    OnBookGot(this, EventArgs.Empty);//分發事件
                       SaveData._data.playerHasBook = true;
                    }
                        break;
                }
                else
                {
                    GetComponentsInChildren<Image>()[itemListNum].sprite = noneSprite;
                }
            }
            itemListNum++;
        }
    }
}
