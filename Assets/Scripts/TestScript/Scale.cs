using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour {
	public GameObject middleRightUp;
	//private Vector3 go;
	// Use this for initialization
	void Start () {
		//GameObject go;
		//go = new Vector3 (rightup.transform.position.x, rightup.transform.transform.position.y - rightdown.transform.position.y, rightup.transform.position.z);
		float distance = Vector3.Distance (transform.position, middleRightUp.transform.position);
		transform.localScale = new Vector3 (distance, transform.localScale.y, transform.localScale.z);
		//Debug.Log (distance);
		transform.parent = middleRightUp.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
