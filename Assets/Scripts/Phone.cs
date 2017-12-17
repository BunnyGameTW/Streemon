using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public string[] phoneNums;
    internal string nowString;
    public GameObject talkUI,desk;
    internal bool canClick = false;
    internal bool isTalk;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {
        // GameObject.Find("mike").SetActive(true);
        //GameObject.Find("phoneLine").SetActive(false);
        transform.Find("mike").gameObject.SetActive(true);
        transform.Find("phoneLine").gameObject.SetActive(false);
        nowString = "";
        canClick = false;


    }
    public void btn(string str)
    {
        if (canClick)
        {
            nowString += str;
            for (int i = 0; i < phoneNums.Length; i++)
            {
                if (string.Compare(nowString, phoneNums[i]) == 0)
                {//same

                    talk(i);
                }

            }
        }
    }
    void talk(int i)
    {

        Talk talky = talkUI.GetComponentInChildren<Talk>();
        int storySize = CSV.GetInstance().arrayData[i].Length;

        talky.SetStorySize(storySize - 1);
        string[] talkStory = talky.story;
        for (int j = 1; j < storySize; j++)
        {//讀入第N段文字
            talkStory[j - 1] = CSV.GetInstance().arrayData[i][j];
            Debug.Log(talkStory[j - 1]);
        }
        talkUI.SetActive(true);
        isTalk = true;
        nowString = "";

    }
    void OnMouseDown()
    {
        if (!isTalk)
        {
            this.gameObject.SetActive(false);
            desk.SetActive(true);
        }
     }
}
