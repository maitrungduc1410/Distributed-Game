using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowClientChooseMap : MonoBehaviour {
	public GameObject quad;
	public GameObject gameScene;
	public GameObject parentGameObject;

	public GUIText clientChoosing;
	public GameObject minimapChild;
	public GameObject minimapParent;

	private Vector3 planeScale;

	private Vector3 realClientMapPosition;
	void Start () {
		//khi bắt đầu đưa vị trí text quay trở lại 1 (vì những lần client chọn trước text bị dịch xuống dưới)
		clientChoosing.transform.position = new Vector3 (clientChoosing.transform.position.x, 1f, clientChoosing.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

	}



	public void createRealMapOnServer(string playerID, Vector3 scale, Vector3 position, Quaternion rotation){
		
		gameScene.transform.eulerAngles = new Vector3(0, rotation.eulerAngles.y + 90f, 0);
		gameScene.transform.localScale = new Vector3 (scale.x, gameScene.transform.localScale.y, scale.y);
		//phuc vu cho qua trinh ghep map
		gameScene.transform.position = new Vector3 (position.x - 10f, position.y - 10f, position.z - 10f);
		//
		GameObject go = Instantiate (gameScene);
		go.transform.position = position;
		go.name = "ClientMap" + playerID;
		parentGameObject.transform.localScale = new Vector3 (1f, 1f, 1f);
		parentGameObject.transform.position = new Vector3(-0.8126959f, 0.1f, 0.4127285f);

		go.transform.parent = parentGameObject.transform;
		parentGameObject.transform.localScale = new Vector3 (30f, 30f, 30f);
		parentGameObject.transform.position = new Vector3 (50f, 50f, 50f);
		realClientMapPosition = go.transform.position;

		Minimap l = (Minimap) minimapParent.GetComponent(typeof(Minimap));
		l.getTargetTransform ();

		GameObject minimap_Child = Instantiate (minimapChild);
		minimap_Child.name = "Minimap" + playerID;

		Blip k = (Blip) minimap_Child.GetComponent(typeof(Blip));
		k.setTartget (go.transform.GetChild(0).gameObject);
		//this.GetComponent<NetworkView> ().RPC ("sendSignalToClientToCreateMinimap", RPCMode.Others, playerID);

	}

	public Vector3 getPlaneScale(){
		return planeScale;
	}

	#region RPC

	[RPC]
	public void sendClientMapPosition(string playerID, Vector3 scale, Vector3 position, Quaternion rotation){
		quad.transform.localScale = scale;
		quad.transform.position = position;
		quad.transform.rotation = rotation;
		GameObject go_quad = Instantiate (quad);
		go_quad.name = "ChooseMap" + playerID;
		this.GetComponent<NetworkView> ().RPC ("sendMapToOtherClients", RPCMode.All, new object[]{playerID, scale, position, rotation});
		createRealMapOnServer (playerID, scale, position, rotation);
		this.planeScale = scale;


	}

	[RPC]
	public void sendMapToOtherClients(string playerID, Vector3 scale, Vector3 position, Quaternion rotation){

	}

	[RPC]
	public void checkIsDoneChoosingMap(string playerID, bool check){
		if (check == false) {
			clientChoosing.name = "ChoosingText" + playerID;
			clientChoosing.text = "Player " + playerID + " is choosing map";
			clientChoosing.transform.position = new Vector3 (clientChoosing.transform.position.x, clientChoosing.transform.position.y - 0.15f, clientChoosing.transform.position.z);
			Instantiate (clientChoosing);
		}
		if (check == true) {
			Destroy (GameObject.Find ("ChoosingText" + playerID + "(Clone)"));
			this.GetComponent<NetworkView> ().RPC ("checkReadyToBeginGame", RPCMode.All, new object[]{playerID});
			this.GetComponent<NetworkView> ().RPC ("sendClientRealTransform", RPCMode.All, new object[]{playerID, gameScene.transform.lossyScale, realClientMapPosition, gameScene.transform.rotation});

		}
	}

	[RPC]
	public void checkReadyToBeginGame(string playerID){

	}

	[RPC]
	public void sendClientRealTransform(string playerID, Vector3 scale, Vector3 position, Quaternion rotation){

	}

	[RPC]
	public void changeRealMapOnClient(string playerID, string wallName){

	}

	[RPC]
	public void printhehe(string s){

	}

	[RPC]
	public void RequestChooseMapInforWhenStart(string playerID){
		GameObject[] list_ChooseMap = GameObject.FindGameObjectsWithTag ("ClientMap");
		foreach (GameObject go in list_ChooseMap) {
			this.GetComponent<NetworkView> ().RPC ("SendChooseMapInforWhenStart", RPCMode.Others, new object[]{playerID, go.transform.position, go.transform.rotation, go.transform.lossyScale});
		}
	}

	[RPC]
	public void SendChooseMapInforWhenStart(string playerID, Vector3 position, Quaternion rotation, Vector3 scale){

	}

	#endregion
}
