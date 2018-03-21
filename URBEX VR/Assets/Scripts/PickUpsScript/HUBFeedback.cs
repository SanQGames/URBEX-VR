using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBFeedback : MonoBehaviour {

	public Material[] materials; //0 unplayable, 1 playable, 2 bronze, 3 silver, 4 gold
	public GameObject buildingL1;
	public GameObject[] level1Cols;
	void Start () {

		bool l1 = BoolPrefs.GetBool ("l1");
		bool l1c1 = BoolPrefs.GetBool ("l1c1");
		bool l1c2 = BoolPrefs.GetBool ("l1c2");
		bool l1c3 = BoolPrefs.GetBool ("l1c3");

		if (!l1c1){
			print ("hinted c1");
			level1Cols [0].SetActive (false);
		}
		if (!l1c2){
			print ("hinted c2");
			level1Cols [1].SetActive (false);
		}
		if (!l1c3){
			print ("hinted c3");
			level1Cols [2].SetActive (false);
		}
		//de momento checkearemos si se ha pasado el nivel + coleccionables
		if (l1 && l1c1 && l1c2 && l1c3) { 
			buildingL1.GetComponent<Renderer> ().material = materials [3]; //silver
		} else if (l1) {
			buildingL1.GetComponent<Renderer> ().material = materials [2]; //bronze
		} else {
			buildingL1.GetComponent<Renderer> ().material = materials [1]; //playable
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
