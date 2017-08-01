using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapClient : MonoBehaviour {
	public Transform target;
	public float zoomLevel = 10f;
	Vector2 XRotation = Vector2.right;
	Vector2 YRotation = Vector2.up;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void LateUpdate () {
		XRotation = new Vector2 (target.right.x, -target.right.z);
		YRotation = new Vector2 (-target.forward.x, target.forward.z);
	}



	public Vector2 TransformPosition(Vector3 position){
		Vector3 offset = position - target.position;
		Vector2 newPosition = offset.x * XRotation;
		newPosition += offset.z * YRotation;
		newPosition *= zoomLevel;
		return newPosition;
	}

	public Vector3 TransformRotation(Vector3 rotation){
		return new Vector3 (0, 0, target.eulerAngles.y - rotation.y);
	}

	public Vector2 MoveInside(Vector2 point){
		Rect mapRect = GetComponent<RectTransform> ().rect;
		point = Vector2.Max (point, mapRect.min);
		point = Vector2.Min (point, mapRect.max);
		return point;
	}
	
}
