using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
	public AudioClip sound;
	AudioSource source;
	void Awake(){
		source = GetComponent<AudioSource> ();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playClickSound(){
		source.PlayOneShot (sound);
		Debug.Log ("asdasD");
	}

}
