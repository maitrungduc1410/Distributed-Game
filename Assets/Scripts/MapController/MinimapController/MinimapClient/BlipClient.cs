using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlipClient : MonoBehaviour {
	public Transform target;
	public bool keepInBounds = true;
	public bool lockScale = false;
	public bool LockRotation = false;
	public float MinScale = 1f;
	MinimapClient map;
	RectTransform myRectTransform;
	bool canUpdate = false;
	string name;
	// Use this for initialization
	void Start () {
		map = GetComponentInParent<MinimapClient> ();
		myRectTransform = GetComponent<RectTransform> ();
		name = this.gameObject.name.Substring(this.gameObject.name.Length - 1);
		//name = GameObject.Find("Aj" + name).
		target = GameObject.Find("Aj" + name).transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//if (canUpdate == true) {
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
		//}
	}

//	public void setTartget(GameObject go){
//		this.target = go.transform;
//		this.canUpdate = true;
//	}

}
