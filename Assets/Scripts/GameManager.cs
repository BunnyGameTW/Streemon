using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager game;//singleton pattern
    [SerializeField]
    private Player _player;
    private GameObject [] _items;
    private void Awake()
    {
        if (game == null) {
            game = this;
        }
        Items = GameObject.FindGameObjectsWithTag("item");

    }
    private void Start()
    {
        Debug.Log(Items.Length);
    }
    public void changeScene(string name) {
        SceneManager.LoadScene(name);
    }
    public Player Player
    {
        get { return _player; }
    }
    public GameObject[] Items
    {
        get { return _items; }
        set { _items = value; }
    }
  
}
