using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour,IPointerDownHandler, IPointerUpHandler {
	public Image currentHealthBar;
	public Text ratioText;
	public Text FPSText;
	private static bool _pressed = false;

	private float hitpoint = 0;
	private float maxHitpoitn = 100;

	private void Start(){
		UpdateHealthBar ();


	}

	private void Update(){
		if (Input.GetKey (KeyCode.Space) || _pressed) {
			HealDamage ();
		}


	}
		
	private void UpdateHealthBar(){
		float ratio = hitpoint / maxHitpoitn;
		currentHealthBar.rectTransform.localScale = new Vector3 (ratio, 1, 1);
		ratioText.text = (ratio * 100).ToString ("0") + "%";
	}

	private void TakeDamage(float damage){
		hitpoint -= damage;
		if (hitpoint < 0) {
			hitpoint = 0;
			Debug.Log ("Dead");
		}
		UpdateHealthBar ();
	}

	public void HealDamage(){
		hitpoint += Time.deltaTime * 10.0f * 1.5f;
		if (hitpoint > maxHitpoitn) {
			hitpoint = maxHitpoitn;
			//Debug.Log ("Dead");
		}
		UpdateHealthBar ();
	}


	public void OnPointerDown(PointerEventData eventData)
	{
		_pressed = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_pressed = false;
	}

	//lam tiep
	public void setToBegin(){
		this.hitpoint = 0;
		currentHealthBar.rectTransform.localScale = new Vector3 (0, 1, 1);
		ratioText.text = "0%";
		UpdateHealthBar ();
	}
				
}


