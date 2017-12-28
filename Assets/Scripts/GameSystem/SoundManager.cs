using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {
    [Serializable]
    public struct music
    {
        public string sceneName;
        public AudioClip bgm;
    }
    [Serializable]
    public struct playerSE
    {
        public AudioClip pick;
        public AudioClip jump;
    }
    [Serializable]
    public struct uiSE {
        public AudioClip [] click;
        public AudioClip[] talk;
    }
    public playerSE playerse;
    public uiSE uise;
    public music[] musics;
    AudioSource bgmSource, seSource;
    public static SoundManager sound;//singleton pattern
    // Use this for initialization
    private void Awake()
    {
        if (sound == null)
        {
            sound = this;
        }
        if(SceneManager.GetActiveScene().name =="SblueRoom") seSource = GameManager.game.Player.GetComponent<AudioSource>();
    }
    void Start () {
        seSource = GameManager.game.Player.GetComponent<AudioSource>();
        for (int i = 0; i < musics.Length; i++)
        {
            if (SceneManager.GetActiveScene().name == musics[i].sceneName)
            {
                bgmSource = gameObject.AddComponent<AudioSource>();
                bgmSource.clip = musics[i].bgm;               
                bgmSource.loop = true;
                bgmSource.Play();
            }
        }
       
	}
    public void playOne(AudioClip se) {
        seSource.PlayOneShot(se);
    }
    public void stopSE() {
        seSource.Stop();
    }
}
