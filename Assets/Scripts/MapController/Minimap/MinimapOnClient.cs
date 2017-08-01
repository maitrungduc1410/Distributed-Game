using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapOnClient : MonoBehaviour {
	public GameObject quad;
	private static bool isDeleted = false;
	// Use this for initialization
	void Start () {
		this.GetComponent<NetworkView> ().RPC ("sendSignalToServerWhenBegin", RPCMode.Server, null);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[RPC]
	public void sendSignalToServerWhenBegin(){

	}

	[RPC]
	public void sendMinimapToOtherClients(Vector3 scale, Vector3 position, Quaternion rotation){
		//dec
		if (isDeleted == false) {
			GameObject[] list_minimap = GameObject.FindGameObjectsWithTag ("ClientMap");
			foreach (GameObject go in list_minimap) {
				Destroy (go);
			}
			isDeleted = true;
		}
		quad.transform.localScale = scale;
		quad.transform.position = position;
		quad.transform.rotation = rotation;
		Instantiate (quad);
	}
		
}
