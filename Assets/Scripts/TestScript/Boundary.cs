using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {
	private Renderer render;
	public GameObject quad2;
	public static bool isDragable = false;
	private Vector3 startPos;
	private Vector3 posRestrinct;
	//public Vector3 lastPos;
	public float distance;
	// Use this for initialization
	void Start () {
		Debug.Log (GetComponent<Renderer> ().bounds);
		//Debug.Log (quad2.transform.position);
		Debug.Log(Vector3.Distance(transform.position, quad2.transform.position));
		startPos = quad2.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			if (isDragable)
				isDragable = false;
			else {
				isDragable = true;
				//distance += 0.1f;
			}
			
		}
	}

	void OnMouseDrag(){
		if (isDragable) {
				Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10f);
				Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);
				//transform.position = new Vector3 (objPosition.x, 0.1f, objPosition.z);
				Vector3 newPos = new Vector3 (objPosition.x, 0.1f, objPosition.z);
				distance = Vector3.Distance (startPos, newPos);
				if (distance > (3f * Mathf.Sqrt (2)))
					distance = 3f * Mathf.Sqrt (2);
				if (distance < 3f)
					distance = 3f;
				posRestrinct = startPos + (newPos - startPos).normalized * distance;
				transform.position = posRestrinct;
				//lastPos = posRestrinct;
				//Debug.Log (transform.position + "--" + startPos);
		}
	}
//	bool checkOverlap(Collider collider){
//		return collider.bounds.Intersects(this.GetComponent<Renderer>().bounds);
//	}
//
//	void OnCollisionEnter(){
//		Debug.Log ("hahahahahahahahahaha");
//	}
}
