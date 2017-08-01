using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginServerController : MonoBehaviour {
	string getUserByUsernameURL = "http://localhost/SkippingStones/SelectUserWithUsername.php";
	string updateAccountStateURL = "http://localhost/SkippingStones/UpdateAccountState.php";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//lam tiepppppp
	[RPC]
	public IEnumerator checkLogin(string username, string password){
		WWWForm form = new WWWForm ();
		form.AddField ("usernamePost", username);
		form.AddField ("passwordPost", password);
		WWW www = new WWW (getUserByUsernameURL, form);
		yield return www;
		string itemsDataString = www.text;
		string[] items = itemsDataString.Split (';');
		if (itemsDataString != "") {
			this.GetComponent<NetworkView> ().RPC ("checkDoneLogin", RPCMode.Others, new object[]{ username, true, items[5] });
		} else {
			this.GetComponent<NetworkView> ().RPC ("checkDoneLogin", RPCMode.Others, new object[]{ username, false, "" });

		}
	}

	[RPC]
	public void checkDoneLogin(string username, bool b, string isInUsed){

	}

	[RPC]
	public void sendStateToServer(string username, string isInUsed){
		WWWForm form = new WWWForm ();
		form.AddField ("usernamePost", username);
		form.AddField ("statePost", isInUsed);
		WWW www = new WWW (updateAccountStateURL, form);
	}

	[RPC]
	public void sendStateBeforeQuit(string username, string isInUsed, string client){
		WWWForm form = new WWWForm ();
		form.AddField ("usernamePost", username);
		form.AddField ("statePost", isInUsed);
		form.AddField ("clientPost", client);
		WWW www = new WWW (updateAccountStateURL, form);
		//Debug.Log (username);
	}
}
