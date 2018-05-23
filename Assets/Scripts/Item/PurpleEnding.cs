using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PurpleEnding : MonoBehaviour {
    public Sprite ending7;
    bool canClick;
    Image _img;
    // Use this for initialization
    void Start () {
        canClick = false;
        _img = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (canClick) GameManager.game.resetGame();
        }
    }
    public void SetEnding()
    {
        StartCoroutine(fading());
       
    }
    IEnumerator fading()
    {
        yield return StartCoroutine(GameManager.game.fadeIn());
        if (SaveData._data.ending == 6)
        {
            GetComponent<Image>().sprite = ending7;
        }
       yield return StartCoroutine(imgFadeOut());
        canClick = true;
    }
    IEnumerator imgFadeOut()
    {
       
        GetComponent<Animator>().SetBool("isFade", true);
        yield return new WaitUntil(() => _img.color.a == 0);
    } 
}
