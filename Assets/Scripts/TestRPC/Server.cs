using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.A)){
			string[] arr = { "MTD1", "MTD2", "MTD3" };
			this.GetComponent<NetworkView> ().RPC ("sendMyRPCFunction", RPCMode.All, new object[]{"MTDDDD"});
			Debug.Log ("hehehe");
		}
	}

	[RPC]
	public void sendMyRPCFunction(string myarr){

	}
}
