using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectServer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InitNet ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void InitNet()
	{
		//
		Network.Connect("192.168.0.107", Constants.cServerPort);
	}

	void OnConnectedToServer(){
		Debug.Log ("Success");
	}
}
