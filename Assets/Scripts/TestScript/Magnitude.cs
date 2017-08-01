using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnitude : MonoBehaviour {
	public Transform point1;
	public Transform point2;

	void Start(){
		Vector3 between = point2.position - point1.position;
		float distance = between.magnitude;
		transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, distance);
		transform.position = point1.position + (between / 2.0f);
		transform.LookAt (point2.position);
	}
}
