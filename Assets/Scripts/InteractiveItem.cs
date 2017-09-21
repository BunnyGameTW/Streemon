using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveItem : MonoBehaviour {

    public string name;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            if (name == "storageDoor") {
                //if player click

            }
        }
    }
}
