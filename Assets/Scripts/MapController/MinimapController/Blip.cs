using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blip : MonoBehaviour {
	public Transform target;
	public bool keepInBounds = true;
	public bool lockScale = false;
	public bool LockRotation = false;
	public float MinScale = 1f;
	Minimap map;
	RectTransform myRectTransform;
	// Use this for initialization
	void Start () {
		this.transform.parent = GameObject.FindGameObjectWithTag("Minimap").transform;
		map = GetComponentInParent<Minimap> ();
		myRectTransform = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector2 newPosition = map.TransformPosition (target.position);
		if (keepInBounds) {
			newPosition = map.MoveInside (newPosition);
		}
		if (!lockScale) {
			float scale = Mathf.Max (MinScale, map.zoomLevel);
			myRectTransform.localScale = new Vector3 (scale, scale, 1);

		}

		if (!LockRotation) {
			myRectTransform.localEulerAngles = map.TransformRotation (target.eulerAngles);
		}
		myRectTransform.localPosition = newPosition;
	}

	public void setTartget(GameObject go){
		target = go.transform;
	}
}
