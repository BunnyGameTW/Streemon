using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Pintoo : MonoBehaviour ,IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public int index;
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
                int tmp = index;
                index = pintoos[i].GetComponent<Pintoo>().index;              
                myRectTransform.position = pintoos[i].position;
                pintoos[i].GetComponent<Pintoo>().ResetPos(originalPosition,tmp);
                originalPosition = myRectTransform.position;
            }

        }
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(myRectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            myRectTransform.position = globalMousePos;
        }
    }
    public void ResetPos(Vector3 pos, int _index)
    {
        transform.position = originalPosition = pos;
        index = _index;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        myTransform.position = originalPosition;
        //check
        check();

    }
    void check()
    {
        if(pintoos[0].position == checkPoint[0].position && pintoos[1].position == checkPoint[1].position && pintoos[2].position == checkPoint[2].position && pintoos[3].position == checkPoint[3].position)
        {
            //play sound
            //save data
            //can enter billy room         
            SaveData._data.canEnterPurpleRoom = true;
            GameObject.Find("SpurpleRoom").GetComponent<InteractiveItem>().SetPurpleDoor();
            Debug.Log("lock");
            //set this unactive
            transform.parent.gameObject.SetActive(false);
        }
    }
   
public void OnPointerDown(PointerEventData eventData)
    {


    }
    public void OnPointerUp(PointerEventData eventData)
    {


    }
}
