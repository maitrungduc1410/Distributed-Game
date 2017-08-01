﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour {
	public GameObject parentbone;
	public Rigidbody rigid;
	//public float force;
	public bool hasBeenThrown;
	//public float maxDist = 30f;
	private bool check;

	[SerializeField] private Image image;
	[SerializeField] private Transform _bullseye;
	// Use this for initialization
	void Start () {
		transform.position = parentbone.transform.position;
		rigid.useGravity = false;
		hasBeenThrown = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasBeenThrown) {
			transform.position = parentbone.transform.position;
		}

		//Vector3 vel = rigid.velocity;
		//transform.Rotate(0, Mathf.Abs(vel.z * 20), 0);

	}


	public void ReleaseMe(){
		transform.parent = null;
		float _angle;
		_angle = image.transform.localScale.x * 100;
		rigid.useGravity = true;
		//transform.rotation = parentbone.transform.rotation;
		//rigid.AddForce (transform.forward * force);

		Vector3 pos = transform.position;
		Vector3 target = _bullseye.position;

		// distance between target and source
		float dist = Vector3.Distance(pos, target);

		// rotate the object to face the target
		transform.LookAt(target);

		// calculate initival velocity required to land the cube on target using the formula (9)
		float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * _angle * 2)));
		float Vy, Vz;   // y,z components of the initial velocity

		Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * _angle);
		Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _angle);

		// create the velocity vector in local space
		Vector3 localVelocity = new Vector3(0f, Vy, Vz);

		// transform it to global vector
		Vector3 globalVelocity = transform.TransformVector(localVelocity) * 3;

		// launch the cube by setting its initial velocity
		GetComponent<Rigidbody>().velocity = globalVelocity;

		hasBeenThrown = true;
	}

//	public void ReleaseMe()
//	{
//		transform.parent = null;
//		float _angle;
//		_angle = image.transform.localScale.x * 100;
//		rigid.useGravity = true;
//		Vector3 pos = transform.position;
//		Vector3 target = _bullseye.position;
//
//		// distance between target and source
//		float dist = Vector3.Distance(pos, target);
//
//		// rotate the object to face the target
//		transform.LookAt(target);
//
//		// calculate initival velocity required to land the cube on target using the formula (9)
//		float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * _angle * 2)));
//		float Vy, Vz;   // y,z components of the initial velocity
//
//		Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * _angle);
//		Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _angle);
//
//		// create the velocity vector in local space
//		Vector3 localVelocity = new Vector3(0f, Vy, Vz);
//
//		// transform it to global vector
//		Vector3 globalVelocity = transform.TransformVector(localVelocity);
//
//		// launch the cube by setting its initial velocity
//		GetComponent<Rigidbody>().velocity = globalVelocity;
//
//		// after launch revert the switch
//		//_targetReady = false;
//	}
}