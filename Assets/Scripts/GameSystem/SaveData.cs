using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SaveData :MonoBehaviour{
    
    public string nowScene;
    public Vector3 playerPos;
    [Serializable]
    public struct PlayerInfo
    {
        public List<string> itemName;
        public Vector3 playerPos;
    }
    [Serializable]
    public struct CharsInfo
    {
       public enum TalkStatus { premissionNotComplete, firstTalk, canDoMission, missionComplete }
       public TalkStatus talkStatus;
       public string name;
       public bool firstTalk;
       public int talkNum;
       public bool charTalkFirst;
    }
    [Serializable]
    public struct SceneInfo
    {
        public string name;
        public bool firstIn;
        public bool firstTalk;
        public string[] itemName;

    }
    public SceneInfo[] rooms;
    public CharsInfo[] chars;
    public PlayerInfo player;
    public bool tutorialEnd;
    public static SaveData _data = new SaveData();

    public SaveData()
    {
        player.itemName = new List<string>();
        rooms = new SceneInfo[6];
        chars = new CharsInfo[6];
        setupRooms();
        tutorialEnd = false;
        setupChars();
    }
    void setupChars()
    {
        chars[0].name = "bird";
        chars[1].name = "blue";
        chars[2].name = "girl";
        chars[3].name = "flower";
        chars[4].name = "purple";
        chars[5].name = "fakeMouse";

        for (int i = 0; i < chars.Length; i++)
        {
            chars[i].firstTalk = true;
            chars[i].talkNum = 1;
            chars[i].talkStatus = CharsInfo.TalkStatus.firstTalk;//
           if(i == 2 || i == 1) chars[i].charTalkFirst = false;
           else chars[i].charTalkFirst = true;
        }
        chars[2].talkNum = 14;
        chars[1].talkNum = 32;
    }
    public CharsInfo getCharInfo(string charName) {
        for (int i = 0; i < chars.Length; i++)
        {
            if (charName == chars[i].name) return chars[i];
        }
        return chars[0];//error
    }
    public void setCharInfo(string charName, CharsInfo charInfo) {
        for (int i = 0; i < chars.Length; i++)
        {
            if (charName == chars[i].name) chars[i] = charInfo;
        }
       
    }

    void setupRooms() {

        rooms[0].name = "Smain";
        rooms[1].name = "Sbalcony";
        rooms[2].name = "SredRoom";
        rooms[3].name = "SblueRoom";
        rooms[4].name = "SstorageRoom";
        rooms[5].name = "SpurpleRoom";

        for (int i = 0; i < rooms.Length; i++)
        {
            rooms[i].firstTalk = rooms[i].firstIn = true;
        }
    }
    public SceneInfo getRoomInfo(string roomName)
    {
       for(int i = 0;i < rooms.Length; i++)
        {
            if (roomName == rooms[i].name) return rooms[i];

        }
       return rooms[0];//error
    }
    public void setRoomInfo(string roomName, SceneInfo roomInfo)
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            if (roomName == rooms[i].name) rooms[i] = roomInfo;

        }
    }
}
