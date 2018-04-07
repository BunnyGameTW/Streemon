using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : InteractiveItem {
    public bool hasPermission;
	// Use this for initialization
	void Start () {
        hasPermission = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseDown()
    {
        if(_canInteractive)
        {       
            GameManager.game.Player.Playerstate = Player.PlayerState.interactive;
            if (hasPermission) {
                GetComponent<SpriteRenderer>().enabled = false;
                GameManager.game.Player.GetComponent<Animator>().SetTrigger("Eat");
                Invoke("SetTalk", 2.0f);
            }
            else
            {
                SetTalk();
            }
        }
    }
    new void SetTalk()
    {
        GameManager.game.Player.Playerstate = Player.PlayerState.talk;

        if (hasPermission)
        {
            StartCoroutine(GameManager.game.fadeInOut(Camera.main.GetComponentInChildren<SpriteRenderer>(), 0.08f));
            GameManager.game.SetTalk(itemName, 2);
        }
        else
        {
            GameManager.game.SetTalk(itemName, 1);
        }
        GameManager.game.Setactive(GameManager.game.TalkUI, true);
    }
}
