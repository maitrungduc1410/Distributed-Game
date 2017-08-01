using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginClientController : MonoBehaviour {
	public InputField username;
	public InputField password;
	public GameObject go_Username;
	public Image notifyText;
	public Image accIsInUsed;
	public AudioClip sound;
	AudioSource source;
	AudioClip startSceneSound;
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		//source.On (startSceneSound);
	}
	// Update is called once per frame
	void Update () {
		
	}

	public void Login(){
		StartCoroutine (ILogin ());
//		string username = this.username.text;
//		string password = this.password.text;
//		GameObject.Find("Client").GetComponent<NetworkView> ().RPC ("checkLogin", RPCMode.Server, new object[]{username, password});
	}

	IEnumerator ILogin(){
		source.PlayOneShot (sound);
		yield return new WaitForSeconds (sound.length);
		string username = this.username.text;
		string password = this.password.text;
		GameObject.Find("Client").GetComponent<NetworkView> ().RPC ("checkLogin", RPCMode.Server, new object[]{username, password});
	}

	[RPC]
	public void checkLogin(string username, string password){

	}

	[RPC]
	public void checkDoneLogin(string username, bool b, string isInUsed){
		if (this.username.text == username && b == true) {
			if (isInUsed.Equals ("0")) {
				SceneManager.LoadScene ("ChooseMapClient", LoadSceneMode.Single);
				go_Username.name = username;
				DontDestroyOnLoad (go_Username);
				GameObject.Find("Client").GetComponent<NetworkView> ().RPC ("sendStateToServer", RPCMode.Server, new object[]{username, "1"});
			} else if(isInUsed.Equals("1")) {
				notifyText.gameObject.SetActive (false);
				if (accIsInUsed.gameObject.activeInHierarchy) {
					accIsInUsed.gameObject.SetActive (false);
					accIsInUsed.gameObject.SetActive (true);
				} else {
					accIsInUsed.gameObject.SetActive (true);
				}
			}

		} else if(this.username.text == username && b == false) {
			Debug.Log ("SAIIIII");
			accIsInUsed.gameObject.SetActive (false);
			if (notifyText.gameObject.activeInHierarchy) {
				notifyText.gameObject.SetActive (false);
				notifyText.gameObject.SetActive (true);
			} else {
				notifyText.gameObject.SetActive (true);
			}

		}
	}

	[RPC]
	public void sendStateToServer(string username, string isInUsed){

	}
}
