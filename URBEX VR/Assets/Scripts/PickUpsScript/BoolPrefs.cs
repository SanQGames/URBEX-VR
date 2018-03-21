using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolPrefs : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	static public void SetBool(string name, bool value){
		PlayerPrefs.SetInt (name, value ? 1 : 0);
	}
	static public bool GetBool(string name){
		return PlayerPrefs.GetInt (name) == 1;
	}
}
