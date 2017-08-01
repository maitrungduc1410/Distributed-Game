using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveRandomly : MonoBehaviour {
	// Use this for initialization
	//float[] arr = {1, -1};
	public Vector3 startPos;
	void Start () {
		//nav = gameObject.GetComponent<NavMeshAgent> ();
		startPos = transform.position;
		this.GetComponent<Rigidbody> ().velocity = new Vector3 (transform.lossyScale.x * 6f, 0, transform.lossyScale.z * 6f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(){
//		int value = Random.Range (-90, 90);
		Vector3 vel = this.GetComponent<Rigidbody> ().velocity;
//		if (vel.x < 3f || vel.z < 3f) {
//			vel = new Vector3 (transform.lossyScale.x * 6f, 0, transform.lossyScale.z * 6f);
//		}
		this.GetComponent<Rigidbody> ().velocity = new Vector3 ( -3f, vel.y,-3f);
		Debug.Log ("nldaskdnkasld");
		//Debug.Log(
	}
}
