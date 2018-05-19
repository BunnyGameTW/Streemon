using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public Text barTxt;
    public Image barImg;
    public GameObject progressBar;
    public Image fadeImg;
    AsyncOperation operation;
    public void loadScene(string name)
    {
        progressBar.SetActive(true);
        StartCoroutine(loadSceneAsync(name));
    }
    IEnumerator loadSceneAsync(string name)
    {
        int displayProgress = 0;
        int toProgress = 0;
        operation = SceneManager.LoadSceneAsync(name);
        operation.allowSceneActivation = false;
        while (operation.progress < 0.9f)
        {
            toProgress = (int)(operation.progress * 100);

            while (displayProgress < toProgress)
            {
                ++displayProgress;
                barImg.fillAmount = displayProgress * 0.01f;
                barTxt.text = displayProgress + "%";
            }
            yield return null;
        }
        toProgress = 100;

        while (displayProgress < toProgress)
        {
            ++displayProgress;
            barImg.fillAmount = displayProgress * 0.01f;
            barTxt.text = displayProgress + "%";
            yield return null;
        }
        StartCoroutine(fadeIn());
        SoundManager.sound.bgmFadeOut();
    }
    IEnumerator fadeIn()
    {
        fadeImg.GetComponent<Animator>().SetBool("isFade", true);
        yield return new WaitUntil(() => fadeImg.color.a == 1);
        operation.allowSceneActivation = true;
    }
}
