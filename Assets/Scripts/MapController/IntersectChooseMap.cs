using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectChooseMap : MonoBehaviour {
	//public GameObject go;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(){
		GameObject go = GameObject.Find ("Plane");
		ClientChooseMap l = (ClientChooseMap) go.GetComponent(typeof(ClientChooseMap));
		l.isDragable = false;
	}
}
