using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skipping : MonoBehaviour {
	public float constant;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnCollisionEnter(Collision col){
		GameObject go = col.gameObject;
		Launcher l = (Launcher) go.GetComponent(typeof(Launcher));
		l.checkSkipping ();

//		col.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0f, 5f - constant*0.5f, 5f - constant*0.5f);
//		constant += 0.5f;
//		col.gameObject.SendMessage ("UpdateCount");
	}
		
}
