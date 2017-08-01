using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour {
	//public GameObject theGridItem;
	public int numberOfColums = 5;
	public int numberOfRows = 5;
	public float sperationValueX = 0.0f;
	public float sperationValueZ = 0.0f;
	private GameObject[] thePattern;
	private float tempSepX = 0;
	private float tempSepZ = 0;
	// Use this for initialization
	void Start () {
		createGrid ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void createGrid()
	{
		for (int i = 0; i < numberOfColums; i++) {
			for (int j = 0; j < numberOfRows; j++) {
				GameObject plane = GameObject.CreatePrimitive (PrimitiveType.Quad);
				//plane.AddComponent<ActiveMap> ();
				plane.transform.position = new Vector3(i + tempSepX, j + tempSepZ);
				plane.transform.eulerAngles = new Vector3 (0, 0, 0);
				tempSepZ += sperationValueZ;
			}
			tempSepX += sperationValueX;
			tempSepZ = 0;
		}
	}
}
