using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowClientController : MonoBehaviour {
	public GameObject mainStone;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter(Collider col){
		if (col.transform.gameObject.name == "Capsule") {
//			GameObject.Find("Boundary").GetComponent<NetworkView> ().RPC ("sendMovementToServer", RPCMode.Server, new object[] {
//				Network.player.ToString(), mainStone.transform.position,
//				mainStone.GetComponent<Rigidbody> ().velocity
//			});
			//mainStone.gameObject.SetActive(false);
			//Debug.Log("Asdasd");
		}
		//Debug.Log(Network.player.ToString());
		//Debug.Log ("HAHAHAHAHAHAHAHAHAHAH");
	}
}
