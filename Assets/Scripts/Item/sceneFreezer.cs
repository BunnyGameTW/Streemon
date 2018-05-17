using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneFreezer : MonoBehaviour {
    public Sprite openFreezer;
    public GameObject trees;
    public GameObject seed, seeds;
	// Use this for initialization
	void Start () {
        if (SaveData._data.treeIsUnlock) SetUnlock();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetUnlock()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GameObject.Find("flower_target").SetActive(false);
        GetComponent<SpriteRenderer>().sprite = openFreezer;
        trees.SetActive(false);
        seed.transform.position = seeds.transform.position = transform.position;
        //set freezer open and seed
        //scene tree disappear
    }
}
