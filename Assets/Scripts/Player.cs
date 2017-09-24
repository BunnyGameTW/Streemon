using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    Vector3 mouseScrPos = Vector3.zero;
    [SerializeField]
    float moveSpeed = 1.0f;
    enum PlayerState { idle, walk, updown, interactive}
    PlayerState playerState;
    Animator ani;
    delegate void PlayerDelegate();
    PlayerDelegate playerClick;
    private void Awake()
    {
        playerState = PlayerState.idle;
        ani = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetMouseButtonDown(0))
        {
            //delegate
            if (playerClick != null) {
                playerClick();
            }
            //interactive item

            //walk
            mouseScrPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseScrPos.z = 0.0f;
            mouseScrPos.y = transform.position.y;
            Debug.Log(mouseScrPos);
            if(mouseScrPos.x -transform.position.x < 0) transform.eulerAngles = new Vector3(0, 180, 0);
            else transform.eulerAngles = new Vector3(0, 0, 0);
            playerState = PlayerState.walk;
        }

        if (playerState == PlayerState.walk) {
            playerState = move();
            
        }
        else if (playerState == PlayerState.idle){

        }
        setAnimation(playerState);
    }

    PlayerState move() {
        transform.position = Vector3.MoveTowards(transform.position, mouseScrPos, moveSpeed * Time.deltaTime);

        if (transform.position == mouseScrPos) return PlayerState.idle;
        else return PlayerState.walk;
    }
    void setAnimation(PlayerState state) {
        if (state == PlayerState.idle) ani.SetBool("isWalk", false);
        else if (state == PlayerState.walk) ani.SetBool("isWalk",true);
    }
}
