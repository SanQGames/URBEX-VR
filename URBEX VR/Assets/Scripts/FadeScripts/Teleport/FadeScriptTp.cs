using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScriptTp : MonoBehaviour {

	public GameObject fadeObject;
	bool fadeProcess;
	public Transform player;
	public Transform teleportPos;
	bool isFadingOut;
	float counterBeforeFadeIn;
	bool teleported;
	// Use this for initialization
	void Start () {
		fadeProcess = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (fadeProcess) {
			
			if (isFadingOut) {
				if (fadeObject.GetComponent<OVRScreenFade> ().FadeOut ()) {
					isFadingOut = false;
				}
			} 
			else {
				counterBeforeFadeIn += Time.deltaTime;
				if (counterBeforeFadeIn > 1.0f) {
					fadeProcess = false;
					fadeObject.GetComponent<OVRScreenFade> ().FadeIn ();
				} else if (counterBeforeFadeIn > 0.2f && !teleported) {
					player.position = teleportPos.position;
					teleported = true;
				}
			}
		}

	}
	public void StartFadeOut(){
		fadeProcess = true;
		isFadingOut = true;
		counterBeforeFadeIn = 0;
		teleported = false;
	}

}