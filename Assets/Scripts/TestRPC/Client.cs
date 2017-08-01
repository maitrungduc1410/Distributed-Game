using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[RPC]
	public void sendMyRPCFunction(string myarr){
		Debug.Log ("asdada");
		Debug.Log (myarr);
	}
}
