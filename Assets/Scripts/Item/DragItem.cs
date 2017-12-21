using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
   //可被吃掉的物品要和空物件 "itemname"+"_target" 搭配使用 ,target tag要設置
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
        Debug.Log("1"+player.Playerstate);//ok
        if (player.Playerstate == Player.PlayerState.talk) canDrag = false;
        if (player.Playerstate == Player.PlayerState.idle || player.Playerstate == Player.PlayerState.walk || player.Playerstate == Player.PlayerState.interactive)
        {
            player.Playerstate = Player.PlayerState.interactive;
            originalPosition = myTransform.position;
            if (GetComponent<Image>().sprite.name == "UIMask" || GetComponent<Image>().sprite.name == "diamond") { canDrag = false; }//TODO:名字要改成書的圖片名
            else canDrag = true;
            Debug.Log(player.Playerstate);
            //Debug.Log(canDrag);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(canDrag);
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
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].name == GetComponent<Image>().sprite.name + "_target")
                {
                    target = targets[i];
                }
            }

            Vector3 pos = Camera.main.ScreenToWorldPoint(transform.position); pos.z = 0;

            if (Vector3.Distance(pos, target.transform.position) < 1.0f)//TBD
            {
                Debug.Log("delete:" + GetComponent<Image>().sprite.name);
                if (GetComponent<Image>().sprite.name == "cake_03")
                {//
                    Debug.Log("123456");
                    target.GetComponentInParent<InteractiveItem>().SetTalkNum(1);
                    target.GetComponentInParent<InteractiveItem>().SetTalk();
                }
                player.DeleteHoldItem(GetComponent<Image>().sprite.name);
                player.OnItemChanged();
                
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
            if (GetComponent<Image>().sprite.name == "diamond")
            {//TODO:名字要改成書的圖片名
                GameManager.game.Setactive(GameManager.game.BookUI, true);
                player.Playerstate = Player.PlayerState.interactive;
            }
        }
    }
}