using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class ButtonController : MonoBehaviour{
	// Use this for initialization
	public Canvas myCanvas;
	public Canvas canvas_NetworkManager;
	public Canvas canvas_AddMenu;
	public Canvas canvas_Items;
	public Canvas canvas_Movement;
	public Canvas canvas_ClientManager;
	public Canvas canvas_Info;
	public Canvas canvas_Map;
	public Canvas canvas_UserManager;
	public Canvas canvas_ConfirmDelete;
	public Canvas canvas_CheckAccIsInUsed;
	public Canvas canvas_Camera;
	public Canvas canvas_Minimap;
	public Canvas canvas_HighScore;
	public InputField serverAddress;
	public Text MyIpAddress;
	public Text Platform;
	public Text clientNum;
	public GameObject clientRow;
	public Slider slider;
	public Text angle;
	public GameObject stone;
	public Text userState;
	public Text username;
	public Text mainText_AccIsInUsed;
	public GameObject User;
	public Text text_Notify_Additem;
	public AudioClip sound;
	AudioSource source;
	//public Image curHP;
	//public Text ratioText;

	//private attributes
	private static bool isPaused = false;

	//
	void Start () {
		source = GetComponent<AudioSource> ();
		//Debug.Log (SystemInfo.deviceType);
		if (SystemInfo.deviceType.ToString().Equals("Handheld")) {
			canvas_Movement.gameObject.SetActive (true);
		}

	}

	void Update(){
		if (isPaused == true) {
			Time.timeScale = 0;
		} else if(isPaused == false) {
			Time.timeScale = 1;
		}
	}

	public void LoadScene(string sceneName){
		SceneManager.LoadScene (sceneName, LoadSceneMode.Single);
		isPaused = false;
		//Application.Quit();
	}

	public void Quit(){
		StartCoroutine (IQuit ());
	}

	IEnumerator IQuit(){
		source.PlayOneShot (sound);
		yield return new WaitForSeconds (sound.length);
		Application.Quit ();
	}

	public void Throw(){
		GameObject go = GameObject.Find("Aj");
		go.SendMessage ("isCanThrow");
	}

	public void Pause(){
		isPaused = true;
		myCanvas.gameObject.SetActive (true);
		canvas_AddMenu.gameObject.SetActive (false);

		//Button btn = GameObject.Find ("Button_AddStone").GetComponent<Button> ();
		//btn.gameObject.SetActive (false);

	}

	public void Resume(){
		isPaused = false;
		myCanvas.gameObject.SetActive (false);
		canvas_AddMenu.gameObject.SetActive (true);
		//Button btn = GameObject.Find ("Button_AddStone", false).GetComponent<Button> ();
		//btn.gameObject.SetActive (true);
	}


	public void openNetworkCenter(){
		canvas_NetworkManager.gameObject.SetActive (true);
		canvas_AddMenu.gameObject.SetActive (false);
		canvas_Camera.gameObject.SetActive (false);
		MyIpAddress.text = "Your IP address is: " + Network.player.ipAddress;
		Platform.text = "Your device type/OS: " + SystemInfo.deviceType.ToString () + "/" + Application.platform.ToString();
	}

	public void closeNetworkCenter(){
		canvas_NetworkManager.gameObject.SetActive (false);
		canvas_AddMenu.gameObject.SetActive (true);
		canvas_Camera.gameObject.SetActive (true);
	}

	public void openClientManager(){
		canvas_ClientManager.gameObject.SetActive (true);
		canvas_Info.gameObject.SetActive (false);
		canvas_Map.gameObject.SetActive (false);
		canvas_UserManager.gameObject.SetActive (false);
		canvas_HighScore.gameObject.SetActive (false);
	}

	public void openInfo(){
		canvas_Info.gameObject.SetActive (true);
		canvas_ClientManager.gameObject.SetActive (false);
		canvas_Map.gameObject.SetActive (false);
		canvas_UserManager.gameObject.SetActive (false);
		canvas_HighScore.gameObject.SetActive (false);
	}

	public void openMap(){
		canvas_ClientManager.gameObject.SetActive (false);
		canvas_Info.gameObject.SetActive (false);
		canvas_Map.gameObject.SetActive (true);
		canvas_UserManager.gameObject.SetActive (false);
		canvas_HighScore.gameObject.SetActive (false);
	}
	public void openUserManager(){
		canvas_ClientManager.gameObject.SetActive (false);
		canvas_Info.gameObject.SetActive (false);
		canvas_Map.gameObject.SetActive (false);
		canvas_UserManager.gameObject.SetActive (true);
		canvas_HighScore.gameObject.SetActive (false);
	}

	public void openHighScore(){
		canvas_ClientManager.gameObject.SetActive (false);
		canvas_Info.gameObject.SetActive (false);
		canvas_Map.gameObject.SetActive (false);
		canvas_UserManager.gameObject.SetActive (false);
		canvas_HighScore.gameObject.SetActive (true);
	}

	public void ConnectToServer(){
		string ip = serverAddress.text.ToString ();
		GameObject go = GameObject.Find("Boundary");
		AppRoot_Scene2 l = (AppRoot_Scene2) go.GetComponent(typeof(AppRoot_Scene2));
		l.InitNet (ip);
	}

	public void DisConnect(){
		GameObject.Find ("Boundary").GetComponent<NetworkView> ().RPC ("DisconnectAClient", RPCMode.Others, clientNum.text);
		Destroy (clientRow);
		Debug.Log (clientNum.text);
		//Debug.Log(num);
	}



	public void addItem(){
		canvas_Items.gameObject.SetActive (true);
		canvas_Minimap.gameObject.SetActive (false);
	}

	public void closeAddItemCanvas(){
		canvas_Items.gameObject.SetActive (false);
		text_Notify_Additem.gameObject.SetActive (false);
		canvas_Minimap.gameObject.SetActive (true);

	}

	public void addStone(){
		stone.SetActive (true);
		stone = GameObject.Find("Capsule");
		Launcher l = (Launcher) stone.GetComponent(typeof(Launcher));
		l.createCapsule ();
		canvas_Items.gameObject.SetActive (false);
		canvas_Minimap.gameObject.SetActive (true);

		//ratioText.text = "0%";
		//curHP.rectTransform.localScale = Vector3.zero;


	}

	public void addSpecificItem(string itemName){
		GameObject go = GameObject.Find(itemName);
		DropABox l = (DropABox) go.GetComponent(typeof(DropABox));
		l.DropBox ();
		canvas_Items.gameObject.SetActive (false);
		//canvas_Minimap.gameObject.SetActive (true);
	}

	public void addDuckItem(string itemName){
		GameObject go = GameObject.Find(itemName);
		AddDuckOnClient l = (AddDuckOnClient) go.GetComponent(typeof(AddDuckOnClient));
		l.DropDuck ();
		canvas_Items.gameObject.SetActive (false);
		//canvas_Minimap.gameObject.SetActive (true);
	}

	public void setAngle(){
		char upArrow = '\u00B0';
		//Mathf.R
		angle.text = Mathf.Round(slider.value).ToString () + upArrow.ToString();
	}

	public void openDeleteUserBox(){
		string[] itemsUsername = username.text.Split (' ');
		StartCoroutine (getUserStateByUsername (itemsUsername[1]));
		this.transform.parent.transform.parent.transform.parent.transform.parent.gameObject.transform.GetChild(3).gameObject.SetActive(true);
		string[] items = this.transform.parent.GetChild (4).gameObject.GetComponent<Text> ().text.Split(' ');
		this.transform.parent.transform.parent.transform.parent.transform.parent.gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).GetChild(3).gameObject.GetComponent<Text>().text = items[1];
	}

	public void closeDeleteUserBox(){
		canvas_ConfirmDelete.gameObject.SetActive (false);
	}

	public void deleteUser(){
		

		string s = userState.text;
		Debug.Log (userState.text);
		if (s.Equals ("0")) {
			StartCoroutine (deleteUserByUsername (username.text));
			canvas_ConfirmDelete.gameObject.SetActive (false);
			Destroy(GameObject.Find (username.text));
		} else if (s.Equals ("1")) {
			canvas_CheckAccIsInUsed.gameObject.GetComponent<Animator> ().SetBool ("dispose", false);
			canvas_CheckAccIsInUsed.gameObject.GetComponent<Animator> ().SetBool ("TurnToBegin", true);
			canvas_CheckAccIsInUsed.gameObject.SetActive (true);
			mainText_AccIsInUsed.text = "Account " + username.text + " is playing";
		}
		Debug.Log ("s:" + s);
	}

	public void deleteUserPlaying(){
		
		canvas_CheckAccIsInUsed.gameObject.GetComponent<Animator> ().SetBool ("dispose", true);
		canvas_CheckAccIsInUsed.gameObject.GetComponent<Animator> ().SetBool ("TurnToBegin", false);
		canvas_ConfirmDelete.gameObject.SetActive (false);
		Destroy(GameObject.Find (username.text));
		//
		StartCoroutine(deleteUserPlayingAndDisconnect(username.text));
		StartCoroutine (deleteUserByUsername (username.text));
		//Lam tiep
	}

	public IEnumerator deleteUserPlayingAndDisconnect(string username){
		string getUserStateByUsername = "http://localhost/SkippingStones/GetUserStateByUsername.php";
		WWWForm form = new WWWForm ();
		form.AddField ("usernamePost", username);
		WWW www = new WWW (getUserStateByUsername, form);
		yield return www;
		string itemsDataString = www.text;
		string[] items = itemsDataString.Split (';');
		string playerID = items [3].Substring (items [3].Length - 1);
		GameObject.Find ("Boundary").GetComponent<NetworkView> ().RPC ("DisconnectAClient", RPCMode.Others, playerID);
	}

	public IEnumerator getUserStateByUsername(string username){
		string getUserStateByUsername = "http://localhost/SkippingStones/GetUserStateByUsername.php";
		WWWForm form = new WWWForm ();
		form.AddField ("usernamePost", username);
		WWW www = new WWW (getUserStateByUsername, form);
		yield return www;
		string itemsDataString = www.text;
		string[] items = itemsDataString.Split (';');
		this.transform.parent.transform.parent.transform.parent.transform.parent.GetChild (3).GetChild(0).GetChild(0).gameObject.GetComponent<Text> ().text = items [2];
		//userState.text = items [2];
		//yield return new WaitForSeconds (1);
		//yield return items [2];
	}

	public IEnumerator deleteUserByUsername(string name){
		string delUserByUsername = "http://localhost/SkippingStones/DeleteUserByUsername.php";
		WWWForm formDelete = new WWWForm ();
		formDelete.AddField ("usernamePost", name);
		WWW wwwDel = new WWW (delUserByUsername, formDelete);
		yield return wwwDel;
	}

	public void closeNoticeAccIsInUsed(){
		canvas_CheckAccIsInUsed.gameObject.GetComponent<Animator> ().SetBool ("dispose", true);
		canvas_CheckAccIsInUsed.gameObject.GetComponent<Animator> ().SetBool ("TurnToBegin", false);
		//canvas_CheckAccIsInUsed.gameObject.SetActive (false);
	}
}
