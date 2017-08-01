using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementSyncServer : MonoBehaviour {
	public Transform parent;
	private GameObject AjClient;
	private GameObject mainStone;
	public Text text;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[RPC]
	public void sendCasting(string playerID){
		AjClient = GameObject.Find ("ClientMap" + playerID).transform.GetChild(0).gameObject;
		AjClient.GetComponent<Animator>().SetTrigger ("isCastingOnServer");
		//Debug.Log(playerID);
	}

	[RPC]
	public void sendJumping(string playerID){
		AjClient = GameObject.Find ("ClientMap" + playerID).transform.GetChild(0).gameObject;
		AjClient.GetComponent<Animator>().SetTrigger ("isJumping");
	}

	[RPC]
	public void sendTranslationAnimations(string playerID, bool runningState, bool idleState){
		AjClient = GameObject.Find ("ClientMap" + playerID).transform.GetChild(0).gameObject;
		AjClient.GetComponent<Animator> ().SetBool ("isRunning", runningState);
		AjClient.GetComponent<Animator> ().SetBool ("isIdle", idleState);
	}

//	[RPC]
//	public void sendTransform(string playerID, Vector3 position, Quaternion rotation){
//		AjClient = GameObject.Find ("ClientMap" + playerID).transform.GetChild(0).gameObject;
//		AjClient.transform.position = position;
//		AjClient.transform.rotation = rotation;
//		text.text = position.x + "//" + position.y + "//" + position.z;
//	}

//	[RPC]
//	public void sendTranslation(string playerID, Vector3 translate, Vector3 rotate){
//		AjClient = GameObject.Find ("ClientMap" + playerID).transform.GetChild(0).gameObject;
//		AjClient.transform.Translate (translate);
//		AjClient.transform.Rotate (rotate);
//	}

	[RPC]
	public void syncTransform(string playerID, Vector3 position, Quaternion rotation){
		AjClient = GameObject.Find ("ClientMap" + playerID).transform.GetChild(0).gameObject;
		AjClient.transform.position = position;
		AjClient.transform.rotation = rotation;
	}

//	[RPC]
//	public void sendStoneState(string playerID){
//		mainStone = GameObject.Find ("ClientMap" + playerID).transform.GetChild(2).gameObject;
//		Launcher l = (Launcher) mainStone.GetComponent(typeof(Launcher));
//		l.hasBeenThrown = true;
//	}
//
	[RPC]
	public void syncStoneTransform(string playerID, Vector3 position, Quaternion rotation, Vector3 velocity){
		mainStone = GameObject.Find ("ClientMap" + playerID).transform.GetChild(2).gameObject;
		mainStone.transform.position = position;
		mainStone.transform.rotation = rotation;
		mainStone.GetComponent<Rigidbody> ().velocity = velocity;
		Launcher l = (Launcher) mainStone.GetComponent(typeof(Launcher));
		l.hasBeenThrown = true;
		mainStone.GetComponent<Rigidbody> ().useGravity = true;
	}

//	[RPC]
//	public void sendVelocityToServer(string playerID, Vector3 velocity){
//		mainStone = GameObject.Find ("ClientMap" + playerID).transform.GetChild(2).gameObject;
//		mainStone.GetComponent<Rigidbody> ().useGravity = true;
//		Launcher l = (Launcher) mainStone.GetComponent(typeof(Launcher));
//		l.hasBeenThrown = true;
//		mainStone.GetComponent<Rigidbody>().velocity = velocity;
//		Debug.Log (velocity);
//	}
}
