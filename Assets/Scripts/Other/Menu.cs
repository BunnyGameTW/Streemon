﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void setstop()
    {
        Time.timeScale = 0;
    }
    public void setStart()
    {
        Time.timeScale = 1;
    }
}
