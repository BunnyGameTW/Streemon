using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager game;//singleton pattern
    [SerializeField]
    private Player _player;
    private GameObject [] _items;
    [SerializeField]
    private GameObject _BookUI;
    [SerializeField]
    private GameObject _TalkUI;
    private void Awake()
    {
        if (game == null) {
            game = this;
        }
        CSV.GetInstance().loadFile(Application.dataPath + "/Resources", "test1217.csv");//loadCSV
        Items = GameObject.FindGameObjectsWithTag("item");

    }
    public void refindItem() {
        Items = GameObject.FindGameObjectsWithTag("item");
    }
    private void Start()
    {
        Debug.Log(Items.Length);
    }
    public void changeScene(string name) {
        SceneManager.LoadScene(name);
    }
    public Player Player
    {
        get { return _player; }
    }
    public GameObject[] Items
    {
        get { return _items; }
        set { _items = value; }
    }
    public IEnumerator fadeInOut(SpriteRenderer target, float speed) {
        while (target.color.a <= 1.0f)
        {
           // Debug.Log(target.color.a);
            Color color = target.color;
            color.a += speed;
            if (color.a > 1.0f) color.a = 1.0f;
            else if (color.a < 0.0f) color.a = 0.0f;
            target.color = color;
            yield return null;
        }       
    }

    public void AddBagItem() {

    }
    public void Setactive(GameObject obj, bool isAble) {
        obj.SetActive(isAble);
       
    }
    public GameObject BookUI
    {
        get { return _BookUI; }
    }
    public void talk(string name, int paragraph) {
        Debug.Log("123");
        //TODO:not finish
        Talk talky = _TalkUI.GetComponent<Talk>();
        //check name
        int iLine = 0;
        for (int i = 0; ; i++) {
            Debug.Log(CSV.GetInstance().arrayData[i][0]);
            if (name == CSV.GetInstance().arrayData[i][0]) {
                iLine = i;
                Debug.Log("123");
                break;
            }
        }
       
        int storySize = CSV.GetInstance().arrayData[iLine + paragraph].Length;
        talky.SetStorySize(storySize - 2);
        string[] talkStory = talky.story;
        for (int j = 2; j < storySize; j++)
        {//讀入第N段文字
            talkStory[j - 2] = CSV.GetInstance().arrayData[iLine + paragraph][j];
            Debug.Log(talkStory[j - 2]);
        }
        //
        Setactive(_TalkUI, true);
    }
    public GameObject TalkUI
    {
        get { return _TalkUI; }
    }
}
