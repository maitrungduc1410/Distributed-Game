using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSController : MonoBehaviour {
	public Text FPSText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate(){
		FPSText.text = "FPS: " + ((int)(1.0f / Time.smoothDeltaTime)).ToString ();
	}
}
