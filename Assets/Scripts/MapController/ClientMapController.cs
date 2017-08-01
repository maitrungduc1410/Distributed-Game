using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMapController : MonoBehaviour {
	public GameObject map;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnConnectedToServer(){
		this.GetComponent<NetworkView>().RPC("getClientScreenResolution", RPCMode.Server, new object[]{Screen.width.ToString(), Screen.height.ToString()});
		//this.GetComponent<NetworkView>().RPC("printText", RPCMode.Server, null);
		//Debug.Log (Screen.height + "---" + Screen.width);
	}

	#region RPC functions
	[RPC]
	public void getClientScreenResolution(string width, string height){

	}

	[RPC]
	public void createRealMapOnClient(string playerID, Transform transform){
		if (playerID != Network.player.ToString ()) {
			map.transform.position = transform.position;
			map.transform.rotation = transform.rotation;
			map.transform.localScale = transform.localScale;
		}
	}

	#endregion
}
