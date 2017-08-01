using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageItemOnServer : MonoBehaviour {
	public Dropdown listClients;
	private string playerID;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void printName(int index) {
		Debug.Log(listClients.options[listClients.value].text);
		string[] items = listClients.options [listClients.value].text.Split (' ');
		if (listClients.value > 0) {
			this.playerID = items [0].Substring (items [0].Length - 1);
			Debug.Log ("print: " + this.playerID + "---" + listClients.value);
			GameObject.Find ("Boundary").GetComponent<NetworkView> ().RPC ("getItemsInformationFromClient", RPCMode.Others, this.playerID);
		}

	}

	public void deleteItem(string itemName){
		
		GameObject.Find ("Boundary").GetComponent<NetworkView> ().RPC ("deleteItemOnClient", RPCMode.Others, new object[]{this.playerID, itemName} );

	}
}
