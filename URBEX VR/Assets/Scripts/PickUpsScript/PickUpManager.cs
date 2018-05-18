using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour {

	// Use this for initialization
	public int level;
	public GameObject col1;
	public GameObject col2;
	public GameObject col3;
	string levelString;
	void Start () {
		levelString = "l" + level.ToString ();

		//desactivar colecionables si ya estan pillaos
		if (BoolPrefs.GetBool(levelString + "c1")){
			col1.GetComponent<PickCollectable> ().detectable = false;
			col1.GetComponent<PickCollectable>().HideObject();
		}
		if (BoolPrefs.GetBool(levelString + "c2")){
			col2.GetComponent<PickCollectable> ().detectable = false;
			col2.GetComponent<PickCollectable>().HideObject();
		}
		if (BoolPrefs.GetBool(levelString + "c3")){
			col3.GetComponent<PickCollectable> ().detectable = false;
			col3.GetComponent<PickCollectable>().HideObject();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("m")) {
			SetPickup (1, true);
			SetPickup (2, true);
			SetPickup (3, true);
			PlayerPrefs.SetInt ("l1", 4);
			PlayerPrefs.SetInt ("l2", 4);
		}
		if (Input.GetKeyDown ("r")) {
			SetPickup (1, false);
			SetPickup (2, false);
			SetPickup (3, false);
			PlayerPrefs.SetInt ("l1", 1);
			PlayerPrefs.SetInt ("l2", 0);
		}
	}
	public void SetPickup(int number, bool value){
		string pickupString = levelString + "c" + number.ToString ();
		BoolPrefs.SetBool (pickupString, value);
	}
}
