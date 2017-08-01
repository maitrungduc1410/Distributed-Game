using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginMapServerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[RPC]
	public IEnumerator SendSignalToServerWhenBegin(string playerID){
		string PrintMapStateURL = "http://localhost/SkippingStones/PrintMapState.php";
		WWWForm form = new WWWForm ();
		form.AddField ("clientIDPost", playerID);
		WWW itemsData = new WWW (PrintMapStateURL, form);
		yield return itemsData;
		string itemsDataString = itemsData.text;
		this.GetComponent<NetworkView> ().RPC ("SendBeginMapToClient", RPCMode.Others, new object[]{playerID, itemsDataString});
		//Debug.Log (itemsDataString + "////" + playerID);
	}

	[RPC]
	public void SendBeginMapToClient(string playerID, string items){

	}
}
