using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMoveClient : MonoBehaviour {
	int count = 1;
	public AudioClip sound;
	AudioSource source;
	// Use this for initialization
	void Start () {	
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		//this.GetComponent<Rigidbody> ().velocity = new Vector3 (6, 0, 6);

		if (count % 4 == 1) {
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (6, 0, 6);
		}
		else if (count % 4 == 2) {
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (-6, 0, 6);
		}
		else if (count % 4 == 3) {
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (6, 0, -6);
		}
		else if (count % 4 == 0) {
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (-6, 0, -6);
			//float tanA = vel.z / vel.x;
		}
		Vector3 vel = this.GetComponent<Rigidbody> ().velocity;
		float tanA = vel.z / vel.x;
		transform.eulerAngles = new Vector3 (0, Mathf.Atan (tanA) * Mathf.Rad2Deg, 0);
		changeVel (this.GetComponent<Rigidbody> ().velocity);
		//Debug.Log(this.transform.eulerAngles);

		//GameObject.Find ("Boundary").GetComponent<NetworkView> ().RPC ("syncDuckMove", RPCMode.Server, new object[]{Network.player.ToString(), this.gameObject.name, transform.position, transform.rotation});

	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "WallDuck") {
			count++;
		}
	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "WallDuck") {
			count++;
		}
	}

	void changeVel(Vector3 vel){
		if (vel.x > 0 && vel.z > 0) {
			transform.eulerAngles = new Vector3 (0, -Mathf.Abs (transform.eulerAngles.y), 0);
		}
		if (vel.x > 0 && vel.z < 0) {
			transform.eulerAngles = new Vector3 (0, Mathf.Abs (transform.eulerAngles.y), 0);
		}
		if (vel.x < 0 && vel.z > 0) {
			transform.eulerAngles = new Vector3 (0, -Mathf.Abs (transform.eulerAngles.y), 0);
		}
		if (vel.x < 0 && vel.z < 0) {
			transform.eulerAngles = new Vector3 (0, Mathf.Abs (transform.eulerAngles.y), 0);
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Stone") {
			source.PlayOneShot (sound);
			GameObject mainStone = GameObject.Find ("Capsule");
			Launcher l = (Launcher) mainStone.GetComponent(typeof(Launcher));
			l.numberOfStones--;
			l.turnCount.text = "Turns: " + (5 - l.numberOfStones + 1).ToString ();
			l.skipTimesText.gameObject.GetComponent<Animator>().SetTrigger("SetScore");
			l.skipTimesText.gameObject.GetComponent<Animator>().SetBool("isIdle", true);
			l.ActiveTurnBonus (Camera.main.WorldToScreenPoint(mainStone.transform.position));
		}
	}


}
