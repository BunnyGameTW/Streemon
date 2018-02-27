using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour {
    public Text barTxt;
    public Image barImg;
    public GameObject progressBar;
    public void loadScene(string name)
    {
        progressBar.SetActive(true);
        StartCoroutine(loadSceneAsync(name));
    }
    IEnumerator loadSceneAsync(string name)
    {
        int displayProgress = 0;
        int toProgress = 0;
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);     
        operation.allowSceneActivation = false;
        while (operation.progress < 0.9f)
        {
            toProgress = (int)operation.progress * 100;

            while (displayProgress < toProgress)
            {
                ++displayProgress;
                barImg.transform.localScale = new Vector3(displayProgress * 0.01f, barImg.transform.localScale.y, barImg.transform.localScale.z);
                barTxt.text = displayProgress + "%";               
            }                   
            yield return null;
        }
        toProgress = 100;

        while (displayProgress < toProgress)
        {
            ++displayProgress;
            barImg.transform.localScale = new Vector3(displayProgress * 0.01f, barImg.transform.localScale.y, barImg.transform.localScale.z);
            barTxt.text = displayProgress + "%";
            yield return null;
        }
        operation.allowSceneActivation = true;
        //TODO:加上轉場黑幕、音樂變小聲
    }

}
