using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LostPolygon.DynamicWaterSystem;

public class AddItemClientController : MonoBehaviour {
	public GameObject mainStone;
	public GameObject Button_AddTube;
	public GameObject Button_AddTorus;
	public GameObject Button_AddDuck;
	public GameObject tube;
	public GameObject torus;
	public GameObject duck;
	// Use this for initialization
	private Vector3 original_stone;
	void Start () {
		original_stone = mainStone.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[RPC]
	public void addItem(string playerID,string itemName, float scale, float weight){
		if (Network.player.ToString () == playerID) {
			if (itemName == "stone") {
				mainStone.transform.localScale = original_stone;
				mainStone.GetComponent<Rigidbody> ().mass = 1f;

				//
				mainStone.transform.localScale = new Vector3 (mainStone.transform.localScale.x * scale, mainStone.transform.localScale.y * scale, mainStone.transform.localScale.z * scale);
				mainStone.GetComponent<Rigidbody> ().mass = weight;

				Launcher l = (Launcher)mainStone.GetComponent (typeof(Launcher));
				l.createCapsule ();

				BuoyancyForce b = (BuoyancyForce)mainStone.GetComponent (typeof(BuoyancyForce));
				b.Resolution = 3;
				b.Resolution = b.Resolution * int.Parse (scale.ToString ());
			} else if (itemName == "tube") {
				tube.transform.localScale = new Vector3 (2f, 1.5f, 2f);
				tube.GetComponent<Rigidbody> ().mass = 1f;


				tube.transform.localScale = new Vector3 (tube.transform.localScale.x * scale, tube.transform.localScale.y * scale, tube.transform.localScale.z * scale);
				tube.GetComponent<Rigidbody> ().mass = weight;

//				BuoyancyForce b = (BuoyancyForce) tube.GetComponent(typeof(BuoyancyForce));
//				b.Resolution = 3;
//				b.Resolution = b.Resolution * int.Parse(scale.ToString());

				DropABox drop = (DropABox)Button_AddTube.GetComponent (typeof(DropABox));
				drop.DropBox ();

			} else if (itemName == "torus") {
				torus.transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
				torus.GetComponent<Rigidbody> ().mass = 1f;


				torus.transform.localScale = new Vector3 (torus.transform.localScale.x * scale, torus.transform.localScale.y * scale, torus.transform.localScale.z * scale);
				torus.GetComponent<Rigidbody> ().mass = weight;

//				BuoyancyForce b = (BuoyancyForce) torus.GetComponent(typeof(BuoyancyForce));
//				b.Resolution = 3;
//				b.Resolution = b.Resolution * int.Parse(scale.ToString());

				DropABox drop = (DropABox)Button_AddTorus.GetComponent (typeof(DropABox));
				drop.DropBox ();

			} else if (itemName == "duck") {
				duck.transform.localScale = new Vector3 (4, 4, 4);
				duck.GetComponent<Rigidbody> ().mass = 1f;

				duck.transform.localScale = new Vector3 (duck.transform.localScale.x * scale, duck.transform.localScale.y * scale, duck.transform.localScale.z * scale);
				duck.GetComponent<Rigidbody> ().mass = weight;

				AddDuckOnClient l = (AddDuckOnClient) Button_AddDuck.GetComponent(typeof(AddDuckOnClient));
				l.DropDuck ();
			}

		}
	}

	[RPC]
	public void getItemsInformationFromClient(string playerID){
		if (Network.player.ToString () == playerID) {
			GameObject[] list_stone = GameObject.FindGameObjectsWithTag ("Stone");
			GameObject[] list_tube = GameObject.FindGameObjectsWithTag ("Tube");
			GameObject[] list_torus = GameObject.FindGameObjectsWithTag ("Torus");
			this.GetComponent<NetworkView> ().RPC ("sendItemsInformationFromClient", RPCMode.Server, new object[]{Network.player.ToString(), list_stone.Length, list_tube.Length, list_torus.Length});
		}
	}

	[RPC]
	public void sendItemsInformationFromClient(string playerID, int stoneNum, int tubeNum, int torusNum){

	}

	[RPC]
	public void deleteItemOnClient(string playerID, string itemName){
		if (Network.player.ToString () == playerID) {
			GameObject[] list_go = GameObject.FindGameObjectsWithTag (itemName);
			Destroy (list_go [0]);
			//------
			GameObject[] list_stone = GameObject.FindGameObjectsWithTag ("Stone");
			GameObject[] list_tube = GameObject.FindGameObjectsWithTag ("Tube");
			GameObject[] list_torus = GameObject.FindGameObjectsWithTag ("Torus");
			this.GetComponent<NetworkView> ().RPC ("sendItemsInformationFromClient", RPCMode.Server, new object[]{Network.player.ToString(), list_stone.Length, list_tube.Length, list_torus.Length});

		}
	}

	[RPC]
	public void addItemDirectlyToServer(string playerID, string itemName, Vector3 scale, Vector3 position, Quaternion rotation){

	}

	[RPC]
	public void cannotAddMoreStone(string playerID){

	}

	[RPC]
	public void syncDuckMove(string playerID, string DuckName, Vector3 position, Quaternion rotation){

	}
}
