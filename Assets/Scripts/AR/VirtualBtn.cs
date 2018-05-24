using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VirtualBtn : MonoBehaviour,IVirtualButtonEventHandler {
    public GameObject VBtn;
	// Use this for initialization
	void Start () {
        VBtn.transform.Rotate(0, -90, 0, 0);
        VBtn.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnButtonPressed(VirtualButtonBehaviour VBtn) {
        Debug.Log("Btn Press Down!");
    }
    public void OnButtonReleased(VirtualButtonBehaviour VBtn)
    {
        Debug.Log("Btn Released!");
    }
}
