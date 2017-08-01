using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LostPolygon.DynamicWaterSystem;

public class AddDuckOnClient : MonoBehaviour {
	public Transform Duck;
	public Transform water_Transform;
	public DynamicWater Water = null;
	private static int count = 1;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void DropDuck(){
		if (Duck != null) {
			//DW_GUILayout.tooltip = "Drops a crate into water. You can drag it around to see how it makes splashes when going in and out of water.";
			//if (DW_GUILayout.Button("Drop a box!", 180f)) {
			Bounds bounds = Water.GetComponent<Collider>().bounds;
			Transform go = Instantiate(Duck, new Vector3(Random.Range(bounds.min.x, bounds.max.x), water_Transform.transform.position.y, Random.Range(bounds.min.z, bounds.max.z)),
				Quaternion.Euler(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f)));
			go.rotation = Quaternion.Euler (0, 0, 0);
			//go.gameObject.GetComponent<Rigidbody> ().drag = 3f;
			//go.gameObject.GetComponent<Rigidbody> ().angularDrag = 3f;
			go.gameObject.name = Network.player.ToString() + "Duck" + count;
			go.gameObject.GetComponent<Rigidbody> ().centerOfMass = new Vector3(-0.1f, 1f, 0);
			Debug.Log (go.gameObject.GetComponent<Rigidbody> ().centerOfMass);
			GameObject.Find("Boundary").GetComponent<NetworkView>().RPC("addItemDirectlyToServer", RPCMode.Server, new object[]{Network.player.ToString(), go.gameObject.name, go.localScale, go.position, go.rotation });
			count++;
		}
	}
}