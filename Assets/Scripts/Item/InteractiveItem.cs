using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InteractiveItem : MonoBehaviour {
    //item tag要設置
    public string itemName;
    Player player;
    public float interactiveDistance;
    bool _canInteractive;
    public event EventHandler OnItemClicked;
    public float walkSpeed = 0.2f;
    [SerializeField]
    bool isHideSprite;
    SpriteRenderer _spriteRender;
    [SerializeField]
    bool canPick;
    [SerializeField]
    bool canTalk;
    int talkNum;
    private void Start()
    {
        player = GameManager.game.Player;
        //TODO: to be improved
        
        _spriteRender = GetComponent<SpriteRenderer>();
        if (isHideSprite)
        {
            _spriteRender.enabled = false;
        }
        talkNum = 1;
    }
    private void Update()
    {
        if(isHideSprite) displaySprite();
    }

    public bool CanInteractive {
        get { return _canInteractive; }
        set { _canInteractive = value; }
    }

    private void OnMouseDown()
    {
       
        if (_canInteractive) {
            if (OnItemClicked != null) OnItemClicked(this, EventArgs.Empty);//分發事件
                     
            if (itemName == "2to3" || itemName =="3to2" || itemName =="2to1" || itemName =="1to2")
            {
                  Debug.Log(itemName + " is clicked");
                //矯正誤差，讓玩家移動到定點再開始波動畫
                Vector3 pointPos = transform.GetChild(0).position; pointPos.y = player.transform.position.y; pointPos.z = 0;
                StartCoroutine(gotoPoint(Player.PlayerState.offset, pointPos, itemName, walkSpeed));         
            }
            if(itemName == "Smain" || itemName =="Sbalcony" || itemName == "SredRoom" || itemName =="SblueRoom")
            {
              //  StartCoroutine(gotoPoint(Player.PlayerState.offset,transform.position, itemName, walkSpeed));
                GameManager.game.changeScene(itemName);
             
            }
            if (canPick)
            {
                Vector3 pointPos = Camera.main.transform.position; pointPos.z = 0.0f;
                StartCoroutine(itemGotoPoint(pointPos)); 
            
            }
            if (canTalk) {
               
                SetTalk();


            }
        }
    }
    public void SetTalkNum(int i)
    {
        //TODO:set talk num
        talkNum = i;
       
    }
    public void SetTalk() {
        player.Playerstate = Player.PlayerState.talk;
        if (itemName == "tutorial") { GameManager.game.SetTalk("yellow", talkNum); }//player talk first
        else GameManager.game.SetTalk(itemName, talkNum);//item talk first
        GameManager.game.Setactive(GameManager.game.TalkUI, true);
    }
    //item move to point
    IEnumerator itemGotoPoint(Vector3 point)
    {
        _canInteractive = false;
        interactiveDistance = 0;
        _spriteRender.sortingOrder = 4;

        if (canPick)
        {
            player.AddHoldItem(itemName);
            StartCoroutine(GameManager.game.fadeInOut(Camera.main.GetComponentInChildren<SpriteRenderer>(), 0.08f));
        }
        else StartCoroutine(GameManager.game.fadeInOut(Camera.main.GetComponentInChildren<SpriteRenderer>(), -0.18f));
        while (transform.position != point) {
            transform.position = Vector3.MoveTowards(transform.position, point, 10.0f * Time.deltaTime);
            yield return null;
        }
       if(canPick) yield return new WaitForSeconds(1.0f);
        afterItemGotoPoint();
    }
    void afterItemGotoPoint()
    {
        if (canPick)
        {
            canPick = false;        
            Vector3 pointPos = FindObjectOfType<BagUI>().transform.GetChild(player.HoldItems.Count-1).position;
            pointPos = Camera.main.ScreenToWorldPoint(pointPos); pointPos.z = 0.0f;        
            StartCoroutine(itemGotoPoint(pointPos));
        }
        else
        {
            player.Playerstate = Player.PlayerState.idle;
            player.OnItemChanged();
            Destroy(this.gameObject);
          
        }
      
    }
    private void OnDestroy()
    {
        GameManager.game.refindItem();//重新統物品數量
    }
    //player go to special point
    IEnumerator gotoPoint(Player.PlayerState state,Vector3 point,string name, float speed) {
        _canInteractive = false;
        player.Playerstate = state;
        if (point.x - player.transform.position.x < 0) player.SetPlayerRotation(180);
        else player.SetPlayerRotation(0);
        while (player.transform.position.x != point.x)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, point, speed * Time.deltaTime);
            yield return null;   
        }
        //
        afterGoToPoint(name);
       
    }
    void afterGoToPoint(string name) {
        if (name == "2to3" || name == "3to2" || name == "1to2" || name == "2to1")
        {
            if (name == "2to3" || name == "1to2") player.Playerstate = Player.PlayerState.up;//變換狀態
            else player.Playerstate = Player.PlayerState.down;
            if (name == "2to3" || name =="2to1") player.SetPlayerRotation(0);//變換腳色上樓移動方向動畫(朝右)
            else player.SetPlayerRotation(180);//朝左
            player.SetOrderInLayer(0);//把腳色Layer變輕
            //走樓梯
            StartCoroutine(gotoPoint(player.Playerstate, transform.GetChild(1).transform.position, transform.GetChild(1).name, walkSpeed));
        }
        else if (name == "2to3pointEnd" || name == "3to2pointEnd" || name =="1to2pointEnd" || name =="2to1pointEnd")
        {
            player.Playerstate = Player.PlayerState.idle;
            if(name == "2to1pointEnd" || name == "3to2pointEnd")player.SetOrderInLayer(2);
        }
       
    }
    //
    void changeSpriteColor() {
        _spriteRender.color = Color.gray;
    }
    //
    void displaySprite() {
        if (_canInteractive) _spriteRender.enabled = true;
        else _spriteRender.enabled = false;
    }
    //debug line
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactiveDistance);
    }

}
