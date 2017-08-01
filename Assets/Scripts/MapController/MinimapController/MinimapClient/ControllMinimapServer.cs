using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllMinimapServer : MonoBehaviour {
	public Transform minimap;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] list_Aj = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject go in list_Aj) {
			string playerID = go.transform.parent.name;
			playerID = playerID.Substring (playerID.Length - 1);
			this.GetComponent<NetworkView> ().RPC ("sendAllAjMovementToClients", RPCMode.Others, new object[]{playerID, go.transform.position, go.transform.rotation});
		}

	}

	[RPC]
	public void sendSignalToServerBeforeCreateMinimap(string playerID){
		for (int i = 0; i < minimap.childCount; i++) {
			string otherPlayerID = minimap.GetChild (i).gameObject.name;
			//Debug.Log (otherPlayerID);
			otherPlayerID = otherPlayerID.Substring (otherPlayerID.Length - 1);
			if (playerID != otherPlayerID) {
				this.GetComponent<NetworkView> ().RPC ("createMinimapOnClient", RPCMode.Others, new object[]{playerID, otherPlayerID});
				//Debug.Log ("YESS");
			}
			//Debug.Log ("HAHAHAHAHHAHAHAHAH: " + playerID + "/// other: " + otherPlayerID);
		}
		this.GetComponent<NetworkView> ().RPC ("addMinimapToOtherClients", RPCMode.Others, new object[]{playerID});
	}

	[RPC]
	public void createMinimapOnClient(string playerID, string otherPlayerID){

	}

	[RPC]
	public void sendAllAjMovementToClients(string playerID, Vector3 position, Quaternion rotation){

	}

	[RPC]
	public void addMinimapToOtherClients(string playerID){

	}
}
