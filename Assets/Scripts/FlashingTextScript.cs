using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingTextScript : MonoBehaviour {

	public GUIText myText;

	void Start(){
		myText = GetComponent<GUIText> ();
	}

	void Update(){
		//guiText.renderer.ma
		Color myColor = myText.material.color;
		myColor.a = (Mathf.Sin(Time.time * 2.0f) + 1.0f)/2.0f;
		//myText.material.color.a = (Mathf.Sin(Time.time * 2.0f) + 1.0f)/2.0f;
		myText.material.color = myColor;
	}

	void Flash(){
		//Color myColor = flashingText.material.color;
		//myColor.a = (Mathf.Sin(Time.time * 2.0f) + 1.0f)/2.0f;
		//flashingText.material.color = myColor;
		//guiText.material.color.a = (Mathf.Sin(Time.time * speed) + 1.0)/2.0;
	}

}
