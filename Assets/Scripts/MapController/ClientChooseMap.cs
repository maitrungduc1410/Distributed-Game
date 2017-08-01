using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClientChooseMap : MonoBehaviour{
	public GameObject quad;
	public GameObject newQuad;

	public GameObject testQuad;

	public Canvas canvas;
	public Text beginText;
	float distance = 10;
	float minX = -3.9f, maxX = 3.9f, minZ = -4.15f, maxZ = 4.15f;
	float width;
	float countAngle = 90;
	public bool isCreated = false;
	public bool isDragable = false;
	private bool check = false;

	public GameObject clientScene;
	public GameObject username;
	public GameObject minimap;
	void Start () {
		username = GameObject.FindGameObjectWithTag ("Username");
		this.GetComponent<NetworkView>().RPC("AddPlayerToList", RPCMode.Server, new object[]{Network.player.ToString(), Network.player.ipAddress, Application.platform.ToString(), username.name});
		this.GetComponent<NetworkView>().RPC("checkIsDoneChoosingMap", RPCMode.Server, new object[]{Network.player.ToString(), this.check});
		this.GetComponent<NetworkView> ().RPC ("RequestChooseMapInforWhenStart", RPCMode.Server, Network.player.ToString ());
	}
	
	// Update is called once per frame
	void Update () {
		if (quad.activeInHierarchy) {
			Vector3 currentPos = quad.transform.position;
			currentPos.x = Mathf.Clamp (currentPos.x, minX, maxX);
			currentPos.z = Mathf.Clamp (currentPos.z, minZ, maxZ);
			quad.transform.position = currentPos;
		}
		if (Input.GetMouseButtonDown (1)) {
			if (isDragable)
				isDragable = false;
			else
				isDragable = true;
		}
	}

	void OnGUI(){
		
		if(GUI.Button(new Rect(50, 50, 200, 100), "Send to server")){
			//Debug.Log ("Sent");
			this.GetComponent<NetworkView> ().RPC ("sendClientMapPosition", RPCMode.Server, new object[]{Network.player.ToString(), quad.transform.localScale, quad.transform.position, quad.transform.rotation});
			this.check = true;
			this.GetComponent<NetworkView>().RPC("checkIsDoneChoosingMap", RPCMode.Server, new object[]{Network.player.ToString(), this.check});

		}

		if (GUI.Button (new Rect (50, 350, 200, 100), "Rotate")) {
			quad.transform.rotation = Quaternion.Euler (90f, 0, countAngle);
			countAngle += 90f;
		}
	}



	void OnMouseEnter(){
		if (isCreated == false) {
			this.width = float.Parse (Screen.width.ToString());
			if (SystemInfo.deviceType.ToString ().Equals ("Handheld")) {
				this.width = 640f;
			}
			//this.height = float.Parse (Screen.height.ToString());
			//quad.transform.localScale = new Vector3 (this.width/640, this.height/480, 2f);
			float epsilon = this.width/640f;
			float originalScaleX = 2.75f;
			float originalScaleY = 0.95f;
			quad.transform.localScale = new Vector3 (originalScaleX * epsilon, originalScaleY * epsilon * 1.5f, 0.1f);
			quad = Instantiate(quad);
			isCreated = true;
			//Debug.Log (epsilon);

			//------
			minimap.transform.position = quad.transform.position;
			minimap.transform.rotation = quad.transform.rotation;
			minimap.transform.localScale = quad.transform.localScale;
			DontDestroyOnLoad (minimap);
		}
	}
	void OnMouseDrag(){
		if (isDragable) {
				Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distance);
				Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);
				quad.transform.position = new Vector3 (objPosition.x, 0.1f, objPosition.z);
		}

	}

	public IEnumerator setBeginText(){
		yield return new WaitForSeconds (1f);
		canvas.gameObject.SetActive (true);
		beginText.text = "Game starting in 3";
		yield return new WaitForSeconds (1f);
		//display “I AM FLASHING TEXT” for the next 0.5 seconds
		beginText.text = "Game starting in 2";
		yield return new WaitForSeconds (1f);
		beginText.text = "Game starting in 1";
		yield return new WaitForSeconds (1f);
		canvas.gameObject.SetActive (false);
		SceneManager.LoadScene ("DW_Pool_2", LoadSceneMode.Single);
	}

	void OnApplicationQuit(){
		this.GetComponent<NetworkView>().RPC("sendStateBeforeQuit", RPCMode.Server, new object[]{username.name, "0", ""});
	}

	#region RPC functions

	[RPC]
	public void RequestChooseMapInforWhenStart(string playerID){

	}

	[RPC]
	public void SendChooseMapInforWhenStart(string playerID, Vector3 position, Quaternion rotation, Vector3 scale){
		if (Network.player.ToString () == playerID) {
			newQuad.transform.localScale = scale;
			newQuad.transform.position = position;
			newQuad.transform.rotation = rotation;
			Instantiate (newQuad);
		}
	}

	//gửi thông tin map mà client vừa chọn(trong phòng chờ)
	[RPC]
	public void sendClientMapPosition(string playerID, Vector3 scale, Vector3 position, Quaternion rotation){

	}
	//gửi thông tin của client cho 
	[RPC]
	void AddPlayerToList(string loginName, string ip, string platform, string username)
	{
		
	}

	//nhận thông tin map của các client khác đc gửi đến từ 
	[RPC]
	public void sendMapToOtherClients(string playerID, Vector3 scale, Vector3 position, Quaternion rotation){
		if (playerID != Network.player.ToString ()) {
			//GameObject newQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
			newQuad.transform.localScale = scale;
			newQuad.transform.position = position;
			newQuad.transform.rotation = rotation;
			Instantiate (newQuad);

		}
	}
	//gửi tín hiệu cho server khi client chọn map 
	[RPC]
	public void checkIsDoneChoosingMap(string playerID, bool check){

	}

	//nhận tín hiệu đồng ý từ server để bắt dau
	[RPC]
	public void checkReadyToBeginGame(string playerID){
		if (playerID == Network.player.ToString ()) {
			//Debug.Log ("Done");     
			StartCoroutine (setBeginText());
			//Debug.Log ("HEHE");
		}

	}

	[RPC]
	public void sendClientRealTransform(string playerID, Vector3 scale, Vector3 position, Quaternion rotation){
		if (playerID == Network.player.ToString ()) {
			clientScene.transform.localScale = Vector3.Scale(scale, new Vector3(30f, 30f, 30f));
			clientScene.transform.position = position;
			clientScene.transform.rotation = rotation;
			DontDestroyOnLoad (clientScene);
			//SceneChangeManager l = (SceneChangeManager) this.gameObject.GetComponent(typeof(SceneChangeManager));
			//l.setWallName
		}
	}

	[RPC]
	public void changeRealMapOnClient(string playerID, string wallName){
		//if (playerID == Network.player.ToString ()) {

			Debug.Log (Network.player);
			Debug.Log (playerID);
			Debug.Log (wallName);
			//Debug.Log (wallPos_Scale [0]);
			////Debug.Log (wallRot [0]);
		//}
	}

	[RPC]
	public void sendStateBeforeQuit(string username, string isInUsed, string client){

	}
		

	#endregion
		
}
