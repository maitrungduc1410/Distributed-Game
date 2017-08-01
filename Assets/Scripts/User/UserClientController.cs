using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserClientController : MonoBehaviour {
	public Canvas canvas_Register;
	public InputField username;
	public InputField password;
	public InputField confirmPass;
	public Image notifyPass;
	public Image accIsInExist;
	public Image success;
	// Use this for initialization

	bool canCreateAcc = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void openCanvasRegister(){
		canvas_Register.gameObject.SetActive (true);
	}

	public void closeCanvasRegister(){
		canvas_Register.gameObject.SetActive (false);
		accIsInExist.gameObject.SetActive (false);
		notifyPass.gameObject.SetActive (false);
		success.gameObject.SetActive (false);
	}

	public void register(){
		this.GetComponent<NetworkView>().RPC("sendUsernameToServerToCheck", RPCMode.Server, new object[]{Network.player.ToString(), username.text});
		if (canCreateAcc == true) {
			if (checkPass (password.text, confirmPass.text) == false) {
				accIsInExist.gameObject.SetActive (false);
				notifyPass.gameObject.SetActive (false);
				if (notifyPass.gameObject.activeInHierarchy) {
					notifyPass.gameObject.SetActive (false);
					notifyPass.gameObject.SetActive (true);
				} else {
					notifyPass.gameObject.SetActive (true);
				}
			} else {
				notifyPass.gameObject.SetActive (false);
				accIsInExist.gameObject.SetActive (false);

				//neu tat ca moi thu deu dung
				this.GetComponent<NetworkView>().RPC("createAcc", RPCMode.Server, new object[]{Network.player.ToString(), username.text, password.text});
			}
		} else {
			notifyPass.gameObject.SetActive (false);
			accIsInExist.gameObject.SetActive (false);
			if (accIsInExist.gameObject.activeInHierarchy) {
				accIsInExist.gameObject.SetActive (false);
				accIsInExist.gameObject.SetActive (true);
			} else {
				accIsInExist.gameObject.SetActive (true);
			}
		}
		Debug.Log (canCreateAcc);
		Debug.Log (checkPass (password.text, confirmPass.text));
	}

	public bool checkPass(string pass, string confirmPass){
		if (pass == confirmPass) {
			return true;
		} else
			return false;
	}

	[RPC]
	public void sendUsernameToServerToCheck(string playerID, string username){

	}

	[RPC]
	public void sendResultToClient(string playerID, bool check){
		if (Network.player.ToString () == playerID) {
			if (check == true) {
				canCreateAcc = true;
			} else if (check == false) {
				canCreateAcc = false;
			}
		}
	}

	[RPC]
	public void createAcc(string playerID, string username, string password){

	}

	[RPC]
	public void sendSuccessSignal(string playerID){
		if (Network.player.ToString () == playerID) {
			success.gameObject.SetActive (true);
		}
	}
}
