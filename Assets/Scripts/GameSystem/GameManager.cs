using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//門的命名要和Scene的名字一樣
public class GameManager : MonoBehaviour {
    public static GameManager game;//singleton pattern
    [SerializeField]
    private Player _player;
    private GameObject [] _items;
    [SerializeField]
    private GameObject _BookUI;
    [SerializeField]
    private GameObject _TalkUI;
    Talk _talky;
    private void Awake()
    {
        if (game == null) {
            game = this;
        }
      
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "Sopen") CSV.GetInstance().loadFile(Application.dataPath + "/Resources", "opentest1221.csv");//loadCSV
        else if (SceneManager.GetActiveScene().name == "SmainFake")
        {
            CSV.GetInstance().loadFile(Application.dataPath + "/Resources", "fakeopentest1223.csv");
        }
        else CSV.GetInstance().loadFile(Application.dataPath + "/Resources", "test1223.csv");//loadCSV

        if (!SaveData._data.blueRoom.firstTalk)
        {
            Player.transform.position = GameObject.Find(SaveData._data.nowScene).transform.position;
            SaveData._data.playerPos = Player.transform.position;
            SaveData._data.nowScene = SceneManager.GetActiveScene().name;//update scene name
        }
        else SaveData._data.nowScene = SceneManager.GetActiveScene().name;
        Items = GameObject.FindGameObjectsWithTag("item");
        _talky = _TalkUI.GetComponent<Talk>();
    }
    public void refindItem() {
        Items = GameObject.FindGameObjectsWithTag("item");
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "SredRoom"  && SaveData._data.blueRoom.firstTalk) {
            SaveData._data.blueRoom.firstTalk = false;
            Camera.main.GetComponentInChildren<SpriteRenderer>().color += new Color(0, 0, 0, 1); 
            StartCoroutine(fadeInOut(Camera.main.GetComponentInChildren<SpriteRenderer>(), -0.08f));
            SetTalk("yellow",1);
            Player.Playerstate = Player.PlayerState.talk;        
            Setactive(TalkUI, true);
        }


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
            //Debug.Log(target.color.a);
            Color color = target.color;
            color.a += speed;
            if (color.a > 1.0f)
            {
                color.a = 1.0f;
                target.color = color;
                break;
            }
            else if (color.a < 0.0f)
            {
                color.a = 0.0f;
                target.color = color;
                break;
            }
            target.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(3.0f);

        if (SceneManager.GetActiveScene().name == "SmainFake")
        {
            changeScene("SredRoom");
        }
    }

    public void Setactive(GameObject obj, bool isAble) {
        obj.SetActive(isAble);
       
    }
    public GameObject BookUI
    {
        get { return _BookUI; }
    }
    public void SetTalk(string name, int paragraph) {
      
        //check name and paragraph
        int iLine = 0;
        for (int i = 0; ; i++) {
            Debug.Log(CSV.GetInstance().arrayData[i][0]);
            if (name == CSV.GetInstance().arrayData[i][0] && paragraph.ToString() == CSV.GetInstance().arrayData[i][1]) {
                iLine = i;
                break;
            }
        }
       
        int np; int.TryParse(CSV.GetInstance().arrayData[iLine][3], out np);//4th parameter
        _talky.SetNextTalk(CSV.GetInstance().arrayData[iLine][2], np);
        int storySize = CSV.GetInstance().arrayData[iLine].Length;
        _talky.SetStorySize(storySize - 4);
        string[] talkStory = _talky.story;
        for (int j = 4; j < storySize; j++)
        {//讀入第N段文字
            talkStory[j - 4] = CSV.GetInstance().arrayData[iLine][j];
            Debug.Log(talkStory[j - 4]);
        }
        Debug.Log(_talky.CheckCharsNum(CSV.GetInstance().arrayData[iLine][0]));
        _talky.SetCharsBG(_talky.CheckCharsNum(CSV.GetInstance().arrayData[iLine][0]));

    }
    public GameObject TalkUI
    {
        get { return _TalkUI; }
    }
    public Talk Talky {
        get { return _talky; }
    }
}
