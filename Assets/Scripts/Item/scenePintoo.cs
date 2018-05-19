using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scenePintoo : InteractiveItem
{
    public GameObject pintooUI;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {

        if (_canInteractive && !SaveData._data.canEnterPurpleRoom)
        {
            pintooUI.SetActive(true);
            GameManager.game.Player.Playerstate = Player.PlayerState.interactive;
        }
    }
}
