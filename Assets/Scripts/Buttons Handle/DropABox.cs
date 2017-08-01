using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LostPolygon.DynamicWaterSystem;

public class DropABox : MonoBehaviour {
	public Transform BuoyantCrate;
	public Transform water_Transform;
	public DynamicWater Water = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DropBox(){
		if (BuoyantCrate != null) {
			//DW_GUILayout.tooltip = "Drops a crate into water. You can drag it around to see how it makes splashes when going in and out of water.";
			//if (DW_GUILayout.Button("Drop a box!", 180f)) {
				Bounds bounds = Water.GetComponent<Collider>().bounds;
			Transform go = Instantiate(BuoyantCrate, new Vector3(Random.Range(bounds.min.x, bounds.max.x), water_Transform.transform.position.y + 10f, Random.Range(bounds.min.z, bounds.max.z)),
					Quaternion.Euler(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f)));
			//}
			//DW_GUILayout.Space(5);
			GameObject.Find("Boundary").GetComponent<NetworkView>().RPC("addItemDirectlyToServer", RPCMode.Server, new object[]{Network.player.ToString(), BuoyantCrate.gameObject.tag, go.localScale, go.position, go.rotation });
			Debug.Log (BuoyantCrate.gameObject.tag); 
		}
	}
}
