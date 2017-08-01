using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserServerController : MonoBehaviour {
	string selectAllUsers = "http://localhost/SkippingStones/SelectAllUsers.php";
	string createAccountURL = "http://localhost/SkippingStones/CreateAccount.php";
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[RPC]
	public void deleteUserWithUsername(string username){

	}

	[RPC]
	public IEnumerator sendUsernameToServerToCheck(string playerID, string username){
		bool check = true;

		WWW userData = new WWW (selectAllUsers);
		yield return userData;
		string userDataString = userData.text;
		userDataString = userDataString.Substring(0, userDataString.Length - 1);
		string[] listUser = userDataString.Split ('/');
		foreach (string userInfo in listUser) {
			string[] userItems = userInfo.Split (';');
			if (username == userItems [1]) {
				check = false;
			}
		}
		this.GetComponent<NetworkView> ().RPC ("sendResultToClient", RPCMode.Others, new object[]{playerID, check});


	}

	[RPC]
	public void sendResultToClient(string playerID, bool check){

	}

	[RPC]
	public void createAcc(string playerID, string username, string password){
		WWWForm form = new WWWForm ();
		form.AddField ("usernamePost", username);
		form.AddField ("passwordPost", password);
		WWW www = new WWW (createAccountURL, form);
		this.GetComponent<NetworkView> ().RPC ("sendSuccessSignal", RPCMode.Others, playerID);
	}

	[RPC]
	public void sendSuccessSignal(string playerID){

	}
}
