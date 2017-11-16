using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InteractiveItem : MonoBehaviour {

    public string itemName;
    Player player;
    public float interactiveDistance;
    bool _canInteractive;
    public event EventHandler OnItemClicked;
    public float walkSpeed = 0.2f;
    [SerializeField]
    bool isHideSprite;
    SpriteRenderer _spriteRender;
    private void Start()
    {
        player = GameManager.game.Player;
        //TODO: to be improved
        if (isHideSprite)
        {
            _spriteRender = GetComponent<SpriteRenderer>();
            _spriteRender.enabled = false;
        }
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
           // changeSpriteColor();//TODO:BUG
           // transform.localScale = transform.localScale * 0.8f;
            if (OnItemClicked != null) OnItemClicked(this, EventArgs.Empty);//分發事件
            if (itemName == "2to3" || itemName =="3to2" || itemName =="2to1" || itemName =="1to2")
            {
                  Debug.Log(itemName + " is clicked");
                //矯正誤差，讓玩家移動到定點再開始波動畫
                Vector3 pointPos = transform.GetChild(0).position; pointPos.y = player.transform.position.y; pointPos.z = 0;
                StartCoroutine(gotoPoint(Player.PlayerState.offset, pointPos, itemName, walkSpeed));         
            }

        }
    }

    //go to special point
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
