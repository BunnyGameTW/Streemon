using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Pintoo : MonoBehaviour ,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform[] checkPoint;
    private Transform myTransform;
    private RectTransform myRectTransform;
    Vector3 originalPosition;
    public Transform[] pintoos;
    // Use this for initialization
    void Start () {
        myTransform = this.transform;
        myRectTransform = this.transform as RectTransform;
        originalPosition = myTransform.position;
    }
	
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = myTransform.position;

    }
    public void OnDrag(PointerEventData eventData)
    {
        //set this image to the front
        transform.SetAsLastSibling(); 
         Vector3 globalMousePos;
       
        //判斷有沒有碰到特定點 有的話交換
        for (int i = 0; i < checkPoint.Length; i++)
        {        
            if (Vector3.Distance(myRectTransform.position, pintoos[i].position) <= 100.0f && pintoos[i].position!=transform.position)
            {
                      
                myRectTransform.position = pintoos[i].position;
                pintoos[i].GetComponent<Pintoo>().ResetPos(originalPosition);
                originalPosition = myRectTransform.position;
            }

        }
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(myRectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            myRectTransform.position = globalMousePos;
        }
    }
    public void ResetPos(Vector3 pos)
    {
        transform.position = originalPosition = pos;
      
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        myTransform.position = originalPosition;
        check();

    }
    void check()
    {
        int checkNum = 0;
        for(int i = 0; i < checkPoint.Length; i++)
        {
            if (Vector3.Distance(pintoos[i].position, checkPoint[i].position) < 2.0f)
            {
                checkNum++;
            }
        }
       
        if  (checkNum == 4)
        {
            //TODO:play sound
            //can enter billy room         
            SaveData._data.canEnterPurpleRoom = true;         
            GameObject.Find("SpurpleRoom").GetComponent<InteractiveItem>().SetPurpleDoor();
            //set this unactive
            transform.parent.gameObject.SetActive(false);
        }
    }
   
}
