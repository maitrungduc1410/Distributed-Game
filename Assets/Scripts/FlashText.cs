using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashText : MonoBehaviour {
	public Text myText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Color myColor = myText.material.color;
		myColor.a = (Mathf.Sin(Time.time * 2.0f) + 1.0f)/2.0f;
		//myText.material.color.a = (Mathf.Sin(Time.time * 2.0f) + 1.0f)/2.0f;
		myText.material.color = myColor;
	}
}
