using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SaveData {
    public int ending;
    public string nowScene;
    public Vector3 playerPos;
    public bool playerHasBook;
    public bool flowerIsSmall;
    public bool treeIsUnlock;
    public bool curtainIsOpen;
    public bool canEnterPurpleRoom;
    public bool mainDoorIsLock;
    public bool giveCheese;
    public float playSpeed;
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
    public bool[] hasDiary;
    public static SaveData _data = new SaveData();

    public SaveData()
    {
        ending = 0;
        player.itemName = new List<string>();
        playerHasBook = false;
        rooms = new SceneInfo[6];
        chars = new CharsInfo[8];
        setupRooms();
        tutorialEnd = false;
        setupChars();
        hasDiary = new bool[16];
        setupDiary();
        flowerIsSmall = false;
        treeIsUnlock = false;
        curtainIsOpen = false;
        canEnterPurpleRoom = false;
        mainDoorIsLock = false;
        giveCheese = false;
        playSpeed = 0.07f;
    }
    void setupChars()
    {
        chars[0].name = "bird";
        chars[1].name = "blue";
        chars[2].name = "girl";
        chars[3].name = "flower";
        chars[4].name = "purple";
        chars[5].name = "fakeMouse";
        chars[6].name = "pizza";
        chars[7].name = "freezer";
        for (int i = 0; i < chars.Length; i++)
        {         
            chars[i].talkNum = 1;
            if (i == 0 || i == 5 || i == 6) chars[i].talkStatus = CharsInfo.TalkStatus.firstTalk;//
            else if (i == 3 || i == 7) chars[i].talkStatus = CharsInfo.TalkStatus.canDoMission;//set flower and freezer can talk
            else chars[i].talkStatus = CharsInfo.TalkStatus.premissionNotComplete;//
           
          if (i==7 || i == 3 || i == 2 || i == 1 ) chars[i].charTalkFirst = false;
           else chars[i].charTalkFirst = true;
        }
        chars[2].talkNum = 14;//girl first talk
        chars[1].talkNum = 32;//blue first talk   
        chars[3].talkNum = 110;//flower
        chars[7].talkNum = 111;//freezer
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
    
    void setupDiary()
    {
        hasDiary[0] = hasDiary[1] = true;
        for (int i = 2;i < hasDiary.Length; i++)
        {
            hasDiary[i] = false;
        }
    }
    public void setDiaryInfo(bool [] diarys)
    {
        hasDiary = diarys;
        int x = 0;
        foreach (bool _item in hasDiary)
        {        
            x++;
        }
    }
    public bool [] getDiaryInfo()
    {
        return hasDiary;
    }
}
