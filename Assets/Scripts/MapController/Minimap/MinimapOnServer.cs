using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapOnServer : MonoBehaviour {
	private static List<object[]> list = new List<object[]> ();
	//string insertMinimapStateURL = "http://localhost/SkippingStones/LoadMinimapState.php";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[RPC]
	public void sendSignalToServerWhenBegin(){
		GameObject[] list_minimap = GameObject.FindGameObjectsWithTag ("ClientMap");
		foreach (GameObject go in list_minimap) {
			this.GetComponent<NetworkView> ().RPC ("sendMinimapToOtherClients", RPCMode.Others, new object[]{go.transform.localScale, go.transform.position, go.transform.rotation});
		}
	}

	[RPC]
	public void sendMinimapToOtherClients(Vector3 scale, Vector3 position, Quaternion rotation){

	}
}
