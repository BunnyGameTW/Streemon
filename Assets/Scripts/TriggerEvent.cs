﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class TriggerEvent : MonoBehaviour {
    public UnityEvent OnEnter;
    public UnityEvent OnDown;
    public UnityEvent OnUp;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter.Invoke();
    }
    private void OnMouseDown()
    {
        OnDown.Invoke();
    }
    private void OnMouseUp()
    {
        OnUp.Invoke();
    }
}
