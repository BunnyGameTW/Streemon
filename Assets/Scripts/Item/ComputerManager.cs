using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ComputerManager : MonoBehaviour {
    public Computer[] computers;
    public GameObject billy;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < computers.Length; i++)
            computers[i].OnButtonClick += this.OnButtonClick;
	}
    void OnButtonClick(object sender, EventArgs args)
    {

        //check password
        if (computers[0].IsCorrect && computers[1].IsCorrect && computers[2].IsCorrect && computers[3].IsCorrect)
        {
            //play sound
            //fade in out
            computers[0].canChange = computers[1].canChange = computers[2].canChange = computers[3].canChange = false;
            StartCoroutine(fading());
          
        }
    }
   public IEnumerator fading()
    {
       yield return StartCoroutine(GameManager.game.fadeIn());
        StartCoroutine(GameManager.game.fadeOut());
        billy.SetActive(true);
        gameObject.SetActive(false);
        GameManager.game.Player.Playerstate = Player.PlayerState.interactive;
    }
}
