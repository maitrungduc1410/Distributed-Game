using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CurrentMapState : MonoBehaviour {
	// Use this for initialization
	string InsertMapURL = "http://localhost/SkippingStones/InsertMapState.php";
	string PrintMapStateURL = "http://localhost/SkippingStones/PrintMapState.php";
	string UpdateMapStateURL = "http://localhost/SkippingStones/UpdateMapState.php";
	string DeleteMapStateURL = "http://localhost/SkippingStones/DeleteMapState.php";
	string UpdateMapWhenAClientQuitURL = "http://localhost/SkippingStones/UpdateMapWhenAClientQuit.php";

	public GameObject grid_ListClientManager;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InsertMapState(object[] obj){
		string clientID, wallName, transform1, transform2, otherClientID;
		clientID = obj [0].ToString();
		wallName = obj [1].ToString();
		transform1 = obj [2].ToString();
		transform2 = obj [3].ToString();
		otherClientID = obj [4].ToString();
		if (clientID != otherClientID) {
			WWWForm form = new WWWForm ();
			form.AddField ("clientIDPost", clientID);
			form.AddField ("wallNamePost", wallName);
			form.AddField ("transform1Post", transform1);
			form.AddField ("transform2Post", transform2);
			form.AddField ("otherClientID", otherClientID);
			WWW www = new WWW (InsertMapURL, form);
		}

		//Debug.Log ("INSERT: " + otherClientID + "--" + clientID);
		//----
//		WWWForm formPrint = new WWWForm ();
//		formPrint.AddField ("clientIDPost", clientID);
//		WWW itemsDataPrint = new WWW (PrintMapStateURL, formPrint);
//		yield return itemsDataPrint;
//		string itemsDataString = itemsDataPrint.text;

//		//----
		//this.GetComponent<NetworkView> ().RPC ("sendSignalToClientToUpdateMap", RPCMode.Others, new object[]{clientID, itemsDataString});
		this.GetComponent<NetworkView> ().RPC ("beforesendSignalToClientToUpdateMap", RPCMode.Others, new object[]{clientID});

		//Debug.Log (itemsDataString + "///" + clientID);
		Debug.Log("INSERT MAP STATE");
	}

	[RPC]
	public void sendSignalToClientToUpdateMap(string playerID, string itemsDataString){

	}

	[RPC]
	public void beforesendSignalToClientToUpdateMap(string playerID){

	}

	[RPC]
	public IEnumerator readyToUpdate(string playerID){
		WWWForm formPrint = new WWWForm ();
		formPrint.AddField ("clientIDPost", playerID);
		WWW itemsDataPrint = new WWW (PrintMapStateURL, formPrint);
		yield return itemsDataPrint;
		string itemsDataString = itemsDataPrint.text;
		this.GetComponent<NetworkView> ().RPC ("sendSignalToClientToUpdateMap", RPCMode.Others, new object[]{playerID, itemsDataString});

	}

	[RPC]
	public void FinishUpdateMap(string playerID){
		WWWForm formPrint = new WWWForm ();
		formPrint.AddField ("clientIDPost", playerID);
		WWW itemsDataPrint = new WWW (UpdateMapStateURL, formPrint);
	}

	[RPC]
	public IEnumerator deleteMapBeforeQuit(string otherPlayerID){
		WWWForm formQuit = new WWWForm();
		formQuit.AddField ("otherClientIDPost", otherPlayerID);
		WWW itemsDataQuit = new WWW (UpdateMapWhenAClientQuitURL, formQuit);
		yield return itemsDataQuit;
		string itemsDataString = itemsDataQuit.text;
		//try catch trong trường hợp chỉ có duy nhất 1 map mà disconnect
		try{
			itemsDataString = itemsDataString.Substring (0, itemsDataString.Length - 1);
		string[] list_items;
		list_items = itemsDataString.Split ('/');
		//Debug.Log (itemsDataString);
		//Debug.Log (otherPlayerID);
		//Debug.Log (list_items [0]);
		foreach (string itemString in list_items) {
			string[] items = itemString.Split (';');
			this.GetComponent<NetworkView> ().RPC ("TellClientToReCreateWall", RPCMode.Others, new object[]{items[1], items[2]});

			//--//
			GameObject[] list_createdWalls = GameObject.FindGameObjectsWithTag("To" + items[2]);
			foreach (GameObject go in list_createdWalls) {
				if (go.transform.IsChildOf (GameObject.Find ("ClientMap" + items [1]).transform)) {
					Destroy (go);
				}
			}

			if (items [2] == "Right") {
				if (GameObject.Find ("ClientMap" + items [1]) != null) {
					GameObject go = GameObject.Find ("ClientMap" + items [1]).transform.GetChild (1).GetChild (4).gameObject;
					go.SetActive (true);

					Contain l = (Contain) go.GetComponent(typeof(Contain));
					l.isRightDownCreated = false;
					l.isRightUpCreated = false;
					//l.listPoints.Clear ();
					//Debug.Log ("GOOO: " + go.name);
				}
			}
			if (items [2] == "Left") {
				if (GameObject.Find ("ClientMap" + items [1]) != null) {
					GameObject go = GameObject.Find ("ClientMap" + items [1]).transform.GetChild (1).GetChild (2).gameObject;
					go.SetActive (true);

					Contain l = (Contain) go.GetComponent(typeof(Contain));
					l.isLeftUpCreated = false;
					l.isLeftDownCreated = false;
					//l.listPoints.Clear ();
					//Debug.Log ("GOOO: " + go.name);
				}
			}
			if (items [2] == "Front") {
				if (GameObject.Find ("ClientMap" + items [1]) != null) {
					GameObject go = GameObject.Find ("ClientMap" + items [1]).transform.GetChild (1).GetChild (5).gameObject;
					go.SetActive (true);

					Contain l = (Contain) go.GetComponent(typeof(Contain));
					l.isLeftUpCreated = true;
					l.isRightUpCreated = true;
					//l.listPoints.Clear ();
					//Debug.Log ("GOOO: " + go.name);
				}
			}
		}
		
		}catch(Exception e){
			Debug.Log (e, this);
		}

		GameObject clientMap = GameObject.Find ("ClientMap" + otherPlayerID);
		Destroy (clientMap);

		GameObject clientMinimap = GameObject.Find ("Minimap" + otherPlayerID);
		Destroy (clientMinimap);
		this.GetComponent<NetworkView> ().RPC ("TellOtherClientsToDeleteMinimap", RPCMode.Others, otherPlayerID);


		GameObject chooseMap = GameObject.Find ("ChooseMap" + otherPlayerID);
		Destroy (chooseMap);

		for (int i = 0; i < grid_ListClientManager.transform.childCount; i++) {
			if (grid_ListClientManager.transform.GetChild (i).gameObject.name.Contains (otherPlayerID)) {
				Destroy (grid_ListClientManager.transform.GetChild (i).gameObject);
			}
		}
		//-----//
		WWWForm formPrint = new WWWForm ();
		formPrint.AddField ("otherClientIDPost", otherPlayerID);
		WWW itemsDataPrint = new WWW (DeleteMapStateURL, formPrint);

	}

	[RPC]
	public void TellClientToReCreateWall(string playerID, string wallName){

	}

	[RPC]
	public void TellOtherClientsToDeleteMinimap(string playerID){

	}
}
