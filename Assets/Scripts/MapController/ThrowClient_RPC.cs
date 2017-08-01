using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowClient_RPC : MonoBehaviour {
	public GameObject stoneToClone;
	public GameObject mainStone;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[RPC]
	public void createStoneOnClient(string playerID, Vector3 position, Vector3 velocity, float parentTransform, string stoneParentID){
		if (playerID == Network.player.ToString ()) {
				GameObject go = (GameObject)Instantiate(stoneToClone);
				go.name = "Stone" + stoneParentID;
				go.transform.parent = null;
				go.GetComponent<Rigidbody> ().useGravity = true;
				go.transform.position = position;
				go.GetComponent<Rigidbody> ().velocity = velocity;

				RPC_Launcher l = (RPC_Launcher) go.GetComponent(typeof(RPC_Launcher));
				l.parentTransform = parentTransform;
		}
	}

	//khi tren server nem da den bien thi da o client chuyen tu may nay sang may khac
	[RPC]
	public void isTouchBoundary(string playerID){
		if (Network.player.ToString () == playerID) {
			GameObject mainStone = GameObject.Find ("Capsule");
			Launcher l = (Launcher) mainStone.GetComponent(typeof(Launcher));
			l.isTouch = true;
			mainStone.SetActive (false);
		}
	}

	//khi đá dừng lại thì gửi thông số về cho client ban đầu để tính toán điểm
	[RPC]
	public void updateScore(string playerID, int additionalSkipTimes, Vector3 position){
		
	}

	[RPC]
	public void sendInforToClientToUpdateScore(string playerID, int additionalSkipTimes, Vector3 position){
		if (Network.player.ToString () == playerID) {
			//GameObject mainStone = GameObject.Find ("Capsule");
			Launcher l = (Launcher) mainStone.GetComponent(typeof(Launcher));
			l.CountTotalScoreInAllClients (additionalSkipTimes, position);
			Debug.Log (additionalSkipTimes + "++//" + position);
		}
	}

}
