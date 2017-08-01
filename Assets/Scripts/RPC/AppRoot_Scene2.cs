using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AppRoot_Scene2 : MonoBehaviour {

	#region Variables

//	// rect for displaying of received message
//	private readonly Rect cMsgRect = new Rect(20, 600, 200, 100);
//	// received message
//	private string mReceiveMessage = "No messages";
	//private GameObject cube;
	public GameObject gameScene;
	public GameObject mainStone;
	public GameObject Aj;
	public GameObject go;
	public Text text_disconnect;
	public Text text_connect;
	public Text stt;
	public Text networkNumber;
	public Canvas canvas_Disconnect;
	#endregion
	///////////////////////////////////////////////////////////////////////////

	///////////////////////////////////////////////////////////////////////////
	#region Interface
	public void Start()
	{
		//InitNet();
		GameObject clientScene = GameObject.Find("ClientScene");
		gameScene.transform.position = clientScene.transform.position;
		gameScene.transform.localScale = clientScene.transform.localScale;
		gameScene.transform.rotation = clientScene.transform.rotation;
		Aj.transform.parent = null;
		mainStone.transform.parent = null;
		Aj.transform.localScale = new Vector3 (2f, 2f, 2f);
		mainStone.transform.localScale = new Vector3(0.2527856f, 0.04999999f, 0.4950002f);

	}

	public void Update()
	{
		//cube.transform.position += new Vector3 (0.1f, 0, 0);
		if (Input.GetKey (KeyCode.T)) {
			Network.Disconnect ();
		}
	}

	public void OnGUI()
	{
		// just show received message
		//GUI.Label(cMsgRect, mReceiveMessage);
	}

	void OnConnectedToServer()
	{
		text_disconnect.gameObject.SetActive (false);
		text_connect.gameObject.SetActive (true);
		this.GetComponent<NetworkView>().RPC("AddPlayerToList", RPCMode.Server, new object[]{Network.player.ToString(), Network.player.ipAddress, Application.platform.ToString()});
		//this.GetComponent<NetworkView>().RPC("getClientScreenResolution", RPCMode.Server, new object[]{Screen.width.ToString(), Screen.height.ToString()});
		NetworkViewID viewID = Network.AllocateViewID();
		//Debug.Log (viewID);
		networkNumber.text = Network.player.ToString ();



//		Debug.Log (NetworkConnection.);
		//Debug.Log (Screen.height + "---" + Screen.width);
	}

	void OnDisconnectedFromServer(NetworkDisconnection info) {
		text_connect.gameObject.SetActive (false);
		text_disconnect.gameObject.SetActive (true);
		canvas_Disconnect.gameObject.SetActive (true);
		//Debug.Log("Da ngat ket noi: " + info);
	}

	public void OnTriggerEnter(Collider col){
		if (col.transform.gameObject.name == "Capsule") {
			this.GetComponent<NetworkView> ().RPC ("RPCSendPosition", RPCMode.Server, new object[] {
				mainStone.transform.position,
				mainStone.GetComponent<Rigidbody> ().velocity
			});
			mainStone.gameObject.SetActive(false);
			//Debug.Log("Asdasd");
		}
	}

	public void keepPlayingOffline(){
		canvas_Disconnect.gameObject.SetActive (false);
	}

	public void goBackToMainMenu(){
		SceneManager.LoadScene ("MainMenu", LoadSceneMode.Single);
		//NetworkManager.singleton.StopClient();
	}
		

	#region RPC functions

	[RPC]
	public void SendNetViewID(string s){

	}

	[RPC]
	void AddPlayerToList(string loginName, string ip, string platform)
	{
		
	}

	[RPC]
	public void getClientScreenResolution(string width, string height){

	}

	[RPC]
	public void sendMovementToServer(string playerID, Vector3 position, Vector3 velocity){

	}

	[RPC]
	public void RPCSendPosition(string playerID, Vector3 position, Vector3 velocity)
	{
		//Debug.Log ("MTD");
		GameObject go1 = (GameObject)Instantiate(go);
		go1.transform.parent = null;
		go1.GetComponent<Rigidbody> ().useGravity = true;
		go1.transform.position = position;
		Vector3 newVel = velocity;
		newVel.z = - velocity.z;
		newVel.x = - velocity.x;
		go1.GetComponent<Rigidbody> ().velocity = newVel;
		//Debug.Log ("RPC: " + newVel + "..." + velocity + " --pos: " + position);
	}

	[RPC]
	public void createMovement(int targetID, Vector3 position, Vector3 velocity){
		//Debug.Log ("AKJHDKSJAHDSKAJHD");
		if (int.Parse (Network.player.ToString()) == targetID) {
			GameObject go1 = (GameObject)Instantiate(go);
			go1.transform.parent = null;
			go1.GetComponent<Rigidbody> ().useGravity = true;
			go1.transform.position = position;
			Vector3 newVel = velocity;
			newVel.z = - velocity.z;
			newVel.x = - velocity.x;
			go1.GetComponent<Rigidbody> ().velocity = newVel;
			Debug.Log ("RPC: " + newVel + "..." + velocity + " --pos: " + position);
		}
	}

	[RPC]
	public void changeRealMapOnClient(string playerID, string wallName){
		if (playerID == Network.player.ToString ()) {
			Debug.Log (playerID);
			Debug.Log (wallName);
			//Debug.Log (wallPos_Scale [0]);
			//Debug.Log (wallRot [0]);
		}
	}

	[RPC]
	public void DisconnectAClient(string playerID){
		if (Network.player.ToString() == playerID) {
			this.GetComponent<NetworkView> ().RPC ("sendStateBeforeQuit", RPCMode.Server, new object[] {
				GameObject.FindGameObjectWithTag ("Username").name,
				"0",
				""
			});
			this.GetComponent<NetworkView> ().RPC ("deleteMapBeforeQuit", RPCMode.Server, Network.player.ToString ());
			Network.Disconnect ();
		}
	}


	#endregion

	#endregion
	///////////////////////////////////////////////////////////////////////////

	///////////////////////////////////////////////////////////////////////////
	#region Implementation

	public void InitNet(string ip)
	{
		//
		Network.Connect(ip, Constants.cServerPort);
	}

	#endregion
	///////////////////////////////////////////////////////////////////////////

	///////////////////////////////////////////////////////////////////////////
	#region Properties

	#endregion
	///////////////////////////////////////////////////////////////////////////
}
