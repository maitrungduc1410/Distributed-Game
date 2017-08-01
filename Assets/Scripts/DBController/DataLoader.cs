using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour {
	public string[] items;
	// Use this for initialization
	IEnumerator Start () {
		WWW itemsData = new WWW ("http://localhost/SkippingStones/person.php");
		yield return itemsData;
		string itemsDataString = itemsData.text;
		print (itemsDataString);
		items = itemsDataString.Split (';');
		print (GetDataValue (items [0], "Name: "));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	string GetDataValue(string data, string index){
		string value = data.Substring (data.IndexOf (index) + index.Length);
		return value;
	}
}
