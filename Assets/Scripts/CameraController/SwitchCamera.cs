using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour {
	private static int count = 0;

	public Camera[] cams;
	public Transform parent;

	void Update(){

	}
	// Use this for initialization
	public void camMainMove(){
		GameObject[] list_go = GameObject.FindGameObjectsWithTag ("ClientCamera");
		foreach (GameObject go in list_go) {
			go.SetActive (false);
		}
		//Debug.Log(Camera.main.gameObject.name);
		if (count % 2 == 0) {
			cams [0].enabled = false;
			cams [1].enabled = true;
			count++;
		} else if (count % 2 != 0) {
			cams [0].enabled = true;
			cams [1].enabled = false;
			count++;
		}

	}

	public void changeCamToSpecificClient(){
		int countChild = parent.childCount;
		int mod = count % countChild;
		//if (mod != 0) {
			cams [0].enabled = false;
			cams [1].enabled = false;
			parent.GetChild (mod).GetChild (0).GetChild (0).gameObject.SetActive (true);
		//}
		count++;
	}
		

}
