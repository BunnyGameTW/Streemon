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
    bool canDrag;
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
        player.Playerstate = Player.PlayerState.interactive;
        originalPosition = myTransform.position;
        if (GetComponent<Image>().sprite.name == "UIMask" || GetComponent<Image>().sprite.name == "diamond") { canDrag = false; }//TODO:名字要改成書的圖片名
        else canDrag = true;
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
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].name == GetComponent<Image>().sprite.name + "_target")
                {
                    target = targets[i];
                }
            }

            Vector3 pos = Camera.main.ScreenToWorldPoint(transform.position); pos.z = 0;

            if (Vector3.Distance(pos, target.transform.position) < 1.0f)
            {
                Debug.Log("delete:" + GetComponent<Image>().sprite.name);

                player.DeleteHoldItem(GetComponent<Image>().sprite.name);
                player.OnItemChanged();
            }
            myTransform.position = originalPosition;

        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        player.Playerstate = Player.PlayerState.interactive;
        transform.localScale = transform.localScale * 1.5f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = transform.localScale / 1.5f;
        player.Playerstate = Player.PlayerState.idle;
        if (GetComponent<Image>().sprite.name == "diamond") {//TODO:名字要改成書的圖片名
            GameManager.game.Setactive(GameManager.game.BookUI, true);
            player.Playerstate = Player.PlayerState.interactive;
        }
    }
}