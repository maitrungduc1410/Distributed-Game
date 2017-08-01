using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour {
	public float _angle;
	public Slider slider;
	public float maxDist = 30f;
    [SerializeField] private Image image;
	private Rigidbody rigid;
	public GameObject parentBone;
	public bool hasBeenThrown;
	public GameObject Stone;
	public GameObject server;
	public Text skipTimesText;
	public Text turnBonusText;
	public Text scoreBonusText;
	//public Transform _bullseye;

	//parameters
	public Vector3 _velocity;
	private float mass;
	private float edge, tanB, tanO, C;
	private float Pw = 1000f;
	public float parentTransform = 0f;

	public int numberOfStones = 1;
	public bool isTouch = false;
	public Text turnCount;
	public GameObject Background;

	//tinh diem
	public Text scoreText;
	float distanceOfSkipping;
	Vector3 beginPos;
	int timesOfSkipping;
	public float numberOfScores;


	void Start(){
		rigid = GetComponent<Rigidbody> ();
		rigid.useGravity = false;
		transform.position = parentBone.transform.position;
		turnCount.text = "Turns: " + (5 - numberOfStones + 1).ToString();
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
				Launch();
				//System.Threading.Thread.Sleep(1000);
				//Instantiate (gameObject);
			//transform.Rotate(Vector3.right * Time.deltaTime);
			//transform.Rotate( new Vector3(0, 10f, 0) );

		}
		if (!hasBeenThrown) {
			transform.position = parentBone.transform.position;
			GameObject.Find ("Capsule").GetComponent<Collider> ().isTrigger = true;

			//Xoay vien da

		}
		if (hasBeenThrown) {
			float x = GetComponent<Rigidbody> ().velocity.x;
			float z = GetComponent<Rigidbody> ().velocity.z;
			transform.Rotate( new Vector3(0, Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(z, 2)) * 2, 0) );
		}

		if (this.gameObject.activeInHierarchy) {
			GameObject.Find ("Boundary").GetComponent<NetworkView> ().RPC ("syncStoneTransform", RPCMode.Server, new object[]{Network.player.ToString(), transform.position, transform.rotation, GetComponent<Rigidbody>().velocity});
		}

		if (numberOfStones > 5) {
			GameObject.Find ("Boundary").GetComponent<NetworkView> ().RPC ("sendScoreToServerToSave", RPCMode.Server, new object[]{Network.player.ToString(), GameObject.FindGameObjectWithTag("Username").name, numberOfScores});

		}

	}

//	void OnGUI(){
//		if (GUI.Button (new Rect(100, 350, 100, 50), "Add Stone")) {
//			//Destroy (GetComponent<GameObject>());
//			//Instantiate (Stone);
//			hasBeenThrown = false;
//		}
//	}

	public void createCapsule(){
		if (numberOfStones <= 5) {
			hasBeenThrown = false;
			HealthBar l = (HealthBar) Background.GetComponent(typeof(HealthBar));
			l.setToBegin ();
		}
		if (numberOfStones > 5) {
			Debug.Log ("ko the add dc nua");
			GameObject.Find ("Boundary").GetComponent<NetworkView> ().RPC ("cannotAddMoreStone", RPCMode.Server, Network.player.ToString());
			turnCount.gameObject.GetComponent<Animator>().SetTrigger("SetScore");
			turnCount.gameObject.GetComponent<Animator>().SetBool("isIdle", true);
			//GameObject.Find ("Boundary").GetComponent<NetworkView> ().RPC ("sendScoreToServerToSave", RPCMode.Server, new object[]{Network.player.ToString(), GameObject.FindGameObjectWithTag("Username").name, numberOfScores});
			//StartCoroutine (noti_Endgame ());
		}
	}

//	IEnumerator noti_Endgame(){
//		yield return new WaitForSeconds (1f);
//		GameObject.Find ("Boundary").GetComponent<NetworkView> ().RPC ("getMyScoreInDB", RPCMode.Server, new object[]{Network.player.ToString(), GameObject.FindGameObjectWithTag("Username").name});
//	}
		
	public void Launch()
	{
		_angle = Mathf.Round(slider.value);
		GameObject.Find ("Capsule").GetComponent<Collider> ().isTrigger = false;
		transform.parent = null;
		rigid.useGravity = true;

		//transform.Rotate( new Vector3(90, 90, 90) );
		float dist = image.transform.localScale.x * maxDist;
		float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * _angle * 2)));
		float Vy,Vxz, Vx = 0, Vz = 0;

		Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * _angle);
		Vxz = Vi * Mathf.Cos(Mathf.Deg2Rad * _angle);
		//Debug.Log (Vxz);
		//Kiem tra chieu nem'
		GameObject go = GameObject.Find ("Aj");
		parentTransform = go.transform.rotation.eulerAngles.y;
//		if (Mathf.Cos (parentTransform * Mathf.Deg2Rad) <0) {
//			Vxz = - Mathf.Abs(Vxz);
//		}
		Vx = Vxz * Mathf.Sin (parentTransform * Mathf.Deg2Rad);
		Vz = Vxz * Mathf.Cos (parentTransform * Mathf.Deg2Rad);

		Debug.Log (Vxz + "--" + Mathf.Sin (parentTransform * Mathf.Deg2Rad));


		Vector3 localVelocity = new Vector3(Vx, Vy, Vz);
		//localVelocity.x = Mathf.Sin (parentTransform * Mathf.Deg2Rad) * localVelocity.z;

		//GetComponent<Rigidbody>().velocity = localVelocity;
		GetComponent<Rigidbody>().velocity = localVelocity;
		_velocity = localVelocity;

		hasBeenThrown = true;
		Debug.Log (GetComponent<Rigidbody>().velocity);
		Debug.Log (Vx + "---" + Vz);

		//For RPC
//		GameObject.Find ("Boundary").GetComponent<NetworkView> ().RPC ("sendVelocityToServer", RPCMode.Server, new object[] {
//			Network.player.ToString (),
//			GetComponent<Rigidbody>().velocity
//		});
		if (numberOfStones <= 5) {
			timesOfSkipping = 0;
			numberOfStones++;
			turnCount.text = "Turns: " + (5 - numberOfStones + 1).ToString ();
			beginPos = transform.position;
		}
	}

	//kiểm tra điều kiện để cho viên đá nảy 
	public bool checkSkipping(){
		Vector3 velocity = GetComponent<Rigidbody> ().velocity;
		float Vxz = Mathf.Sqrt (Mathf.Pow(velocity.x, 2) + Mathf.Pow(velocity.z, 2));
		float tanB = velocity.y / Vxz;
		edge = transform.lossyScale.x;
		float C = 1 * Mathf.Cos (Mathf.Atan (tanB)) - 1 * Mathf.Sin(Mathf.Atan(tanB));

		float vc = Mathf.Sqrt ((float)((4 * rigid.mass * Mathf.Abs(Physics.gravity.y)) / (C * Pw * Mathf.Pow (edge, 2)))
			/ (1 - (((2 * Mathf.Pow(tanB, 2) * rigid.mass)) / (Mathf.Pow(edge, 3) * C * Pw * Mathf.Sin(Mathf.Atan(tanB)))))) * 10;
		if (Vxz > vc)
			return true;
		else
			return false;
	}

	//tính vận tốc sau khi nẩy
	public void getNextVelocity(){
		Vector3 velOld = _velocity;
		Vector3 velNew = new Vector3();
		float Vxz = Mathf.Sqrt (Mathf.Pow(velOld.x, 2) + Mathf.Pow(velOld.z, 2));
		tanB = velOld.y / Vxz;
		C = 1 * Mathf.Cos (Mathf.Atan (tanB)) - 1 * Mathf.Sin(Mathf.Atan(tanB));
		mass = GetComponent<Rigidbody> ().mass;
			
		float l = 2 * Mathf.PI * Mathf.Sqrt ((2 * mass * Mathf.Sin(Mathf.Abs(Mathf.Atan(tanB)))) / (C * Pw * edge));

		float u = (Mathf.Cos (Mathf.Atan (tanB)) + Mathf.Sin (Mathf.Atan (tanB))) / (Mathf.Cos (Mathf.Atan (tanB)) - Mathf.Sin (Mathf.Atan (tanB)));
		float expression = Mathf.Pow (Vxz, 2) - 2 * u * -Physics.gravity.y * l * 3;
		if (expression < 0) {
			expression = 0;
		}
		Vxz = Mathf.Sqrt (expression);
//		if (Mathf.Cos (parentTransform * Mathf.Deg2Rad) <0) {
//			Vxz = -Vxz;
//		}
		velNew.y = velOld.y;
		velNew.x = Vxz * Mathf.Sin (parentTransform * Mathf.Deg2Rad);
		velNew.z = Vxz * Mathf.Cos (parentTransform * Mathf.Deg2Rad);

		//Debug.Log (velNew + "/////" + velOld);
		GetComponent<Rigidbody> ().velocity = velNew;
		_velocity = velNew;
		//Debug.Log(velNew);
//		Debug.Log(velOld + "//" +velNew + "//" + _velocity + "////" + Vxz + "///" + expression + "///" + parentTransform);
//		Debug.Log ("mass:" + mass + "//" + C + "//" + Pw + "//" + edge + "//" + u + "///" + tanB);


	}

	public void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "DynamicWater") {
			timesOfSkipping++;
			//Vector3 mousePosition = transform.position;
			//Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);
			skipTimesText.gameObject.SetActive (true);
			Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
			skipTimesText.gameObject.transform.position = new Vector3 (screenPos.x + 15f, screenPos.y, 0);
			skipTimesText.text = timesOfSkipping.ToString();
			skipTimesText.gameObject.GetComponent<Animator>().SetTrigger("SetScore");
			skipTimesText.gameObject.GetComponent<Animator>().SetBool("isIdle", true);

		}
		if (col.gameObject.name == "ScoreFlat") {
			//Debug.Log ("Last score: " + numberOfScores);
			distanceOfSkipping = Vector3.Distance (beginPos, transform.position);
			numberOfScores += Mathf.Round (distanceOfSkipping * Mathf.Pow (2, timesOfSkipping - 1));
			scoreText.text = numberOfScores.ToString();
			//Debug.Log ("New score: " + numberOfScores + ",more: " + Mathf.Round (distanceOfSkipping * Mathf.Pow (2, timesOfSkipping - 1)));
			scoreText.gameObject.GetComponent<Animator>().SetTrigger("SetScore");
			scoreText.gameObject.GetComponent<Animator>().SetBool("isIdle", true);
			Debug.Log ("TOuching");
			if (numberOfStones > 5) {
				GameObject.Find ("Boundary").GetComponent<NetworkView> ().RPC ("getMyScoreInDB", RPCMode.Server, new object[]{Network.player.ToString(), GameObject.FindGameObjectWithTag("Username").name});
			}
			StartCoroutine (waitToDisActiveSkipText ());
		}
		if (col.gameObject.tag != "WallDuck") {
			if (checkSkipping ()) {
				getNextVelocity ();
			} else 
			{
				Vector3 curVel = this.GetComponent<Rigidbody> ().velocity;
				this.GetComponent<Rigidbody> ().velocity = new Vector3 (curVel.x, -5f, curVel.z);

				//Tinh Diem

			}

			//-----
			GameObject[] list_tube = GameObject.FindGameObjectsWithTag ("Tube");
			foreach (GameObject go in list_tube) {
				float subX = Mathf.Abs (go.transform.position.x - this.transform.position.x);
				float subZ = Mathf.Abs (go.transform.position.z - this.transform.position.z);
				if (subX < 10f && subZ < 15f) {
					StartCoroutine (pickUp (go));
				}
				//Debug.Log (go.name + "-" + subX + "-" + subZ);
			}
			//---
		}

	}

	IEnumerator pickUp(GameObject go){
		float cons = 0.1f;
		for (int i = 0; i < 2; i++) {
			go.transform.position = new Vector3 (go.transform.position.x, go.transform.position.y + cons, go.transform.position.z);
			yield return new WaitForSeconds(1.5f);
		}
	}

	public void CountTotalScoreInAllClients(int additionalSkipTimes, Vector3 position){
		distanceOfSkipping = Vector3.Distance (beginPos, position);
		numberOfScores += Mathf.Round (distanceOfSkipping * Mathf.Pow (2, timesOfSkipping + additionalSkipTimes - 1));
		scoreText.text = numberOfScores.ToString();
		//Debug.Log ("New score: " + numberOfScores + ",more: " + Mathf.Round (distanceOfSkipping * Mathf.Pow (2, timesOfSkipping - 1)));
		scoreText.gameObject.GetComponent<Animator>().SetTrigger("SetScore");
		scoreText.gameObject.GetComponent<Animator>().SetBool("isIdle", true);
		Debug.Log ("Times Skipping: " + timesOfSkipping + "---" + additionalSkipTimes);
		skipTimesText.text = (timesOfSkipping + additionalSkipTimes).ToString();
	}
		
	IEnumerator waitToDisActiveSkipText(){
		yield return new WaitForSeconds (2f);
		skipTimesText.gameObject.SetActive (false);
	}

	public void PwaitToDisActiveSkipText(){
		StartCoroutine (waitToDisActiveSkipText ());
	}

	IEnumerator IActiveTurnBonus(Vector3 position){
		turnBonusText.gameObject.SetActive (true);
		turnBonusText.gameObject.transform.position = new Vector3 (position.x + 55f, position.y, position.z);
		yield return new WaitForSeconds (2f);
		turnBonusText.gameObject.SetActive (false);
	}

	public void ActiveTurnBonus(Vector3 position){
		StartCoroutine (IActiveTurnBonus (position));
	}
}
