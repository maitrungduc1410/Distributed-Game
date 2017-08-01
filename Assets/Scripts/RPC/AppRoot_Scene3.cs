using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppRoot_Scene3 : MonoBehaviour {

	#region Variables

	//	// rect for displaying of received message
	//	private readonly Rect cMsgRect = new Rect(20, 600, 200, 100);
	//	// received message
	//	private string mReceiveMessage = "No messages";
	//private GameObject cube;
	public GameObject mainStone;
	public GameObject go;
	#endregion
	///////////////////////////////////////////////////////////////////////////

	///////////////////////////////////////////////////////////////////////////
	#region Interface
	public void Start()
	{
		//InitNet();
	}

	public void Update()
	{
		//cube.transform.position += new Vector3 (0.1f, 0, 0);
	}

	public void OnGUI()
	{
		// just show received message
		//GUI.Label(cMsgRect, mReceiveMessage);
	}

	void OnConnectedToServer()
	{
		this.GetComponent<NetworkView>().RPC("AddPlayerToList", RPCMode.All, Network.player.ipAddress);
		Debug.Log ("Success");
	}

	public void OnTriggerEnter(){
		this.GetComponent<NetworkView>().RPC(Constants.cRPCSendPosition, RPCMode.Others, new object[] {mainStone.transform.position, mainStone.GetComponent<Rigidbody>().velocity});
		mainStone.gameObject.SetActive(false);
	}

	#region RPC functions

	//	[RPC]
	//	public void RPCSendMessage(string msg)
	//	{
	//		mReceiveMessage = "Message received = " + msg;
	//	}
	//
	//	[RPC]
	//	public void RPCSendData(byte[] data)
	//	{
	//		mReceiveMessage = "Data received. Data length = " + data.Length;
	//	}

	[RPC]
	void AddPlayerToList(string loginName)
	{

	}

	[RPC]
	public void RPCSendPosition(Vector3 position, Vector3 velocity)
	{
//		GameObject go1 = (GameObject)Instantiate(go);
//		go1.transform.parent = null;
//		go1.GetComponent<Rigidbody> ().useGravity = true;
//		go1.transform.position = position;
//		Vector3 newVel = velocity;
//		newVel.z = - velocity.z;
//		newVel.x = - velocity.x;
//		go1.GetComponent<Rigidbody> ().velocity = newVel;
//		Debug.Log ("RPC: " + newVel + "..." + velocity + " --pos: " + position);
	}

	[RPC]
	public void RPCSendPosition1(Vector3 position, Vector3 velocity)
	{
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
