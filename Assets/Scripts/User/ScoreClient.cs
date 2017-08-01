using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreClient : MonoBehaviour {
	public Text myScore;
	public Text myHighScore;
	public Canvas canvas_Infor;
	public Canvas canvas_HighScore;

	public Text[] infor;
	public GameObject highScore;
	public Transform parent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[RPC]
	public void sendScoreToServerToSave(string playerID, string username, float score){

	}

	[RPC]
	public void getMyScoreInDB(string playerID, string username){

	}

	[RPC]
	public void sendScoreToAClient(string playerID, string username, string score){
		if (Network.player.ToString () == playerID) {
			canvas_Infor.gameObject.SetActive (true);
			myHighScore.text = score;

			GameObject go = GameObject.Find("Capsule");
			Launcher l = (Launcher) go.GetComponent(typeof(Launcher));

			myScore.text = l.scoreText.text;
		}
	}

	[RPC]
	public void requestListHighScore(){

	}

	[RPC]
	public void sendListHighScoreToClient(string itemsDataString){
		for (int j = 0; j < parent.childCount; j++) {
			Destroy (parent.GetChild (j).gameObject);
		}
		string[] list_items;
		list_items = itemsDataString.Split ('/');
		//for (int i = 0; i < list_items.Length; i++) {
		int i = 1;
		foreach (string itemString in list_items) {
			string[] items = itemString.Split (';');
			infor [0].text = i.ToString();
			infor [1].text = items [0];
			infor [2].text = items [1];
			GameObject go =  Instantiate (highScore, parent.position, Quaternion.identity, parent);
			go.name = i.ToString() + "_" + items [0];
			i++;
		}
	}

	public void closeScoreInfor(){
		canvas_Infor.gameObject.SetActive (false);
	}

	public void openCanvasHighScore(){
		canvas_HighScore.gameObject.SetActive (true);
		this.GetComponent<NetworkView> ().RPC ("requestListHighScore", RPCMode.Server, null);
	}

	public void closeCanvasHighScore(){
		canvas_HighScore.gameObject.SetActive (false);

	}
}
