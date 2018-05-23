using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purple : MonoBehaviour {
    public GameObject purple;
    public Transform targetPoint;
    public float speed;
    void Start()
    {
        StartCoroutine(purpleMove(speed));
    }


    IEnumerator purpleMove(float speed)
    {
        yield return new WaitForSeconds(0.2f);
        Camera.main.GetComponent<CameraFollow>().target = purple;
        while (purple.transform.position.x != targetPoint.position.x)
        {
            purple.transform.position = Vector3.MoveTowards(purple.transform.position, targetPoint.position, speed * Time.deltaTime);
            yield return null;
        }
        purple.GetComponentInChildren<Animator>().SetBool("isWalk", false);
        yield return new WaitForSeconds(0.5f);
        //TODO:
        GameManager.game.Player.Playerstate = Player.PlayerState.talk;
        if (SaveData._data.ending == 6) GameManager.game.SetTalk("purple",0);
        else GameManager.game.SetTalk("purple", 2);
        GameManager.game.Setactive(GameManager.game.TalkUI, true);

        //change scene
        //   GameManager.game.changeSceneWithFade("Sout");

    }
}
