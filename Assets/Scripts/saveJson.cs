using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class saveJson : MonoBehaviour
{
    public Player playerdata;       //Read the Player's playerPos in 6th.
    public SavedVariable itemUse;   //Read the item on/off status.

    private string FilePath_player;        //Save the player FilePath.
    private string FilePath_item;        //Save the item FilePath.
    //Build the .txt file to save and Load if there's a file.
    void Start () {
        FilePath_player = Path.Combine(Application.dataPath,"player.json");
        FilePath_item = Path.Combine(Application.dataPath, "item.json");
        //Above is same as Below.
        //FilePath = Application.dataPath + "/save.txt"
        load();
    }
	
	// Update the saved variables per frame
	void Update () {
        save();
    }

    public void save()
    {
        string jsonString1 = JsonUtility.ToJson(playerdata);
        File.WriteAllText(FilePath_player, jsonString1);
        string jsonString2 = JsonUtility.ToJson(itemUse);
        File.WriteAllText(FilePath_item, jsonString2);
    }

    public void load()
    {
        string jsonString1 = File.ReadAllText(FilePath_player);
        JsonUtility.FromJsonOverwrite(jsonString1, playerdata);
        string jsonString2 = File.ReadAllText(FilePath_player);
        JsonUtility.FromJsonOverwrite(jsonString2, itemUse);
    }
        
}
