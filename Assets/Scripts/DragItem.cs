using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject target;
    private Transform myTransform;
    private RectTransform myRectTransform;
    // 用于event trigger对自身检测的开关
    private CanvasGroup canvasGroup;
    // 拖拽操作前的有效位置，拖拽到有效位置时更新
    public Vector3 originalPosition;
    // 记录上一帧所在物品格子
    Player player;
    // 记录上一帧所在物品格子的正常颜色
    void Start()
    {
        myTransform = this.transform;
        myRectTransform = this.transform as RectTransform;
        originalPosition = myTransform.position;
        player = GameManager.game.Player;
    }
   
    public void OnBeginDrag(PointerEventData eventData)
    {
        player.Playerstate = Player.PlayerState.interactive;
         originalPosition = myTransform.position;//拖拽前记录起始位置
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(myRectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            myRectTransform.position = globalMousePos;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(transform.position); pos.z = 0;
       
        if (Vector3.Distance(pos, target.transform.position) < 1.0f)
        {
            Debug.Log("name:" + GetComponent<Image>().sprite.name);
            
            player.DeleteHoldItem(GetComponent<Image>().sprite.name);
            player.OnItemChanged();
        }
         myTransform.position = originalPosition;
        

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

    }
}