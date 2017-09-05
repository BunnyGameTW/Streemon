using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    Vector3 mouseScrPos = Vector3.zero;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePoint = Input.mousePosition;
             mouseScrPos = Camera.main.ScreenToWorldPoint(mousePoint);
            mouseScrPos.z = 0.0f;
            Debug.Log(mouseScrPos);
            float distanceX = mouseScrPos.x - transform.position.x;//offsetX
           
                                            // robotPoint = gameObject.transform.position;
                                            //  lastPlayerPos = transform.position;
                                            //  move(distanceX, 0);
        }
        move(1, 1);


    }
    void move(float x,float y) {
       // transform.position = Vector3.Lerp(transform.position, mouseScrPos, 1.5f * Time.deltaTime);
       if(transform.position )
       // transform.position += new Vector3(x, y, 0) * Time.deltaTime;//speed will change by clickPoint

    }
}
