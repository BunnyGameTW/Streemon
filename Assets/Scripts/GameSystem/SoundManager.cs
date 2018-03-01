using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
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
    public AudioMixerGroup auGroup;
    public float mixerVol;
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
        if (SceneManager.GetActiveScene().name != "Sout" && SceneManager.GetActiveScene().name != "Stitle") seSource = GameManager.game.Player.GetComponent<AudioSource>();//get player se
        for (int i = 0; i < musics.Length; i++)//set bgm
        {
            if (SceneManager.GetActiveScene().name == musics[i].sceneName)
            {
                bgmSource = gameObject.AddComponent<AudioSource>();
                bgmSource.outputAudioMixerGroup = auGroup;
                bgmSource.clip = musics[i].bgm;               
                bgmSource.loop = true;
                bgmSource.Play();
            }
        }
        if (SceneManager.GetActiveScene().name == "Sopen")//TODO:調整數值
        {
            mixerVol = 20;         
        }
        else
        {
            mixerVol = 0;
        }
        auGroup.audioMixer.SetFloat("volume", mixerVol);
    }
    public void playOne(AudioClip se) {
        seSource.PlayOneShot(se);
    }
    public void stopSE() {
        seSource.Stop();
    }
    public void bgmFadeIn()
    {
        //-10~0 10~20
        auGroup.audioMixer.SetFloat("volume", mixerVol);

    }
}
