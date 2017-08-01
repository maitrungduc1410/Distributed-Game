using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllMinimapClient : MonoBehaviour {
	public GameObject minimap;
	public GameObject minimapChild;
	public GameObject listOtherAj;
	public GameObject Aj;
	// Use this for initialization
	void Start () {
		this.GetComponent<NetworkView> ().RPC ("sendSignalToServerBeforeCreateMinimap", RPCMode.Server, Network.player.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[RPC]
	public void sendSignalToServerBeforeCreateMinimap(string playerID){
		//GameObject minimap_Child = Instantiate (minimapChild);
		//minimap_Child.name = "Minimap" + playerID;

	}

	[RPC]
	public void createMinimapOnClient(string playerID, string otherPlayerID){
		if (Network.player.ToString () == playerID) {
			GameObject go = Instantiate (Aj);
			go.name = "Aj" + otherPlayerID;
			go.transform.parent = listOtherAj.transform;

			GameObject minimap_Child = Instantiate (minimapChild);
			minimap_Child.transform.parent = minimap.transform;
			minimap_Child.name = "Minimap" + otherPlayerID;

			//BlipClient k = (BlipClient) minimap_Child.GetComponent(typeof(BlipClient));
			//k.setTartget (go);
		}
	}

	[RPC]
	public void sendAllAjMovementToClients(string playerID, Vector3 position, Quaternion rotation){
		//if (Network.player.ToString () != playerID) {
			for (int i = 0; i < listOtherAj.transform.childCount; i++) {
				GameObject go = listOtherAj.transform.GetChild (i).gameObject;
				string playerIDToUpdate = go.name;
				playerIDToUpdate = playerIDToUpdate.Substring (playerIDToUpdate.Length - 1);
				if (playerIDToUpdate == playerID) {
					go.transform.position = position;
					go.transform.rotation = rotation;
				}
			}
		//}
	}

	[RPC]
	public void addMinimapToOtherClients(string playerID){
		if (Network.player.ToString () != playerID) {
			GameObject go = Instantiate (Aj);
			go.name = "Aj" + playerID;
			go.transform.parent = listOtherAj.transform;

			GameObject minimap_Child = Instantiate (minimapChild);
			minimap_Child.transform.parent = minimap.transform;
			minimap_Child.name = "Minimap" + playerID;
		}
	}
}
