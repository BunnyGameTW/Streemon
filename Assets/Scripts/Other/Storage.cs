using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Storage : MonoBehaviour {
    public GameObject storageRoom;
    public GameObject outDoor,Switch;
    public Image fadeImg;
    public Transform roomPos;
    Vector3 pos;
    public void goIn(bool isIn)
    {
        pos = transform.parent.position;
        StartCoroutine(fadeIn(isIn));       
       
    }
    public void goOut(bool isIn)
    {

        StartCoroutine(fadeIn(isIn));
    }
    void init(bool isIn)
    {
       
        outDoor.SetActive(!isIn);
        Switch.SetActive(!isIn);
        GameManager.game.refindItem();
        GameManager.game.Player.SetPlayerState(0);
        Camera.main.GetComponent<CameraFollow>().enabled = !isIn;
        if (isIn)
        {
            GameManager.game.Player.SetOrderInLayer(5);
            transform.parent.position = roomPos.position;
        }
        else
        {
            GameManager.game.Player.SetOrderInLayer(2);
            transform.parent.position = pos;
        }
    }
    IEnumerator fadeIn(bool isIn)
    {

        fadeImg.GetComponent<Animator>().SetBool("isFade", true);
        yield return new WaitUntil(() => fadeImg.color.a == 1);
        StartCoroutine(fadeOut());
        init(isIn);
    }

    IEnumerator fadeOut()
    {

        fadeImg.GetComponent<Animator>().SetBool("isFade", false);
        yield return new WaitUntil(() => fadeImg.color.a == 0);
        
    }
}
