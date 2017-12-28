using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeSprite : MonoBehaviour {
    public Sprite sprite;
    Sprite _sprite;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void change()
    {
         GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
