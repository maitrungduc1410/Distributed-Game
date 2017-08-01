using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSize : MonoBehaviour {
	public GameObject quad;
	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (quad.transform.position.x, transform.position.y, quad.transform.position.y);
		transform.position = quad.transform.position;
		Debug.Log (quad.transform.position + "--" + quad.transform.localPosition);
		//transform.rotation = quad.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
