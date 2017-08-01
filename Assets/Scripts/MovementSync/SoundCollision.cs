using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundCollision : MonoBehaviour {
	public AudioClip sound;
	AudioSource source;
	//public Text scoreBonusText;
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Stone") {
			source.PlayOneShot (sound);
			GameObject mainStone = GameObject.Find ("Capsule");
			Launcher l = (Launcher)mainStone.GetComponent (typeof(Launcher));

			l.numberOfScores += 500f;
			l.scoreText.text = l.numberOfScores.ToString ();
			l.scoreText.gameObject.GetComponent<Animator> ().SetTrigger ("SetScore");
			l.scoreText.gameObject.GetComponent<Animator> ().SetBool ("isIdle", true);

			StartCoroutine (IActiveBonusText ());
		}
	}

	IEnumerator IActiveBonusText(){
		GameObject mainStone = GameObject.Find ("Capsule");
		Launcher l = (Launcher)mainStone.GetComponent (typeof(Launcher));
		l.scoreBonusText.gameObject.SetActive (true);
		//Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
		//l.scoreBonusText.gameObject.transform.position = new Vector3 (screenPos.x + 45f, screenPos.y + 15f, 0);
		yield return new WaitForSeconds (2f);
		l.scoreBonusText.gameObject.SetActive (false);
	}
}
