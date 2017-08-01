using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AjController : MonoBehaviour {
	static Animator anim;
	public float speed = 10.0f;
	public float rotationSpeed = 100.0f;
	private bool _pressed;
	//public Image forceBar;
	//public Text forceText;


	public GameObject server;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		//Debug.Log (SystemInfo.deviceType);
		//server.GetComponent<NetworkView> ().RPC ("sendTransform", RPCMode.Server,new object[] {Network.player.ToString(), transform.position, transform.rotation});
	}
	
	// Update is called once per frame
	void Update () {
		float translation = Input.GetAxis ("Vertical") * speed;
		float rotation = Input.GetAxis ("Horizontal") * rotationSpeed;
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;
		transform.Translate (0, 0, translation);
		transform.Rotate (0, rotation, 0);
		server.GetComponent<NetworkView> ().RPC ("syncTransform", RPCMode.Server,new object[] {Network.player.ToString(), transform.position, transform.rotation});
	
		if (Input.GetButtonDown ("Jump")) {
			anim.SetTrigger ("isJumping");
			server.GetComponent<NetworkView> ().RPC ("sendJumping", RPCMode.Server, Network.player.ToString());
		}

		if (Input.GetKeyDown (KeyCode.RightShift)) {
			anim.SetTrigger ("isThrowing");
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			anim.SetTrigger ("isPickingUp");
		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			anim.SetTrigger ("isSad");
			//anim.SetBool ("isIdle", false);
		}

		if (Input.GetKeyDown (KeyCode.X)) {
			anim.SetTrigger ("isDancing");
			//anim.SetBool ("isIdle", false);
		}

		if (Input.GetKeyDown (KeyCode.C)) {
			anim.SetTrigger ("isCasting");
			//anim.SetBool ("isIdle", false);
			_pressed = false;
			server.GetComponent<NetworkView> ().RPC ("sendCasting", RPCMode.Server, Network.player.ToString());
		}

		if (translation != 0) {
			anim.SetBool ("isRunning", true);
			anim.SetBool ("isIdle", false);
			server.GetComponent<NetworkView> ().RPC ("sendTranslationAnimations", RPCMode.Server, new object[]{ Network.player.ToString (), true, false });
		} else {
			anim.SetBool ("isRunning", false);
			anim.SetBool ("isIdle", true);
			server.GetComponent<NetworkView> ().RPC ("sendTranslationAnimations", RPCMode.Server, new object[]{ Network.player.ToString (), false, true });

		}
			
	}

	public void isCanThrow()
	{
		anim.SetTrigger ("isCasting");
		server.GetComponent<NetworkView> ().RPC ("sendCasting", RPCMode.Server, Network.player.ToString());
	}

	public void isRun_Mobile(){
		anim.SetBool ("isRunning_Mobile", true);
		anim.SetBool ("isIdle", false);
		//Debug.Log ("Start");
		server.GetComponent<NetworkView> ().RPC ("sendTranslationAnimations", RPCMode.Server, new object[]{ Network.player.ToString (), true, false });
	}

	public void isStop_Mobile(){
		anim.SetBool ("isRunning_Mobile", false);
		anim.SetBool ("isIdle", true);
		//Debug.Log("Stop");
		server.GetComponent<NetworkView> ().RPC ("sendTranslationAnimations", RPCMode.Server, new object[]{ Network.player.ToString (), false, true });
	}

//	public void isRun(){
//		anim.SetBool ("isRunning", true);
//		anim.SetBool ("isIdle", false);
//		//Debug.Log ("Running");
//	}
//
//	public void isIdle(){
//		anim.SetBool ("isRunning", false);
//		anim.SetBool ("isIdle", true);
//		//Debug.Log ("Idle");
//	}

//	public void OnPointerUp(PointerEventData eventData)
//	{
//		_pressed = false;
//	}
}
