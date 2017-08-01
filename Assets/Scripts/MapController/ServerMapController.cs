using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerMapController : MonoBehaviour {
	public GameObject map_GameObject;
	public Image map_image;
	public Transform panel;

	public GameObject quad;
	public Transform planeParent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	#region RPC functions
	[RPC]
	public void getClientScreenResolution(string width, string height){
		float w = float.Parse (width);
		float h = float.Parse (height);
		quad.transform.localScale = new Vector3 (w/300, h/300, 2f);
		Instantiate (quad);
	}

	#endregion
}
