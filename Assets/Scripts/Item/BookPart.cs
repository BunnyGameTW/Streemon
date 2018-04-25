using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class BookPart : InteractiveItem {
    public int index;
    public Book book;
    public BagUI _bagUI;
    public GameObject diaryUI;
	// Use this for initialization
	void Start () {
        player = GameManager.game.Player;
        _spriteRender = GetComponent<SpriteRenderer>();
    }
	
    //pick up   
    private void OnMouseDown()
    {
        if (canPick && SaveData._data.playerHasBook && _canInteractive)
        {
            GameManager.game.Player.Playerstate = Player.PlayerState.pick;
            SoundManager.sound.playOne(SoundManager.sound.playerse.pick);
            Vector3 pointPos = Camera.main.transform.position; pointPos.z = 0.0f;
            StartCoroutine(itemGotoPoint(pointPos));
        }  
    }

    //item move to point
    IEnumerator itemGotoPoint(Vector3 point)
    {
        _canInteractive = false;
        interactiveDistance = 0;
        _spriteRender.sortingOrder = 9;

        if (canPick)
        {
           book.AddDiary(index);
            StartCoroutine(GameManager.game.fadeInOut(Camera.main.GetComponentInChildren<SpriteRenderer>(), 0.08f));
        }
        else StartCoroutine(GameManager.game.fadeInOut(Camera.main.GetComponentInChildren<SpriteRenderer>(), -0.18f));
        while (transform.position != point)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, 10.0f * Time.deltaTime);
            yield return null;
        }
        if (canPick)
        {         
            diaryUI.GetComponentInChildren<Text>().text = "獲得了日記本第"+(index+1)+"頁";
            diaryUI.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            diaryUI.SetActive(false);
        }
        afterItemGotoPoint();
    }
    void afterItemGotoPoint()
    {
        if (canPick)
        {
            canPick = false;
            //go to diary pos
            Vector3 pointPos = Vector3.zero;            
            for (int i = 0;i < player.HoldItems.Count; i++)
            {
                if (FindObjectOfType<BagUI>().transform.GetChild(i).GetComponent<Image>().sprite.name == "book")
                {
                    pointPos = FindObjectOfType<BagUI>().transform.GetChild(i).position;
                }
            }
            pointPos = Camera.main.ScreenToWorldPoint(pointPos); pointPos.z = 0.0f;
            StartCoroutine(itemGotoPoint(pointPos));
        }
        else
        {
            player.Playerstate = Player.PlayerState.idle;
            player.OnItemChanged();
            Destroy(this.gameObject);

        }

    }
}
