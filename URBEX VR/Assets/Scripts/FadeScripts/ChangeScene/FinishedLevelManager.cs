using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedLevelManager : MonoBehaviour {

	// Use this for initialization
	public int level;
	public float timeToBeat;
	float timer = 0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
	}
	public void SetPrefabs(){ //llamar cuando se acaba el nivel
		int counter = 2; //already sets to bronze
		string levelString = "l" + level.ToString();
		//analyze if all collectibles are picked
		if (BoolPrefs.GetBool(levelString + "c1") && BoolPrefs.GetBool(levelString + "c2") && BoolPrefs.GetBool(levelString + "c3"))
			counter++;
		
		if (timer < timeToBeat)
			counter++;

		PlayerPrefs.SetInt(levelString, counter);

		//activar l2
		int checkl2 = PlayerPrefs.GetInt ("l2");
		if (level == 1 && checkl2 == 0) {
			PlayerPrefs.SetInt ("l2", 1);
		}

	}
}
