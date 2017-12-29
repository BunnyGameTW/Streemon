using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class mouse : MonoBehaviour {
    Vector3 mishScrPos = Vector3.zero;
    [SerializeField]
    float moveSpeed = 4.0f;
    public enum MishState { idle, walk , caught }
    public MishState _mishState;
    Animator ani;
    SpriteRenderer _spriteRender;
     public Vector3 cagePos;
    public SpriteRenderer mouseInCage, cheeseInCage;
    public Sprite cageCaught;
    public GameObject realMouse;
    public Transform start, end;
    Transform mouseTarPos;
    private void Awake()
    {
        _mishState = MishState.idle;
        ani = GetComponent<Animator>();
        _spriteRender = GetComponentInChildren<SpriteRenderer>();
    }
    // Use this for initialization
    void Start () {
        transform.position = start.position;   
        mouseTarPos = end;
        cagePos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        
        if (_mishState == MishState.idle)
        {   
            transform.position = Vector3.MoveTowards(transform.position, mouseTarPos.position, moveSpeed * Time.deltaTime);
            if (transform.position.x >= end.position.x) SetRotation(0);
            else SetRotation(180);
            if (transform.position == mouseTarPos.position)
            {
                mouseTarPos = start;
                start = end;
                end = mouseTarPos;
            }
        }
        else if(_mishState == MishState.caught)
        {
            //go to cage
            transform.position = Vector3.MoveTowards(transform.position, cagePos, moveSpeed * Time.deltaTime);
            if (transform.position.x >= cagePos.x) SetRotation(0);
            else SetRotation(180);
            if (transform.position == cagePos) { //set mouse in cage display
                cheeseInCage.enabled = false;
                mouseInCage.enabled = true;
                mouseInCage.transform.root.GetComponent<SpriteRenderer>().sprite = cageCaught;
                realMouse.GetComponent<BoxCollider2D>().enabled = true; 
                GameManager.game.refindItem();
                Destroy(this.gameObject);
            }
        }
    }

    public void SetRotation(int rot)
    {
        transform.eulerAngles = new Vector3(0, rot, 0);
    }
    public void SetMishState(int state)
    {
        _mishState = (MishState)state;
    }

    public MishState Mishstate
    {
        get { return _mishState; }
        set { _mishState = value; }
    }
}
