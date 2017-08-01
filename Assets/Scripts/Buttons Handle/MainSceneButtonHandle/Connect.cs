using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<NetworkView>().RPC("AddPlayerToList", RPCMode.Server, new object[]{Network.player.ToString(), Network.player.ipAddress, Application.platform.ToString()});
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[RPC]
	public void PrintABC(){
		Debug.Log ("HAHAHAHA");
	}

	[RPC]
	void AddPlayerToList(string loginName, string ip, string platform)
	{

	}
}
