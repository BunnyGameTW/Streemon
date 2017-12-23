using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SaveData :MonoBehaviour{
    
    public string nowScene;
    public Vector3 playerPos;
    [Serializable]
    public struct CharsInfo
    {    
       public bool firstTalk;
       public int talkNum;
    }
    [Serializable]
    public struct SceneInfo
    {
        public string name;
        public bool firstTalk;
        public Vector3[] doorPos;

    }
    public CharsInfo purple, blue, girl, bird, flower;
    public SceneInfo blueRoom, redRoom, storageRoom, balcony;
    public static SaveData _data = new SaveData();

    SaveData()
    {
        purple.firstTalk = blue.firstTalk = girl.firstTalk = bird.firstTalk = flower.firstTalk = true;
        blueRoom.firstTalk = redRoom.firstTalk = storageRoom.firstTalk = balcony.firstTalk = true;
    }
   
}
