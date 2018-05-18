using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBFeedback : MonoBehaviour {

	public Material[] materials; //0 unplayable, 1 playable, 2 bronze, 3 silver, 4 gold
	public GameObject buildingL1;
	public GameObject[] level1Cols;

	public GameObject buildingL2;
	public GameObject[] level2Cols;
	void Start () {

		int l1 = PlayerPrefs.GetInt ("l1");
		bool l1c1 = BoolPrefs.GetBool ("l1c1");
		bool l1c2 = BoolPrefs.GetBool ("l1c2");
		bool l1c3 = BoolPrefs.GetBool ("l1c3");

		int l2 = PlayerPrefs.GetInt ("l2");
		bool l2c1 = BoolPrefs.GetBool ("l2c1");
		bool l2c2 = BoolPrefs.GetBool ("l2c2");
		bool l2c3 = BoolPrefs.GetBool ("l2c3");


		if (l1 == 0) {
			l1 = 1;
			PlayerPrefs.SetInt ("l1", 1);
			l2 = 0;
			PlayerPrefs.SetInt ("l2", 0);
			l1c1 = false;
			BoolPrefs.SetBool ("l1c1", false);
			l1c2 = false;
			BoolPrefs.SetBool ("l1c2", false);
			l1c3 = false;
			BoolPrefs.SetBool ("l1c3", false);
			l2c1 = false;
			BoolPrefs.SetBool ("l2c1", false);
			l2c2 = false;
			BoolPrefs.SetBool ("l2c2", false);
			l2c3 = false;
			BoolPrefs.SetBool ("l2c3", false);
		}

		if (!l1c1){
			level1Cols [0].SetActive (false);
		}
		if (!l1c2){
			level1Cols [1].SetActive (false);
		}
		if (!l1c3){
			level1Cols [2].SetActive (false);
		}
		if (!l2c1){
			level2Cols [0].SetActive (false);
		}
		if (!l2c2){
			level2Cols [1].SetActive (false);
		}
		if (!l2c3){
			level2Cols [2].SetActive (false);
		}

		buildingL1.GetComponent<Renderer> ().material = materials [l1];
		buildingL2.GetComponent<Renderer> ().material = materials [l2];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
