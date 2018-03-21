using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour {

	public GameObject fadeObject;
	bool isFadingOut;
	public int nextLevel;
	// Use this for initialization
	void Start () {
		isFadingOut = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isFadingOut) {
			if (fadeObject.GetComponent<OVRScreenFade> ().FadeOut ()) {
				SceneManager.LoadScene (nextLevel);
			}
		}
	}
	public void StartFadeOut(){
		isFadingOut = true;
	}
}