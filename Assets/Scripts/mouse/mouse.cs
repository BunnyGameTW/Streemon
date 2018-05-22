using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class mouse : MonoBehaviour {
    Vector3 mishScrPos = Vector3.zero;
    [SerializeField]
    float moveSpeed = 4.0f;
    public enum MishState { walk , caught }
    public MishState _mishState;
    Animator ani;
    SpriteRenderer _spriteRender;
     public Vector3 cagePos;
    public SpriteRenderer mouseInCage, cheeseInCage;
    public Sprite cageCaught;
    public GameObject realMouse;
    public Transform start, end;
    Transform mouseTarPos;
    Vector3 targetPos;
    private void Awake()
    {
        _mishState = MishState.walk;
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
        if (_mishState == MishState.walk) targetPos = mouseTarPos.position;
        else
        {
            targetPos = cagePos;
            GetComponent<InteractiveItem>().interactiveDistance = 0;//can't talk
        }

         transform.position = Vector3.MoveTowards(transform.position,targetPos, moveSpeed * Time.deltaTime);
        if (transform.position.x >= targetPos.x) SetRotation(0);
        else SetRotation(180);
        if (transform.position == targetPos)
        {
            if (_mishState == MishState.walk) {
                mouseTarPos = start;
                start = end;
                end = mouseTarPos;
            }
            else
            {           
                cheeseInCage.enabled = false;
                mouseInCage.enabled = true;
                mouseInCage.transform.root.GetComponent<SpriteRenderer>().sprite = cageCaught;
                SaveData._data.giveCheese = true;
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
}
