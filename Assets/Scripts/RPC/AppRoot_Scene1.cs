using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AppRoot_Scene1 : MonoBehaviour {

	///////////////////////////////////////////////////////////////////////////
	#region Variables
	// timer to create some delay for sending messages
	private float mWaitTimeUpdate = 0.0f;
	private const float cMaxWaitTimeUpdate = 0.1f;
	//private NetworkViewID viewID;

	public GameObject mainStone;
	public GameObject go;
	//public Text vel;
	//private static List<string> listClients = new List<string>();
	public Image img;
	public GameObject myPanel;
	public Text clientNum;
	public Text clientIp;
	public Text clientOS;
	public Text username;
	//
	public Text user_username;
	public Text user_highScore;
	public Text user_createdDate;
	public Text user_accountState;
	public GameObject User;
	public Transform userManager_parent;
	//
	public Transform parent;

	string selectAllUsers = "http://localhost/SkippingStones/SelectAllUsers.php";
	string updateAccountStateURL = "http://localhost/SkippingStones/UpdateAccountState.php";
	#endregion
	///////////////////////////////////////////////////////////////////////////

	///////////////////////////////////////////////////////////////////////////
	#region Interface
	public void Start()
	{
		//
		InitNet();
		//viewID = Network.AllocateViewID();
		WWW itemsData = new WWW ("http://localhost/SkippingStones/MakeTableEmpty.php");
		//


	}

	public void Update()
	{
		
	}

	public void callToLoadListUser(){
		StartCoroutine (loadListUser ());
	}

	IEnumerator loadListUser(){
		for (int i = 0; i < userManager_parent.childCount; i++) {
			Destroy (userManager_parent.GetChild (i).gameObject);
		}


		WWW userData = new WWW (selectAllUsers);
		yield return userData;
		string userDataString = userData.text;
		userDataString = userDataString.Substring(0, userDataString.Length - 1);
		string[] listUser = userDataString.Split ('/');
		foreach(string userInfo in listUser){
			string[] userItems = userInfo.Split (';');
			this.user_username.text = "Username: " + userItems [1];
			this.user_createdDate.text = "Created date: " + userItems [3];
			this.user_highScore.text = "Highscore: " + userItems [4];
			if (userItems [5] == "0") {
				this.user_accountState.text = "Account state: Offline";
				this.user_accountState.color = Color.red;
			} else if (userItems [5] == "1") {
				this.user_accountState.text = "Account state: Playing";
				this.user_accountState.color = Color.blue;
			}
			//userManager_parent.transform.eulerAngles = new Vector3 (0, 0, 0);
			GameObject g = Instantiate (User, userManager_parent.position, Quaternion.identity, userManager_parent);
			g.name = userItems [1];
			//go_parent.transform.eulerAngles = new Vector3 (0, 0, 0);
		}
	}

	public void OnGUI()
	{
		// increment wait time
		if (mWaitTimeUpdate < cMaxWaitTimeUpdate)
		{
			mWaitTimeUpdate += Time.deltaTime;
		}
	}

	public void OnTriggerEnter(Collider col){
		if (col.transform.gameObject.name == "Capsule") {
			this.GetComponent<NetworkView> ().RPC ("RPCSendPosition", RPCMode.Others, new object[] {
				mainStone.transform.position,
				mainStone.GetComponent<Rigidbody> ().velocity
			});
			mainStone.gameObject.SetActive(false);
			//Debug.Log("Asdasd");
		}
	}

	public void disConnect(string clientNumber){
		//Debug.Log (clientNum);
		//Network.CloseConnection(Network.connections[clientNumber - 1], true);
	}
		
	///////////////////////////////////////////////////////////////////////////
	#region RPC functions

//	[RPC]
//	public void RPCSendPosition(Vector3 position, Vector3 velocity){
//		Debug.Log("MTD");
//		GameObject go1 = (GameObject)Instantiate(go);
//		go1.transform.parent = null;
//		go1.GetComponent<Rigidbody> ().useGravity = true;
//		go1.transform.position = position;
//		Vector3 newVel = velocity;
//		newVel.z = - velocity.z;
//		newVel.x = - velocity.x;
//		go1.GetComponent<Rigidbody> ().velocity = newVel;
//		Debug.Log ("RPC: " + newVel + "..." + velocity + " --pos: " + position);
//	}

	[RPC]
	public void RPCSendPosition(Vector3 position, Vector3 velocity){
		this.GetComponent<NetworkView>().RPC("createMovement", RPCMode.All, new object[]{2, position, velocity});
	}

	[RPC]
	public void createMovement(int targetID, Vector3 position, Vector3 velocity){

	}
		
	[RPC]
	void AddPlayerToList(string loginName, string ip, string platform, string username)
	{
		//Debug.Log ("Client connected");

		clientNum.text = loginName;
		clientIp.text = "Client Ip: " + ip;
		clientOS.text = "Client OS: " + platform;
		this.username.text = "Username: " + username;
		//Debug.Log (loginName + "--" +  ip + "--" + platform);
		GameObject go_panel = Instantiate (myPanel, parent.position, Quaternion.identity, parent);
		go_panel.name = "Client" + loginName;
		//Update DB
		WWWForm form1 = new WWWForm ();
		form1.AddField ("usernamePost", username);
		form1.AddField ("statePost", "1");
		form1.AddField ("clientPost", "Client" + loginName);
		WWW www1 = new WWW (updateAccountStateURL, form1);
	}

	[RPC]
	public void DisconnectAClient(string playerID){

	}
		
	#endregion
	///////////////////////////////////////////////////////////////////////////

	#endregion
	///////////////////////////////////////////////////////////////////////////

	///////////////////////////////////////////////////////////////////////////
	#region Implementation

	/// <summary>
	/// Initializes RPC server
	/// </summary>
	private void InitNet()
	{
		Network.InitializeServer(3, Constants.cServerPort, false);
	}

	#endregion
	///////////////////////////////////////////////////////////////////////////

	///////////////////////////////////////////////////////////////////////////
	#region Properties

	#endregion
	///////////////////////////////////////////////////////////////////////////
	/// 
	/// 

}
