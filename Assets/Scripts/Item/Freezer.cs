using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class Freezer : MonoBehaviour {
    public int canUnlock;
    public GameObject sceneFreezer;
    bool isUpClick, isMidClick, isDownClick;
    
    Animator[] anis;
	// Use this for initialization
	void Start () {
        isUpClick = isMidClick = isDownClick =false;
        anis = GetComponentsInChildren<Animator>();
        canUnlock = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if(canUnlock == 6) {
            canUnlock = 0;
          //TODO: play music     
          //animation
            for (int i = 0; i < 3; i++)
            {
                anis[i].SetBool("isUnlock", true);
            }
            //delete flower
            GameManager.game.Player.DeleteHoldItem("flower");
            GameManager.game.Player.OnItemChanged();
            //save data
            Invoke("setUnlock", 1.0f);
        }

    }
    void setUnlock()
    {
        
        //save data
        SaveData._data.treeIsUnlock = true;       
        sceneFreezer.GetComponent<sceneFreezer>().SetUnlock();
        //fade in fade out
        StartCoroutine(fade());
      
    }
    IEnumerator fade()
    {
       
        yield return StartCoroutine(GameManager.game.fadeIn());
        Image[] imgs = transform.parent.gameObject.GetComponentsInChildren<Image>();
        for (int i = 0; i < imgs.Length; i++) imgs[i].enabled = false;
         StartCoroutine(GameManager.game.fadeOut());
        sceneFreezer.GetComponent<Animator>().SetTrigger("Open");

        GameManager.game.Player.Playerstate = Player.PlayerState.idle;
        transform.parent.gameObject.SetActive(false);
    }
    public void SetClick(string name)
    {
        if (name == "up" && isUpClick == false)
        {
            isUpClick = true;
            anis[0].SetTrigger("Touch");
            canUnlock++;
        }
        else if (name == "mid" && isMidClick == false)
        {
            isMidClick = true;
            anis[1].SetTrigger("Touch");
            canUnlock += 2;
        }
        else if (name == "down" && isDownClick == false)
        {
            isDownClick = true;
            anis[2].SetTrigger("Touch");
            canUnlock += 3;
        }
    }
    public void SetUnClick(string name)
    {
        if (name == "up")
        {
            isUpClick = false;           
        }
        else if (name == "mid") isMidClick = false;
        else if (name == "down") isDownClick = false;
    }
}
