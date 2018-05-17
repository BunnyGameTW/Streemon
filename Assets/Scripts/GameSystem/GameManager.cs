using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    [SerializeField]
    private GameObject _FreezerUI;
    [SerializeField]
    private GameObject _LizardUI;
    Talk _talky;
    public Image fadeImg;
    public GameObject clickEffect;
    private void Awake()
    {
        if (game == null) {
            game = this;
        }

#if UNITY_EDITOR

        if (SceneManager.GetActiveScene().name == "Sopen") CSV.GetInstance().loadFile(Application.dataPath + "/Resources", "opentest1221");//loadCSV
        else if (SceneManager.GetActiveScene().name == "SmainFake")
        {
            CSV.GetInstance().loadFile(Application.dataPath + "/Resources", "fakeopentest1223");
        }
        else CSV.GetInstance().loadFile(Application.dataPath + "/Resources", "test1223");//loadCSV

#else
        if (SceneManager.GetActiveScene().name == "Sopen") CSV.GetInstance().loadFile(Application.dataPath + "/StreamingAssets", "opentest1221");//loadCSV
        else if (SceneManager.GetActiveScene().name == "SmainFake")
        {
            CSV.GetInstance().loadFile(Application.dataPath + "/StreamingAssets", "fakeopentest1223");
        }
        else CSV.GetInstance().loadFile(Application.dataPath + "/StreamingAssets", "test1223");//loadCSV

#endif


        if (SceneManager.GetActiveScene().name == "SblueRoom")
        {
            SaveData._data.tutorialEnd = true;
            

        }
        else if (SceneManager.GetActiveScene().name == "Sout") SaveData._data.tutorialEnd = false;
        //load player position
        if (SaveData._data.tutorialEnd)
        {
            SaveData.SceneInfo roomInfo = SaveData._data.getRoomInfo(SceneManager.GetActiveScene().name);

            if ((roomInfo.name != "SblueRoom" || !roomInfo.firstTalk))
            {
                Player.transform.position = GameObject.Find(SaveData._data.nowScene).transform.position;
                SaveData._data.playerPos = Player.transform.position;
            }
            SaveData._data.nowScene = roomInfo.name;//first set scene name
        }

        Items = GameObject.FindGameObjectsWithTag("item");
        
        if(SceneManager.GetActiveScene().name != "Stitle") _talky = _TalkUI.GetComponent<Talk>();
    }
    public void refindItem() {
        Items = GameObject.FindGameObjectsWithTag("item");
    }
    private void Start()
    {
        if (SaveData._data.tutorialEnd)
        {
            // load scene info
             SaveData.SceneInfo roomInfo = SaveData._data.getRoomInfo(SceneManager.GetActiveScene().name);
            if (!roomInfo.firstIn)
            {
                for (int i = 0; i < Items.Length; i++)
                {
                    bool isExist = false;
                    for (int j = 0; j < roomInfo.itemName.Length; j++)
                    {// in scene set active
                        if (Items[i].name == roomInfo.itemName[j]) isExist = true;
                    }
                    Items[i].SetActive(isExist);
                }
                //check flower status
                if (SaveData._data.flowerIsSmall) {
                    GameObject obj = GameObject.Find("Flower");
                    if (obj != null)
                    {
                        obj.transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
                        obj.GetComponentInParent<Animator>().SetTrigger("Small");
                    }
                }
                //check curtain status
                if (SaveData._data.curtainIsOpen)
                {
                    GameObject obj = GameObject.Find("paint");
                    if (obj != null)
                    {
                        GameObject obj2 = GameObject.Find("curtain");
                        if(obj2 != null)obj2.SetActive(false);
                        obj.GetComponent<InteractiveItem>().interactiveDistance = 4;
                    }
                }
            }

            //load player info

            foreach (string _item in SaveData._data.player.itemName)
            {
               if(_item != "") Player.AddHoldItem(_item);
            }
            if (Player.HoldItems.Count > 0)
            {
                Player.OnItemChanged();
            
            }

            //tutorial
            if (roomInfo.name == "SblueRoom" && roomInfo.firstTalk)
            {
                roomInfo.firstTalk = false;
                Camera.main.GetComponentInChildren<SpriteRenderer>().color += new Color(0, 0, 0, 1);
                StartCoroutine(fadeInOut(Camera.main.GetComponentInChildren<SpriteRenderer>(), -0.08f));
                SetTalk("yellow", 1);
                Player.Playerstate = Player.PlayerState.talk;
                Setactive(TalkUI, true);
                Player.setAnimation(Player.PlayerState.bed);
            }

            if (roomInfo.firstIn)  //is first in
            {
                roomInfo.firstIn = false;
            }
            SaveData._data.setRoomInfo(roomInfo.name, roomInfo);//save room
        }
    }
    public void Update()
    {
        ////cheat code
        if (Input.GetKeyDown(KeyCode.S)) _talky.skip();
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Player.AddHoldItem("book");
            Player.OnItemChanged();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Player.AddHoldItem("flashlight");
            Player.OnItemChanged();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SaveData._data.tutorialEnd = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Player.AddHoldItem("flower");
            Player.OnItemChanged();
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            SaveData._data.chars[1].talkStatus = SaveData.CharsInfo.TalkStatus.canDoMission;
            SaveData._data.chars[2].talkStatus = SaveData.CharsInfo.TalkStatus.canDoMission;
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            Player.AddHoldItem("seed");
            Player.OnItemChanged();
        }
        ////  if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9)) resetGame();
        //  if (Input.GetKeyDown(KeyCode.Escape)) endGame();
        //點擊特效
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); pos.z = 0;
            GameObject g = Instantiate(clickEffect, pos, transform.rotation);
            Destroy(g, 1);
        }
    }

    public void resetGame()
    {
        SaveData._data = new SaveData();
        changeSceneWithFade("Stitle");
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
            if (name == CSV.GetInstance().arrayData[i][0] && paragraph.ToString() == CSV.GetInstance().arrayData[i][1]) {
                iLine = i;
                break;
            }
        }
        //set se
        if (CSV.GetInstance().arrayData[iLine][0] == "yellow" || CSV.GetInstance().arrayData[iLine][0] == "pizza") _talky.talkSE = SoundManager.sound.uise.talk[0];
        else _talky.talkSE = SoundManager.sound.uise.talk[1];

        int np; int.TryParse(CSV.GetInstance().arrayData[iLine][3], out np);//4th parameter
        _talky.SetNextTalk(CSV.GetInstance().arrayData[iLine][2], np);
        int storySize = CSV.GetInstance().arrayData[iLine].Length;
        _talky.SetStorySize(storySize - 4);
        string[] talkStory = _talky.story;
        for (int j = 4; j < storySize; j++)
        {//讀入第N段文字
            talkStory[j - 4] = CSV.GetInstance().arrayData[iLine][j];
        }
        _talky.SetCharsBG(_talky.CheckCharsNum(CSV.GetInstance().arrayData[iLine][0]));

    }
    public GameObject TalkUI
    {
        get { return _TalkUI; }
    }
    public Talk Talky {
        get { return _talky; }
    }
    public void endGame()
    {
        Application.Quit();
    }
    public void changeSceneWithFade(string sceneName)
    {
        SoundManager.sound.bgmFadeOut();
        StartCoroutine(fadeInScene(sceneName));
    }
    IEnumerator fadeInScene(string sceneName)
    {
        fadeImg.GetComponent<Animator>().SetBool("isFade", true);
        yield return new WaitUntil(()=>fadeImg.color.a == 1);
        changeScene(sceneName);
    }
    public IEnumerator fadeIn()
    {
        fadeImg.GetComponent<Animator>().SetBool("isFade", true);
        yield return new WaitUntil(() => fadeImg.color.a == 1);
        
    }
    public IEnumerator fadeOut()
    {      
        fadeImg.GetComponent<Animator>().SetBool("isFade", false);
        yield return new WaitUntil(() => fadeImg.color.a == 0);       
    }
    public GameObject FreezerUI
    {
        get { return _FreezerUI; }
    }
    public GameObject LizardUI
    {
        get { return _LizardUI; }
    }
}
