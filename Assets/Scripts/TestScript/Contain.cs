using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contain : MonoBehaviour {
	public GameObject parent;
	private GameObject[] listWalls;
	public List<Vector3> listPoints = new List<Vector3>();

	public GameObject toRightUp;
	public bool isRightUpCreated = false;
	public GameObject toRightDown;
	public bool isRightDownCreated = false;

	//public GameObject toLeftUp;
	public bool isLeftUpCreated = false;
	//public GameObject toLeftDown;
	public bool isLeftDownCreated = false;
	void Start () {
		listWalls =  GameObject.FindGameObjectsWithTag ("Wall");
	}


	void Update () {

	}

	void OnCollisionEnter(Collision collision) {
		foreach (GameObject go in listWalls) {
			if (!go.transform.IsChildOf (parent.transform)) {
				for (int i = 0; i < collision.contacts.Length; i++) {
					ContactPoint contact = collision.contacts [i];
					if (go.GetComponent<Collider> ().bounds.Contains (contact.point) && collision.contacts.Length <= 4) {
						Debug.DrawRay (contact.point, contact.normal, Color.red, 200, true);
						listPoints.Add (contact.point);

						Debug.Log (contact.point + "---" + collision.contacts.Length);
						if (collision.gameObject.tag == "Wall") {
							collision.gameObject.SetActive (false);
							this.gameObject.SetActive (false);
						}
					}
				}
				Debug.Log (listPoints.Count);
				//makeNotKinematic ();
				if (listPoints.Count >= 3) {
					Vector3 v4 = listPoints [0];
					Vector3 v7 = listPoints [1];

					if (collision.gameObject.name.Contains ("Right") && isRightDownCreated == false && isRightUpCreated == false) {
						
						GameObject wallRightDown = Instantiate (toRightDown);
						GameObject wallRightUp = Instantiate (toRightUp);
						Debug.Log ("CONTAIN RIGHT");
						wallRightDown.tag = "ToRight";
						wallRightUp.tag = "ToRight";

						GameObject middleRightDown = collision.gameObject.transform.parent.Find ("MiddleRightDown").gameObject;
						wallRightDown.transform.position = new Vector3 (v7.x, middleRightDown.transform.position.y, v7.z);
						float distanceDown = Vector3.Distance (wallRightDown.transform.position, middleRightDown.transform.position);
						wallRightDown.transform.localScale = new Vector3 (distanceDown, wallRightDown.transform.localScale.y, wallRightDown.transform.localScale.z);

						GameObject middleRightUp = collision.gameObject.transform.parent.Find ("MiddleRightUp").gameObject;
						wallRightUp.transform.position = new Vector3 (v4.x, middleRightUp.transform.position.y, v4.z);
						float distanceUp = Vector3.Distance (wallRightUp.transform.position, middleRightUp.transform.position);
						wallRightUp.transform.localScale = new Vector3 (distanceUp, wallRightUp.transform.localScale.y, wallRightUp.transform.localScale.z);

						//wallRightUp.transform.rotation = Quaternion.Euler (0, this.transform.parent.transform.parent.transform.rotation.y - 360f, 0);
						//wallRightDown.transform.rotation = Quaternion.Euler (0, -(this.transform.parent.transform.parent.transform.rotation.y - 360f), 0);

						isRightDownCreated = true;
						isRightUpCreated = true;
						wallRightUp.transform.parent = collision.gameObject.transform.parent;
						wallRightDown.transform.parent = collision.gameObject.transform.parent;

						//Insert map state into db
						string playerID = collision.gameObject.transform.parent.transform.parent.name;
						playerID = playerID.Substring (playerID.Length - 1);
						string thisPlayerID = this.gameObject.transform.parent.transform.parent.name;
						thisPlayerID = thisPlayerID.Substring (thisPlayerID.Length - 1);
						GameObject boundaryObj = GameObject.Find("Boundary");
						boundaryObj.SendMessage ("InsertMapState", new object[]{playerID, "Right", JoinString(wallRightUp.transform), JoinString(wallRightDown.transform), thisPlayerID});
						boundaryObj.SendMessage ("InsertMapState", new object[]{thisPlayerID, "Front", "", "", playerID});

					}

					if (collision.gameObject.name.Contains ("Left") && isLeftDownCreated == false && isLeftUpCreated == false) {
						
						GameObject wallLeftDown = Instantiate (toRightDown);
						GameObject wallLeftUp = Instantiate (toRightUp);

						wallLeftDown.tag = "ToLeft";
						wallLeftUp.tag = "ToLeft";
						//------
						GameObject middleLeftDown = collision.gameObject.transform.parent.Find ("MiddleLeftDown").gameObject;
						wallLeftDown.transform.position = new Vector3 (v4.x, middleLeftDown.transform.position.y, v4.z);		
						float distanceDown = Vector3.Distance (wallLeftDown.transform.position, middleLeftDown.transform.position);
						wallLeftDown.transform.localScale = new Vector3 (distanceDown, wallLeftDown.transform.localScale.y, wallLeftDown.transform.localScale.z);
												//------
						GameObject middleLeftUp = collision.gameObject.transform.parent.Find ("MiddleLeftUp").gameObject;
						wallLeftUp.transform.position = new Vector3 (v7.x, middleLeftUp.transform.position.y, v7.z);
						float distanceUp = Vector3.Distance (wallLeftUp.transform.position, middleLeftUp.transform.position);
						wallLeftUp.transform.localScale = new Vector3 (distanceUp, wallLeftUp.transform.localScale.y, wallLeftUp.transform.localScale.z);
						
						isLeftDownCreated = true;
						isLeftUpCreated = true;
						wallLeftUp.transform.parent = collision.gameObject.transform.parent;
						wallLeftDown.transform.parent = collision.gameObject.transform.parent;

						//InsertMapState
						string playerID = collision.gameObject.transform.parent.transform.parent.name;
						playerID = playerID.Substring (playerID.Length - 1);
						string thisPlayerID = this.gameObject.transform.parent.transform.parent.name;
						thisPlayerID = thisPlayerID.Substring (thisPlayerID.Length - 1);
						GameObject boundaryObj = GameObject.Find("Boundary");
						boundaryObj.SendMessage ("InsertMapState", new object[]{playerID, "Left", JoinString(wallLeftUp.transform), JoinString(wallLeftDown.transform), thisPlayerID});
						boundaryObj.SendMessage ("InsertMapState", new object[]{thisPlayerID, "Front", "", "", playerID});

					}




				}
				if (listPoints.Count >= 1 && collision.gameObject.name.Contains ("Front") && isRightUpCreated == false && isLeftUpCreated == false) {
					//Vector3 v = listPoints [0];
					//For insert into DB

						GameObject boundaryObj = GameObject.Find("Boundary");
						string playerCol = collision.gameObject.transform.parent.transform.parent.name;
						playerCol = playerCol.Substring (playerCol.Length - 1);
						string thisPlayerID = this.gameObject.transform.parent.transform.parent.name;
						thisPlayerID = thisPlayerID.Substring (thisPlayerID.Length - 1);
					//

					GameObject wallRightUp = Instantiate (toRightUp);
					GameObject wallLeftUp = Instantiate (toRightUp);

					wallRightUp.tag = "ToFront";
					wallLeftUp.tag = "ToFront";

					GameObject middleRightUp = collision.gameObject.transform.parent.Find ("MiddleRightUp").gameObject;
					GameObject thisMiddleLeftUp = this.transform.parent.Find ("MiddleLeftUp").gameObject;
					wallRightUp.transform.position = thisMiddleLeftUp.transform.position;
					float distanceUp = Vector3.Distance (wallRightUp.transform.position, middleRightUp.transform.position);
					wallRightUp.transform.localScale = new Vector3 (distanceUp, wallRightUp.transform.localScale.y, wallRightUp.transform.localScale.z);
					wallRightUp.transform.rotation = Quaternion.Euler (0, -180f, 0);

					GameObject middleLeftUp = collision.gameObject.transform.parent.Find ("MiddleLeftUp").gameObject;
					GameObject thisMiddleRightUp = this.transform.parent.Find ("MiddleRightUp").gameObject;
					wallLeftUp.transform.position = new Vector3 (thisMiddleRightUp.transform.position.x, wallRightUp.transform.position.y, wallRightUp.transform.position.z);
					float distanceDown = Vector3.Distance (wallLeftUp.transform.position, middleLeftUp.transform.position);
					wallLeftUp.transform.localScale = new Vector3 (distanceDown, wallLeftUp.transform.localScale.y, wallLeftUp.transform.localScale.z);
					wallLeftUp.transform.rotation = Quaternion.Euler (0, 180f, 0);

					Vector3 thisColliderPosition = this.transform.parent.transform.parent.position;
					Vector3 otherColliderPosition = collision.transform.parent.transform.parent.position;
					if (thisColliderPosition.x < otherColliderPosition.x) {
						wallRightUp.transform.rotation = Quaternion.Euler (0, 0, 0);
						wallLeftUp.transform.rotation = Quaternion.Euler (0, 0, 0);
						wallRightUp.transform.parent = collision.transform.parent;
						wallLeftUp.transform.parent = this.transform.parent;

						//Insert into DB
						boundaryObj.SendMessage ("InsertMapState", new object[]{thisPlayerID, "Front", JoinString(wallLeftUp.transform), "", playerCol});
						boundaryObj.SendMessage ("InsertMapState", new object[]{playerCol, "Front", JoinString(wallRightUp.transform), "", thisPlayerID});
					} else {
						wallRightUp.transform.parent = this.transform.parent;
						wallLeftUp.transform.parent = collision.transform.parent;


						//Insert into DB
						boundaryObj.SendMessage ("InsertMapState", new object[]{playerCol, "Front", JoinString(wallLeftUp.transform), "", thisPlayerID});
						boundaryObj.SendMessage ("InsertMapState", new object[]{thisPlayerID, "Front", JoinString(wallRightUp.transform), "", playerCol});
					}	
					isLeftUpCreated = true;
					isRightUpCreated = true;
					//Debug.Log (v);


				}
				//makeIsKinematic ();
			}
		}
	}

	public string JoinString(Transform transform){
		string pos, rot, scale;
		pos = transform.position.x.ToString () + ";" + transform.position.y.ToString () + ";" + transform.position.z.ToString ();
		rot = transform.rotation.eulerAngles.x.ToString () + ";" + transform.rotation.eulerAngles.y.ToString () + ";" + transform.rotation.eulerAngles.z.ToString ();
		scale = transform.lossyScale.x.ToString () + ";" + transform.lossyScale.y.ToString () + ";" + transform.lossyScale.z.ToString ();
		return pos + ";" + rot + ";" + scale;
	}

	[RPC]
	public void createRealMapOnClient(string playerID, Transform transform, GameObject wall1, GameObject wall2, string theWalltToBeDisappeared){

	}

	[RPC]
	public void checkIsDoneChoosingMap(string playerID, bool check){
		Debug.Log ("this is: " + playerID );
	}

}
