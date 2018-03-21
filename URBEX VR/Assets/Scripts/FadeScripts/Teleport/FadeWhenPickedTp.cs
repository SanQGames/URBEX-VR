using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeWhenPickedTp : MonoBehaviour {

	// Use this for initialization
	public FadeScriptTp FadeManager;
	public OVRInput.Controller lController;
	public OVRInput.Controller rController;
	bool once;
	float counter; //counter to restart grab xd
	void Start () {
		once = true;
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("u"))
			FadeManager.StartFadeOut ();
	}
	void OnTriggerStay(Collider col) {
		if (col.gameObject.layer.Equals (10)) {
			if ((OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, lController) > 0 || OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, rController) > 0) && once) {
				once = false;
				FadeManager.StartFadeOut ();
			} else if (!once) {
				counter += Time.deltaTime;
				if (counter > 10.0f) {
					counter = 0;
					once = true;
				}
			}
		}

	}
}