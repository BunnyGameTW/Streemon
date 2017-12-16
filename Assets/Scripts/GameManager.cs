using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager game;//singleton pattern
    [SerializeField]
    private Player _player;
    private GameObject [] _items;
    [SerializeField]
    private GameObject _BookUI;
    private void Awake()
    {
        if (game == null) {
            game = this;
        }
        Items = GameObject.FindGameObjectsWithTag("item");

    }
    public void refindItem() {
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
    public IEnumerator fadeInOut(SpriteRenderer target, float speed) {
        while (target.color.a <= 1.0f)
        {
           // Debug.Log(target.color.a);
            Color color = target.color;
            color.a += speed;
            if (color.a > 1.0f) color.a = 1.0f;
            else if (color.a < 0.0f) color.a = 0.0f;
            target.color = color;
            yield return null;
        }       
    }

    public void AddBagItem() {

    }
    public void Setactive(GameObject obj, bool isAble) {
        obj.SetActive(isAble);
       
    }
    public GameObject BookUI
    {
        get { return _BookUI; }
    }
}
