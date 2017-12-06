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
    public Vector3 originalPosition;   
    Player player;    
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
         originalPosition = myTransform.position;
        //TODO:判斷target的位置
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