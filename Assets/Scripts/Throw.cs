using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {
	public AudioClip throwSound;
	AudioSource audioSource;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ThrowBall(){
		audioSource.PlayOneShot (throwSound);
		GameObject go = GameObject.Find("Capsule");
		Launcher l = (Launcher) go.GetComponent(typeof(Launcher));
		l.Launch ();
	}
}
