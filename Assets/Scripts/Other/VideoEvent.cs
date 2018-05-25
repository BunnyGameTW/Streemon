using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoEvent : MonoBehaviour
{
    VideoPlayer videoPlayer;
    AudioSource auSource;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(init());
    }
    IEnumerator init()
    {
       yield return new WaitForSeconds(2.0f);
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;
        auSource = GetComponent<AudioSource>();
        videoPlayer.Play();
        auSource.Play();
    }
   
    void EndReached(VideoPlayer vp)
    {     
        GameManager.game.GetComponent<LevelLoader>().loadScene("ARcamera");      
    }
   
}