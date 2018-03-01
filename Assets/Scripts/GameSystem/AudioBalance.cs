using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioBalance : MonoBehaviour {
    public AudioMixer auMixer;
    public float vloumn;
	// Use this for initialization
	void Start () {
        auMixer.SetFloat("volume", vloumn);
        
    }
	
	// Update is called once per frame
	void Update () {
        auMixer.SetFloat("volume", vloumn);
    
    }
}
