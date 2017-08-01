using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MainSceneButton : NetworkBehaviour {
	public Canvas canvas_Before;
	public Canvas canvas_After;
	public Canvas canvas_Login;
	public InputField serverIP;
	public GameObject btn_Login;
	public AudioClip sound;
	AudioSource source;
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void openCanvasBefore(){
		StartCoroutine (IopenCanvasBefore ());
		//canvas_Login.gameObject.SetActive (false);
	}

	IEnumerator IopenCanvasBefore(){
		source.PlayOneShot (sound);
		yield return new WaitForSeconds (sound.length);
		canvas_Before.gameObject.SetActive (true);
		canvas_After.gameObject.SetActive (false);
	}

	public void openCanvasAfter(){
		StartCoroutine (IopenCanvasAfter ());
	}

	IEnumerator IopenCanvasAfter(){
		source.PlayOneShot (sound);
		yield return new WaitForSeconds (sound.length);
		canvas_Before.gameObject.SetActive (false);
		canvas_After.gameObject.SetActive (true);
		canvas_Login.gameObject.SetActive (false);
	}

	public void openCanvasLogin(){
		//canvas_Before.gameObject.SetActive (false);
		StartCoroutine(IopenCanvasLogin());
	}

	IEnumerator IopenCanvasLogin(){
		source.PlayOneShot (sound);
		yield return new WaitForSeconds (sound.length);
		canvas_After.gameObject.SetActive (false);
		canvas_Login.gameObject.SetActive (true);
	}

	public void InitNet()
	{
		StartCoroutine (I_InitNet ());
	}

	IEnumerator I_InitNet(){
		source.PlayOneShot (sound);
		yield return new WaitForSeconds (sound.length);
		Network.Connect(serverIP.text, Constants.cServerPort);
	}

	void OnConnectedToServer(){
		//canvas_Before.gameObject.SetActive (false);
		canvas_After.gameObject.SetActive (false);
		canvas_Login.gameObject.SetActive (true);
	}

	public void playOffline(){
		StartCoroutine (playSoundAndChangeToOfflineMode());
		//SceneManager.LoadScene ("DW_Pool_2", LoadSceneMode.Single);
		//Debug.Log("HIHI");
	}

	IEnumerator playSoundAndChangeToOfflineMode(){
		source.PlayOneShot (sound);
		yield return new WaitForSeconds (sound.length);
		SceneManager.LoadScene ("DW_Pool_2", LoadSceneMode.Single);
	}
}
