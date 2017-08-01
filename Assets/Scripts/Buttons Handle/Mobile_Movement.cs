using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mobile_Movement : MonoBehaviour,IPointerDownHandler, IPointerUpHandler {
	private static bool isMoveUp;
	private static bool _pressed;
	private static bool isRotateLeft;
	private static bool isRotateRight;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(EventSystem.current.currentSelectedGameObject.name);
		if (isMoveUp == true)
			MoveUp ();
		else if (isMoveUp == false)
			StopAnimation ();
		if (isRotateLeft ==  true)
			RotateLeft ();
		if (isRotateRight ==  true)
			RotateRight ();
		else
			Stop ();
	}

	public void MoveUp(){
		GameObject go = GameObject.Find("Aj");
		float translation = 3f;
		translation *= Time.deltaTime;
		go.transform.Translate (0, 0, translation);
		go.SendMessage ("isRun_Mobile");
	}

	public void StopAnimation(){
		GameObject go = GameObject.Find("Aj");
		go.SendMessage ("isStop_Mobile");
	}

	public void RotateLeft(){
		GameObject go = GameObject.Find("Aj");
		float rotation = 30f;
		rotation *= Time.deltaTime;
		go.transform.Rotate (0, -rotation, 0);
	}

	public void RotateRight(){
		GameObject go = GameObject.Find("Aj");
		float rotation = 30f;
		rotation *= Time.deltaTime;
		go.transform.Rotate (0, rotation, 0);
	}

	public void Stop(){
		GameObject myChar = GameObject.Find("Aj");
		myChar.transform.Translate (0, 0, 0);
		//myChar.SendMessage ("isStop_Mobile");
	}
		

	public void OnPointerDown(PointerEventData eventData)
	{
		string s = EventSystem.current.currentSelectedGameObject.name;
		if(s.Equals("Button_MoveUp")){
			isMoveUp = true;
		}
		else if(s.Equals("Button_RotateLeft")){
			isRotateLeft = true;
		}
		else if(s.Equals("Button_RotateRight")){
			isRotateRight = true;
		}
		//_pressed = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		string s = EventSystem.current.currentSelectedGameObject.name;
		if(s.Equals("Button_MoveUp")){
			isMoveUp = false;
		}
		else if(s.Equals("Button_RotateLeft")){
			isRotateLeft = false;
		}
		else if(s.Equals("Button_RotateRight")){
			isRotateRight = false;
		}
		//_pressed = false;
	}
}
