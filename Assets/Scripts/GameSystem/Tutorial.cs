using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
    public GameObject purple,player;
    public Transform kitchen;
    public float speed;
	// Use this for initialization
	void Start () {
        StartCoroutine(purpleMove(speed));
    }

    
    IEnumerator purpleMove(float speed)
    {
        yield return new WaitForSeconds(0.2f);
        Camera.main.GetComponent<CameraFollow>().target = purple;
        Camera.main.GetComponent<CameraFollow>().offset.y = -0.02f;
        GameManager.game.Player.Playerstate = Player.PlayerState.interactive;
        while (purple.transform.position.x != kitchen.position.x)
        {
            purple.transform.position = Vector3.MoveTowards(purple.transform.position, kitchen.position, speed * Time.deltaTime);
            yield return null;
        }
        purple.GetComponent<Animator>().SetBool("isWalk", false);
        yield return new WaitForSeconds(1.0f);
        Camera.main.GetComponent<CameraFollow>().target = player;
        Camera.main.GetComponent<CameraFollow>().offset.y = -0.12f;
        GameManager.game.Player.Playerstate = Player.PlayerState.idle;
       
    }
   
}
