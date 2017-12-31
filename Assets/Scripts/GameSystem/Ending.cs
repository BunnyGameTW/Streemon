using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour {
    public Sprite[] endingSprite;
	// Use this for initialization
	void Start () {
        changeSprite();
        Debug.Log(SaveData._data.ending);
        GameManager.game.SetTalk("ending", 1);
        GameManager.game.Setactive(GameManager.game.TalkUI, true);
    }
    void changeSprite() {
        GetComponent<SpriteRenderer>().sprite = endingSprite[SaveData._data.ending];
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.game.Talky.next();
        }
   }
    //TODO:改結局圖片, to title,

}
