using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour {
    public Sprite[] endingSprite;
    public bool canEnd;
	// Use this for initialization
	void Start () {
        canEnd = false;
        changeSprite();
        Debug.Log(SaveData._data.ending);
        GameManager.game.SetTalk("ending", SaveData._data.ending);
        GameManager.game.Setactive(GameManager.game.TalkUI, true);
    }
    void changeSprite() {
        GetComponent<SpriteRenderer>().sprite = endingSprite[SaveData._data.ending];
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!canEnd) GameManager.game.Talky.next();
            else GameManager.game.resetGame();
        }
   }

}
