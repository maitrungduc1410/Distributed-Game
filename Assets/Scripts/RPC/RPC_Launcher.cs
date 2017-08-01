using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPC_Launcher : MonoBehaviour {
	[Range(10,90)] public float _angle = 20;
	public float maxDist = 30f;
    [SerializeField] private Image image;
	private Rigidbody rigid;
	public GameObject parentBone;
	private bool hasBeenThrown = false;
	public float parentTransform;

	//public Transform _bullseye;

	//parameters
	private Vector3 _velocity;
	private float mass;
	private float edge, tanB, tanO, C;
	private float Pw = 1000f;
	private string parentID;
	int timesOfSkipping;

	void Start(){
		parentID = this.gameObject.name.Substring (this.gameObject.name.Length - 1);
		Debug.Log (parentID);
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
				//Launch();
		}
		if (!hasBeenThrown) {
			transform.position = parentBone.transform.position;
		}

		//GameObject.Find("Boundary").GetComponent<NetworkView>().RPC("syncStoneTransform", RPCMode.Server, new object[]{parentID, transform.position, transform.rotation, this.GetComponent<Rigidbody>().velocity});
		Debug.Log ("Update: " + parentID);
	}
		
//	public void Launch()
//	{
//
//		float dist = image.transform.localScale.x * maxDist;
//		float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * _angle * 2)));
//		float Vy, Vz;
//
//		Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * _angle);
//		Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _angle);
//
//		GameObject go = GameObject.Find ("Aj");
//		float parentTransform = go.transform.rotation.eulerAngles.y;
//
//
//		if (Mathf.Cos (parentTransform * Mathf.Deg2Rad) <0) {
//			Vz = -Vz;
//		}
//
//		Vector3 localVelocity = new Vector3(0f, Vy, Vz);
//		localVelocity.x = Mathf.Sin (parentTransform * Mathf.Deg2Rad) * localVelocity.z;
//
//		GetComponent<Rigidbody>().velocity = localVelocity;
//		_velocity = localVelocity;
//		hasBeenThrown = true;
//
//	}

	//kiểm tra điều kiện để cho viên đá nảy 
	public bool checkSkipping(){
		Vector3 velocity = GetComponent<Rigidbody> ().velocity;
		float Vxz = Mathf.Sqrt (Mathf.Pow(velocity.x, 2) + Mathf.Pow(velocity.z, 2));
		_velocity = velocity;
		float tanB = velocity.y / Vxz;
		edge = transform.lossyScale.x;
		float C = 1 * Mathf.Cos (Mathf.Atan (tanB)) - 1 * Mathf.Sin(Mathf.Atan(tanB));

		float vc = Mathf.Sqrt ((float)((4 * GetComponent<Rigidbody>().mass * Mathf.Abs(Physics.gravity.y)) / (C * Pw * Mathf.Pow (edge, 2)))
			/ (1 - (((2 * Mathf.Pow(tanB, 2) * GetComponent<Rigidbody>().mass)) / (Mathf.Pow(edge, 3) * C * Pw * Mathf.Sin(Mathf.Atan(tanB)))))) * 10;
		//Debug.Log (velocity);
		//Debug.Log(vc);
		Debug.Log(Vxz + "=====" + vc + velocity);
		if (Vxz > vc)
			return true;
		else
			return false;
	}

	//tính vận tốc sau khi nẩy
	public Vector3 getNextVelocity(){
		//float currentZ = _velocity.z;
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
		velNew.y = Mathf.Abs(velOld.y);
		velNew.x = Vxz * Mathf.Sin (parentTransform * Mathf.Deg2Rad);
		velNew.z = Vxz * Mathf.Cos (parentTransform * Mathf.Deg2Rad);
		_velocity = velNew;
		Debug.Log (velNew + "/////" + velOld + "//" + parentTransform);
		//GetComponent<Rigidbody> ().velocity = velNew;
		return velNew;
		//Debug.Log(velNew + "--" + velOld);

	}

	public void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "DynamicWater") {
			timesOfSkipping++;

		}
		if (col.gameObject.name == "ScoreFlat") {
			string playerID = this.gameObject.name;
			playerID = playerID.Substring (playerID.Length - 1);
			GameObject.Find ("Boundary").GetComponent<NetworkView> ().RPC ("updateScore", RPCMode.Others, new object[]{playerID, timesOfSkipping, transform.position});
		}
		if (checkSkipping ()) {
			GetComponent<Rigidbody> ().velocity = getNextVelocity ();
			//getNextVelocity ();
			Debug.Log(GetComponent<Rigidbody> ().velocity);
		} else 
		{
			//col.isTrigger = false;
		}
		//GetComponent<Rigidbody>().velocity = new Vector3 (0, 7, -10);
	}
				
}
