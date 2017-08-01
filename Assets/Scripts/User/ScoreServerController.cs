using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreServerController : MonoBehaviour {
	string HighScoreURL = "http://localhost/SkippingStones/SelectHighScore.php";
	string ScoreUsernameURL = "http://localhost/SkippingStones/SelectScoreByUsername.php";
	string UpdateScoreURL = "http://localhost/SkippingStones/UpdateScore.php";
	public Text[] infor;
	public GameObject highScore;
	public Transform parent;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void loadHighScore(){
		StartCoroutine (IGetHighScore ());
	}
	IEnumerator IGetHighScore(){
		for (int j = 0; j < parent.childCount; j++) {
			Destroy (parent.GetChild (j).gameObject);
		}
		WWW itemsDataPrint = new WWW (HighScoreURL);
		yield return itemsDataPrint;
		string itemsDataString = itemsDataPrint.text;
		//Debug.Log (itemsDataString);
		itemsDataString = itemsDataString.Substring (0, itemsDataString.Length - 1);
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
		//}

	}

	[RPC]
	public IEnumerator sendScoreToServerToSave(string playerID, string username, float score){
		WWWForm formScore = new WWWForm();
		formScore.AddField ("usernamePost", username);
		WWW itemsScore = new WWW (ScoreUsernameURL, formScore);
		yield return itemsScore;
		string itemsDataString = itemsScore.text;
		string[] items = itemsDataString.Split (';');
		if (score > float.Parse (items [1])) {
			WWWForm formUpdateScore = new WWWForm();
			formUpdateScore.AddField ("usernamePost", username);
			formUpdateScore.AddField ("scorePost", score.ToString());
			WWW itemsDataUpdate = new WWW (UpdateScoreURL, formUpdateScore);
		}
		Debug.Log (score + "((((())))" + items [1]);
	}

	[RPC]
	public IEnumerator getMyScoreInDB(string playerID, string username){
		WWWForm formScore = new WWWForm();
		formScore.AddField ("usernamePost", username);
		WWW itemsScore = new WWW (ScoreUsernameURL, formScore);
		yield return itemsScore;
		string itemsDataString = itemsScore.text;
		string[] items = itemsDataString.Split (';');
		this.GetComponent<NetworkView> ().RPC ("sendScoreToAClient", RPCMode.Others, new object[]{playerID, username, items[1]});
	}

	[RPC]
	public void sendScoreToAClient(string playerID, string username, string score){

	}

	[RPC]
	public IEnumerator requestListHighScore(){
		WWW itemsDataPrint = new WWW (HighScoreURL);
		yield return itemsDataPrint;
		string itemsDataString = itemsDataPrint.text;
		itemsDataString = itemsDataString.Substring (0, itemsDataString.Length - 1);
		this.GetComponent<NetworkView> ().RPC ("sendListHighScoreToClient", RPCMode.Others, new object[]{itemsDataString});
	}

	[RPC]
	public void sendListHighScoreToClient(string itemsDataString){

	}
}
