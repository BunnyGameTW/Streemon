using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class BagUI : MonoBehaviour {
    public Sprite[] itemSprite;
    public Sprite noneSprite;
    // Use this for initialization
    Sprite [] _sprite;
    Player player;
    List<string> itemList;
	void Start () {
        _sprite = new Sprite[6];
        player = GameManager.game.Player;
        player.OnItemChange += this.OnItemChange;//監聽
        for (int i = 0; i < GetComponentsInChildren<Image>().Length; i++)
        {
            //_sprite[i] = GetComponentsInChildren<Image>()[i].sprite;
            //_sprite[i] = noneSprite;
             GetComponentsInChildren<Image>()[i].sprite = noneSprite;
        }
    }

    
    void OnItemChange(object sender, EventArgs args) {
        displayItem();
    
    }
    void displayItem() {

        int itemListNum = 0;
        foreach (string _item in player.HoldItems)
        {
          //TODO:BUG 要每個都檢查 不能只檢查串列有的
            for (int i = 0; i < itemSprite.Length; i++)
            {
                if (_item == itemSprite[i].name)
                {
                    GetComponentsInChildren<Image>()[itemListNum].sprite = itemSprite[i];
                    break;
                }
                else GetComponentsInChildren<Image>()[itemListNum].sprite = noneSprite;
            }
            itemListNum++;
        }
    }
}
