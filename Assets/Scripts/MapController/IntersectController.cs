using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectController : MonoBehaviour {
	private GameObject[] listWalls;
	public GameObject parent;
	// Use this for initialization
	void Start () {
		listWalls =  GameObject.FindGameObjectsWithTag ("Wall");
		foreach (GameObject go1 in listWalls) {
			if (go1.GetComponent<Collider> ().bounds.Intersects (this.GetComponent<Renderer> ().bounds) && !go1.transform.IsChildOf(parent.transform)) {
				go1.SetActive (false);
				this.gameObject.SetActive (false);

			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
//	List<GameObject> getListWallsWithTag(){
//		listWalls =  GameObject.FindGameObjectsWithTag ("Wall");
//		return listWalls;
//	}
}
