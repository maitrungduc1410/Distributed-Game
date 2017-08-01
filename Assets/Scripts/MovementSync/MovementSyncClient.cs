using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSyncClient : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//	[RPC]
//	public void sendTransform(string playerID, Vector3 position, Quaternion rotation){
//
//	}

	[RPC]
	public void sendCasting(string playerID){

	}

	[RPC]
	public void sendJumping(string playerID){

	}

	[RPC]
	public void sendTranslationAnimations(string playerID, bool runningState, bool idleState){

	}

//	[RPC]
//	public void sendTranslation(string playerID, Vector3 translate, Vector3 rotate){
//
//	}

	[RPC]
	public void syncTransform(string playerID, Vector3 position, Quaternion rotation){

	}

	//Ngay khi nem o client thi o server set lai gia tri hasbeenthrown
//	[RPC]
//	public void sendStoneState(string playerID){
//
//	}
//
//	//For Stone
//
	[RPC]
	public void syncStoneTransform(string playerID, Vector3 position, Quaternion rotation, Vector3 velocity){

	}

//	[RPC]
//	public void sendVelocityToServer(string playerID, Vector3 velocity){
//
//	}
}
