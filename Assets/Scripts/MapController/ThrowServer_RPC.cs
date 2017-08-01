using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowServer_RPC : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[RPC]
	public void createStoneOnClient(string playerID, Vector3 position, Vector3 velocity, float parentTransform, string stoneParentID){

	}

	[RPC]
	public void isTouchBoundary(string playerID){

	}

	[RPC]
	public void updateScore(string playerID, int additionalSkipTimes, Vector3 position){
		this.GetComponent<NetworkView> ().RPC ("sendInforToClientToUpdateScore", RPCMode.Others, new object[]{playerID, additionalSkipTimes, position});
		Debug.Log ("ScoreID: " + playerID);
	}

	[RPC]
	public void sendInforToClientToUpdateScore(string playerID, int additionalSkipTimes, Vector3 position){

	}
}
