using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour {

	private bool colliding = false;
	private float counter = 0.0f;

	public OVRInput.Controller lController, rController;

	
	// Update is called once per frame
	void Update () {
		if (colliding && (OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, lController) > 0 || OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, rController) > 0)) {
			//I'm colliding with the door AND PRESSING BUTTON
			counter += Time.deltaTime;
			if (counter > 3.0f) {
				//CLOSE APP;
				Application.Quit();
			}
		}
	}

	void OnTriggerEnter(Collider col) {
		colliding = true;
	}

	void OnTriggerExit(Collider col) {
		colliding = false;
		counter = 0.0f;
	}
}
