using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
	public GameObject mainStone;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<NetworkView> ().RPC ("sendMainStoneStatus", RPCMode.Server, new object[]{Network.player.ToString(), mainStone.transform.position, mainStone.transform.rotation});
	}

	[RPC]
	public void sendMainStoneStatus(string playerID, Vector3 position, Quaternion rotation){

	}
}
