using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCapsule : MonoBehaviour {
	public GameObject go;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		if (GUI.Button (new Rect (100, 350, 100, 50), "Add Stone")) {
			Instantiate (go);
		}
	}
}
