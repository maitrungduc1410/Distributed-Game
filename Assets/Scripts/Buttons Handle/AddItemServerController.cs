using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddItemServerController : MonoBehaviour {
	public Dropdown listClients;
	public Slider scale;
	public Slider weight;
	public Text ItemToChoose;
	public Text CurScaleVal;
	public Text CurWeightVal;

	//
	public Text stoneNum;
	public Text tubeNum;
	public Text torusNum;

	public GameObject tube;
	public GameObject torus;
	public GameObject duck;

	public Text text_notify;


	List<string> names = new List<string>();
	private string playerID;
	// Use this for initialization
	void Start () {
		//Debug.Log ("Degree: " + Mathf.Deg2Rad * 1f);
	}
	
	// Update is called once per frame
	void Update(){
		CurScaleVal.text = scale.value.ToString();
		CurWeightVal.text = weight.value.ToString();
	}
		
	public void printName(int index) {
		string[] items = listClients.options [listClients.value].text.Split (' ');
		if (listClients.value > 0) {
			this.playerID = items [0].Substring (items [0].Length - 1);
		}

	}

	public void chooseItem(string itemName){
		ItemToChoose.text = itemName;
	}


	public void addItemOnClient(){
		Vector3 stoneOriginalScale = new Vector3 (0.003064067f, 0.5f, 0.01157895f);
		this.GetComponent<NetworkView> ().RPC ("addItem", RPCMode.Others, new object[]{this.playerID, ItemToChoose.text, scale.value, weight.value });
		GameObject mainStone = GameObject.Find ("ClientMap" + this.playerID).transform.GetChild(2).gameObject;
		mainStone.transform.localScale = new Vector3 (stoneOriginalScale.x * scale.value, stoneOriginalScale.y * scale.value, stoneOriginalScale.z * scale.value);
	}

	[RPC]
	public void addItem(string playerID,string itemName, float scale, float weight){

	}

	[RPC]
	public void getItemsInformationFromClient(string playerID){

	}

	[RPC]
	public void sendItemsInformationFromClient(string playerID, int stoneNum, int tubeNum, int torusNum){
		this.stoneNum.text = "Stone: " + stoneNum;
		this.tubeNum.text = "Tube: " + tubeNum;
		this.torusNum.text = "Torus: " + torusNum;
	}

	[RPC]
	public void deleteItemOnClient(string playerID, string itemName){

	}

	[RPC]
	public void addItemDirectlyToServer(string playerID, string itemName, Vector3 scale, Vector3 position, Quaternion rotation){
		if (itemName == "Torus") {
			torus.transform.localScale = scale;
			torus.transform.position = position;
			torus.transform.rotation = rotation;
			GameObject go = Instantiate (torus);
			//go.transform.parent = GameObject.Find ("ClientMap" + playerID).transform;
		}
		if (itemName == "Tube") {
			tube.transform.localScale = scale;
			tube.transform.position = position;
			tube.transform.rotation = rotation;
			GameObject go = Instantiate (tube);
			//go.transform.parent = GameObject.Find ("ClientMap" + playerID).transform;
		}
		if (itemName.Contains("Duck")) {
			duck.transform.localScale = scale;
			duck.transform.position = position;
			duck.transform.rotation = rotation;
			GameObject go = Instantiate (duck);
			go.name = itemName;
			go.transform.parent = GameObject.Find ("ClientMap" + playerID).transform;
		}
	}

	[RPC]
	public void cannotAddMoreStone(string playerID){
		text_notify.gameObject.SetActive (true);
	}

	[RPC]
	public void syncDuckMove(string playerID, string DuckName, Vector3 position, Quaternion rotation){
		Debug.Log (DuckName);
		GameObject go = GameObject.Find (DuckName);
		go.transform.position = position;
		go.transform.rotation = rotation;
	}
}
