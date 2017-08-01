using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownList : MonoBehaviour {
	string selectAllUsers = "http://localhost/SkippingStones/GetUsersPlaying.php";
	public Dropdown userDropDown;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateDropDownList(){
		StartCoroutine (getListUserPlaying ());
		Debug.Log ("KASJHDKJASHD");
	}

	public IEnumerator getListUserPlaying(){
		List<string> listClient = new List<string> ();
		userDropDown.ClearOptions ();
		listClient.Add ("Choose Client...");
		WWW userData = new WWW (selectAllUsers);
		yield return userData;
		if (userData.text.Length > 0) {
			string userDataString = userData.text;
			userDataString = userDataString.Substring (0, userDataString.Length - 1);
			string[] listUser = userDataString.Split ('/');
			foreach (string userInfo in listUser) {
				string[] userItems = userInfo.Split (';');
				listClient.Add (userItems [2] + " - " + userItems[1]);
			}
		}
		userDropDown.AddOptions (listClient);

	}

}
