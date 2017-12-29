﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
   //可被吃掉的物品要和空物件 "image name"+"_target" 搭配使用 ,target tag要設置
    GameObject target;
    private Transform myTransform;
    private RectTransform myRectTransform;
    Vector3 originalPosition;   
    Player player;
    bool canDrag, isBig;
    void Start()
    {
        myTransform = this.transform;
        myRectTransform = this.transform as RectTransform;
        originalPosition = myTransform.position;
        player = GameManager.game.Player;
        canDrag = false;
      
    }
   
    public void OnBeginDrag(PointerEventData eventData)
    {
      
        if (player.Playerstate == Player.PlayerState.talk) canDrag = false;
        if (player.Playerstate == Player.PlayerState.idle || player.Playerstate == Player.PlayerState.walk || player.Playerstate == Player.PlayerState.interactive)
        {
            player.Playerstate = Player.PlayerState.interactive;
            originalPosition = myTransform.position;
            if (GetComponent<Image>().sprite.name == "transparent" || GetComponent<Image>().sprite.name == "book") { canDrag = false; }//TODO:名字要改成書的圖片名
            else canDrag = true;
         
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(myRectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
            {
                myRectTransform.position = globalMousePos;
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("target");         
            Vector3 pos = Camera.main.ScreenToWorldPoint(transform.position); pos.z = 0;
            if (targets.Length != 0) {
                for (int i = 0; i < targets.Length; i++)
                {
                    if (Vector3.Distance(pos, targets[i].transform.position) < 1.0f)
                    {
                        Debug.Log(targets[i]);
                        //判斷是否為正確目標
                        if (targets[i].name == GetComponent<Image>().sprite.name + "_target")
                        {
                            Debug.Log("delete:" + GetComponent<Image>().sprite.name);
                            if (GetComponent<Image>().sprite.name == "cheese_02")//cage has cheese, mouse go to cage
                            {
                              
                                targets[i].GetComponentInChildren<SpriteRenderer>().enabled = true;//display cheese
                                FindObjectOfType<mouse>()._mishState = mouse.MishState.caught;//set mouse go to cage
                                FindObjectOfType<mouse>().cagePos.x = targets[i].transform.position.x;//set mouse cage pos               
                                player.DeleteHoldItem(GetComponent<Image>().sprite.name);
                                player.OnItemChanged();
                            }
                            else {
                                SaveData.CharsInfo charInfo = SaveData._data.getCharInfo(targets[i].GetComponentInParent<InteractiveItem>().itemName);
                                               
                                if (charInfo.talkStatus == SaveData.CharsInfo.TalkStatus.canDoMission)
                                {
                                    if (GetComponent<Image>().sprite.name == "mouse")
                                    {
                                        targets[i].GetComponentInParent<Animator>().SetTrigger("Flip");
                                        charInfo.talkNum = 8;
                                        charInfo.charTalkFirst = true;
                                        //set girl can first talk
                                        SaveData._data.chars[2].talkStatus = SaveData.CharsInfo.TalkStatus.firstTalk;
                                    }
                                    else if (GetComponent<Image>().sprite.name == "diamond")
                                    {
                                        charInfo.talkNum = 15;
                                        charInfo.charTalkFirst = true;
                                        //set blue can first talk
                                        SaveData._data.chars[1].talkStatus = SaveData.CharsInfo.TalkStatus.firstTalk;

                                    }
                                    else if (GetComponent<Image>().sprite.name == "seed")
                                    {
                                        charInfo.talkNum = 13;
                                        charInfo.charTalkFirst = true;
                                        targets[i].GetComponentInParent<Animator>().SetTrigger("Eat");
                                    }
                                    //save talk data
                                    SaveData._data.setCharInfo(charInfo.name, charInfo);
                                    targets[i].GetComponentInParent<InteractiveItem>().SetTalk();
                                    //delete item
                                    player.DeleteHoldItem(GetComponent<Image>().sprite.name);
                                    player.OnItemChanged();
                                }
                                else if (charInfo.talkStatus == SaveData.CharsInfo.TalkStatus.firstTalk || charInfo.talkStatus == SaveData.CharsInfo.TalkStatus.premissionNotComplete)
                                {
                                    targets[i].GetComponentInParent<InteractiveItem>().SetSpecialTalk();                                 
                                }
                            }                          
                            break;
                        }
                        else
                        {
                            SaveData.CharsInfo charInfo = SaveData._data.getCharInfo(targets[i].GetComponentInParent<InteractiveItem>().itemName);
                            if (charInfo.talkStatus == SaveData.CharsInfo.TalkStatus.canDoMission)//解任務 給錯
                            {
                                if (charInfo.name == "bird")
                                {
                                    charInfo.talkNum = 54;
                                    charInfo.charTalkFirst = false;
                                }
                                else if (charInfo.name == "girl")
                                {
                                    charInfo.talkNum = 21;
                                    charInfo.charTalkFirst = true;
                                }
                                else if (charInfo.name == "blue")
                                {
                                    charInfo.talkNum = 20;
                                    charInfo.charTalkFirst = true;
                                }
                                SaveData._data.setCharInfo(charInfo.name, charInfo);
                                targets[i].GetComponentInParent<InteractiveItem>().SetTalk();

                            }
                            else
                            {
                                targets[i].GetComponentInParent<InteractiveItem>().SetSpecialTalk();
                            }
                            Debug.Log("uncorrect target");
                        }                    
                    }
                   
                }
            }
            myTransform.position = originalPosition;
           

        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("dragitem point down:"+player.Playerstate);
        if (player.Playerstate == Player.PlayerState.interactive)
        {
            isBig = true;
            transform.localScale = transform.localScale * 1.5f;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("dragitem point up:" + player.Playerstate);

        if (isBig)
        {
            isBig = false;
            transform.localScale = transform.localScale / 1.5f;
            if (GetComponent<Image>().sprite.name == "book")
            {//TODO:名字要改成書的圖片名
                GameManager.game.Setactive(GameManager.game.BookUI, true);
                player.Playerstate = Player.PlayerState.read;
            }
        }
    }
}