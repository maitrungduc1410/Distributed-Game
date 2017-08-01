using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour {
	public GameObject parent;
	private GameObject[] listWalls;
	// Use this for initialization
	void Start () {
		listWalls =  GameObject.FindGameObjectsWithTag ("Wall");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		//GameObject go = GameObject.FindWithTag ("Wall");
		foreach (GameObject go in listWalls) {
			if (!go.transform.IsChildOf (parent.transform)) {
//				foreach (ContactPoint contact in collision.contacts) {
//					//print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
//					if (go.GetComponent<Collider> ().bounds.Contains (contact.point)) {
//						Debug.DrawRay (contact.point, contact.normal, Color.red, 20, true);
//						Debug.Log (contact.point);
//					}
//				}
				for (int i = 0; i < collision.contacts.Length; i++) {
					ContactPoint contact = collision.contacts[i];
					if (go.GetComponent<Collider> ().bounds.Contains (contact.point) && collision.contacts.Length <= 4) {
						Debug.DrawRay (contact.point, contact.normal, Color.red, 2, true);
						Debug.Log (contact.point + "---" + collision.contacts.Length);
					}
				}
			}
		}

	}
}
