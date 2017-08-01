using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowServerController : MonoBehaviour {
	public static float parentTransform;
	// Use this for initialization
	void Start () {
		Debug.Log ("9HIUHKJHDKJADJKEDNKEJWN");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//	[RPC]
//	public void sendMovementToServer(string playerID, Vector3 position, Vector3 velocity){
//		GameObject go = (GameObject)Instantiate(stoneToClone);
//		go.transform.parent = null;
//		go.GetComponent<Rigidbody> ().useGravity = true;
//		go.transform.position = position;
//		go.GetComponent<Rigidbody> ().velocity = velocity;
//		Debug.Log (velocity);
//	}

	public void setParentTransform(float f){
		parentTransform = f;
	}

	void OnTriggerExit(Collider col){
		
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Stone" && col.transform.IsChildOf(this.gameObject.transform.parent) == false){
			//Client ID cua vien da' bay toi'
			string parentID = col.gameObject.transform.parent.name;
			parentID = parentID.Substring (parentID.Length - 1);

			float transformParent = col.gameObject.transform.parent.GetChild (0).gameObject.transform.eulerAngles.y;

			//Debug.Log ("Enter Map: " + this.gameObject.transform.parent.name);
			string playerID = this.gameObject.transform.parent.name;
			playerID = playerID.Substring (playerID.Length - 1);
			GameObject.Find ("Boundary").GetComponent<NetworkView> ().RPC ("isTouchBoundary", RPCMode.Others, parentID);
			GameObject.Find("Boundary").GetComponent<NetworkView> ().RPC ("createStoneOnClient", RPCMode.Others, new object[]{playerID, col.transform.position, col.gameObject.GetComponent<Rigidbody>().velocity, transformParent, parentID});
			Debug.Log (transformParent);
		}
	}


}
