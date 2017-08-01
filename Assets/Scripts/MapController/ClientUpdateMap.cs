using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientUpdateMap : MonoBehaviour {
	private string[] list_items;
	//string PrintMapStateURL = "http://localhost/SkippingStones/PrintMapState.php";
	//string DeleteMapStateURL = "http://localhost/SkippingStones/DeleteMapState.php";
	public GameObject[] listWalls;
	public GameObject[] listBoundaries;
	public GameObject toRightUp;
	public GameObject toRightDown;
	public Transform parent;
	// Use this for initialization
	void Start () {
		this.GetComponent<NetworkView> ().RPC ("SendSignalToServerWhenBegin", RPCMode.Server, Network.player.ToString());
//		WWWForm form = new WWWForm ();
//		form.AddField ("clientIDPost", Network.player.ToString());
//		WWW itemsData = new WWW (PrintMapStateURL, form);
//		yield return itemsData;
//		string itemsDataString = itemsData.text;
//		//print (itemsDataString);
//
//
//		list_items = itemsDataString.Split ('/');
//		string[] items1 = list_items [0].Split (';');
//		//string[] items2 = list_items [1].Split (';');
//		if (items1 [2].Contains ("Right")){
//			listWalls [0].SetActive (false);
//			listBoundaries [0].SetActive (true);
//		}
//		if (items1 [2].Contains ("Left")) {
//			listWalls [1].SetActive (false);
//			listBoundaries [1].SetActive (true);
//		}
//		if (items1 [2].Contains ("Front")) {
//			listWalls [2].SetActive (false);
//			listBoundaries [2].SetActive (true);
//		}
//
//
//
//		if (items1 [2].Contains ("Right") || items1 [2].Contains ("Left")) {
//			GameObject wallRightDown = Instantiate (toRightDown);
//			GameObject wallRightUp = Instantiate (toRightUp);
//
//			wallRightUp.transform.position = new Vector3 (float.Parse (items1 [3]), float.Parse (items1 [4]), float.Parse (items1 [5]));
//			wallRightUp.transform.rotation = Quaternion.Euler (float.Parse (items1 [6]), float.Parse (items1 [7]), float.Parse (items1 [8]));
//			wallRightUp.transform.localScale = new Vector3 (float.Parse (items1 [9]), float.Parse (items1 [10]), float.Parse (items1 [11]));
//
//			wallRightDown.transform.position = new Vector3 (float.Parse (items1 [12]), float.Parse (items1 [13]), float.Parse (items1 [14]));
//			wallRightDown.transform.rotation = Quaternion.Euler (float.Parse (items1 [15]), float.Parse (items1 [16]), float.Parse (items1 [17]));
//			wallRightDown.transform.localScale = new Vector3 (float.Parse (items1 [18]), float.Parse (items1 [19]), float.Parse (items1 [20]));
//
//			wallRightUp.transform.parent = parent;
//			wallRightDown.transform.parent = parent;
//			Debug.Log (itemsDataString);
//		}
//		if (items1 [2].Contains ("Front")) {
//			GameObject wallRightUp = Instantiate (toRightUp);
//			wallRightUp.transform.position = new Vector3 (float.Parse (items1 [3]), float.Parse (items1 [4]), float.Parse (items1 [5]));
//			wallRightUp.transform.rotation = Quaternion.Euler (float.Parse (items1 [6]), float.Parse (items1 [7]), float.Parse (items1 [8]));
//			wallRightUp.transform.localScale = new Vector3 (float.Parse (items1 [9]), float.Parse (items1 [10]), float.Parse (items1 [11]));
//		}
//
//		//delete in DB
//		//WWWForm formDeleteStart = new WWWForm ();
//		//formDeleteStart.AddField ("idPost", items1[0]);
//		//WWW deleteStart = new WWW (DeleteMapStateURL, formDeleteStart);
//		//
//		Debug.Log("ID:" + items1[0]);
//		Debug.Log ("LAHDJSAHDKASHJD");
//		Debug.Log (items1 [2]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[RPC]
	public void beforesendSignalToClientToUpdateMap(string playerID){
		this.GetComponent<NetworkView> ().RPC ("readyToUpdate", RPCMode.Server, Network.player.ToString());
	}

	[RPC]
	public void readyToUpdate(string playerID){

	}

	[RPC]
	public void sendSignalToClientToUpdateMap(string playerID, string itemsDataString){
		if (Network.player.ToString () == playerID) {
			Debug.Log ("Here is Client: " + itemsDataString);
//			WWWForm form = new WWWForm ();
//			form.AddField ("clientIDPost", Network.player.ToString());
//			WWW itemsData = new WWW (PrintMapStateURL, form);
//			yield return itemsData;
//			string itemsDataString = itemsData.text;
//			print (itemsDataString);
//			Debug.Log(itemsDataString);
			list_items = itemsDataString.Split ('/');
			string[] items1 = list_items [0].Split (';');
			//string[] items2 = list_items [1].Split (';');
			if (items1 [2].Contains ("Right")){
				listWalls [0].SetActive (false);
				listBoundaries [0].SetActive (true);
			}
			if (items1 [2].Contains ("Left")) {
				listWalls [1].SetActive (false);
				listBoundaries [1].SetActive (true);
			}
			if (items1 [2].Contains ("Front")) {
				listWalls [2].SetActive (false);
				listBoundaries [2].SetActive (true);
			}
			//----------

			if (items1 [2].Contains ("Right") || items1 [2].Contains ("Left")) {
				GameObject wallRightDown = Instantiate (toRightDown);
				GameObject wallRightUp = Instantiate (toRightUp);

				if (items1 [2].Contains ("Right")) {
					wallRightUp.tag = "ToRight";
					wallRightDown.tag = "ToRight";
				}
				if (items1 [2].Contains ("Left")) {
					wallRightUp.tag = "ToLeft";
					wallRightDown.tag = "ToLeft";
				}

				wallRightUp.transform.position = new Vector3 (float.Parse (items1 [3]), float.Parse (items1 [4]), float.Parse (items1 [5]));
				wallRightUp.transform.rotation = Quaternion.Euler (float.Parse (items1 [6]), float.Parse (items1 [7]), float.Parse (items1 [8]));
				wallRightUp.transform.localScale = new Vector3 (float.Parse (items1 [9]), float.Parse (items1 [10]), float.Parse (items1 [11]));

				wallRightDown.transform.position = new Vector3 (float.Parse (items1 [12]), float.Parse (items1 [13]), float.Parse (items1 [14]));
				wallRightDown.transform.rotation = Quaternion.Euler (float.Parse (items1 [15]), float.Parse (items1 [16]), float.Parse (items1 [17]));
				wallRightDown.transform.localScale = new Vector3 (float.Parse (items1 [18]), float.Parse (items1 [19]), float.Parse (items1 [20]));

				wallRightUp.transform.parent = parent;
				wallRightDown.transform.parent = parent;
				Debug.Log (itemsDataString);
			}
			if (items1 [2].Contains ("Front")) {
				GameObject wallRightUp = Instantiate (toRightUp);
				wallRightUp.tag = "ToFront";
				wallRightUp.transform.position = new Vector3 (float.Parse (items1 [3]), float.Parse (items1 [4]), float.Parse (items1 [5]));
				wallRightUp.transform.rotation = Quaternion.Euler (float.Parse (items1 [6]), float.Parse (items1 [7]), float.Parse (items1 [8]));
				wallRightUp.transform.localScale = new Vector3 (float.Parse (items1 [9]), float.Parse (items1 [10]), float.Parse (items1 [11]));
			}
			this.GetComponent<NetworkView>().RPC("FinishUpdateMap", RPCMode.Server, playerID);
			Debug.Log ("AFTERRRR");
		}

	}

	public void OnApplicationQuit(){
		this.GetComponent<NetworkView>().RPC("sendStateBeforeQuit", RPCMode.Server, new object[]{GameObject.FindGameObjectWithTag("Username").name, "0", ""});
		this.GetComponent<NetworkView>().RPC("deleteMapBeforeQuit", RPCMode.Server, Network.player.ToString());
	}

	[RPC]
	public void sendStateBeforeQuit(string username, string isInUsed, string client){

	}

	[RPC]
	public void deleteMapBeforeQuit(string playerID){

	}

	//gửi tín hiệu khi bắt đầu game cho server yêu cầu update 
	[RPC]
	public void SendSignalToServerWhenBegin(string playerID){

	}

	//nhận tín hiệu sau khi gửi yêu cầu update map
	[RPC]
	public void SendBeginMapToClient(string playerID, string itemsDataString){
		if (Network.player.ToString () == playerID) {
			Debug.Log ("BEGINN12222");
			list_items = itemsDataString.Split ('/');
			string[] items1 = list_items [0].Split (';');
			//string[] items2 = list_items [1].Split (';');
			if (items1 [2].Contains ("Right")){
				listWalls [0].SetActive (false);
				listBoundaries [0].SetActive (true);
			}
			if (items1 [2].Contains ("Left")) {
				listWalls [1].SetActive (false);
				listBoundaries [1].SetActive (true);
			}
			if (items1 [2].Contains ("Front")) {
				listWalls [2].SetActive (false);
				listBoundaries [2].SetActive (true);
			}



			if (items1 [2].Contains ("Right") || items1 [2].Contains ("Left")) {
				GameObject wallRightDown = Instantiate (toRightDown);
				GameObject wallRightUp = Instantiate (toRightUp);

				if (items1 [2].Contains ("Right")) {
					wallRightUp.tag = "ToRight";
					wallRightDown.tag = "ToRight";
				}
				if (items1 [2].Contains ("Left")) {
					wallRightUp.tag = "ToLeft";
					wallRightDown.tag = "ToLeft";
				}

				wallRightUp.transform.position = new Vector3 (float.Parse (items1 [3]), float.Parse (items1 [4]), float.Parse (items1 [5]));
				wallRightUp.transform.rotation = Quaternion.Euler (float.Parse (items1 [6]), float.Parse (items1 [7]), float.Parse (items1 [8]));
				wallRightUp.transform.localScale = new Vector3 (float.Parse (items1 [9]), float.Parse (items1 [10]), float.Parse (items1 [11]));

				wallRightDown.transform.position = new Vector3 (float.Parse (items1 [12]), float.Parse (items1 [13]), float.Parse (items1 [14]));
				wallRightDown.transform.rotation = Quaternion.Euler (float.Parse (items1 [15]), float.Parse (items1 [16]), float.Parse (items1 [17]));
				wallRightDown.transform.localScale = new Vector3 (float.Parse (items1 [18]), float.Parse (items1 [19]), float.Parse (items1 [20]));

				wallRightUp.transform.parent = parent;
				wallRightDown.transform.parent = parent;
				Debug.Log (itemsDataString);
				this.GetComponent<NetworkView>().RPC("FinishUpdateMap", RPCMode.Server, playerID);
			}
			if (items1 [2].Contains ("Front")) {
				GameObject wallRightUp = Instantiate (toRightUp);

				wallRightUp.tag = "ToFront";

				wallRightUp.transform.position = new Vector3 (float.Parse (items1 [3]), float.Parse (items1 [4]), float.Parse (items1 [5]));
				wallRightUp.transform.rotation = Quaternion.Euler (float.Parse (items1 [6]), float.Parse (items1 [7]), float.Parse (items1 [8]));
				wallRightUp.transform.localScale = new Vector3 (float.Parse (items1 [9]), float.Parse (items1 [10]), float.Parse (items1 [11]));
				this.GetComponent<NetworkView>().RPC("FinishUpdateMap", RPCMode.Server, playerID);
			}

		}
	}

	[RPC]
	public void FinishUpdateMap(string playerID){

	}

	[RPC]
	public void TellClientToReCreateWall(string playerID, string wallName){
		if (Network.player.ToString () == playerID) {
			if(wallName.Contains("Right")){
				listWalls [0].SetActive (true);
				listBoundaries [0].SetActive (false);

				GameObject[] list_go = GameObject.FindGameObjectsWithTag ("ToRight");
				foreach (GameObject go in list_go) {
					Destroy (go);
				}
			}
			if(wallName.Contains("Left")){
				listWalls [1].SetActive (true);
				listBoundaries [1].SetActive (false);

				GameObject[] list_go = GameObject.FindGameObjectsWithTag ("ToLeft");
				foreach (GameObject go in list_go) {
					Destroy (go);
				}
			}
			if(wallName.Contains("Front")){
				listWalls [2].SetActive (true);
				listBoundaries [2].SetActive (false);

				GameObject[] list_go = GameObject.FindGameObjectsWithTag ("ToFront");
				foreach (GameObject go in list_go) {
					Destroy (go);
				}
			}
		}
	}

	[RPC]
	public void TellOtherClientsToDeleteMinimap(string playerID){
		if (Network.player.ToString () != playerID) {
			Destroy (GameObject.Find ("Minimap" + playerID));
		}
	}
}
