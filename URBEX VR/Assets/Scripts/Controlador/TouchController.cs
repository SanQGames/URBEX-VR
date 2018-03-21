using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

	public OVRInput.Controller controller;

	// Update is called once per frame
	void Update () {
		this.transform.position = OVRInput.GetLocalControllerPosition (controller);
		this.transform.rotation = OVRInput.GetLocalControllerRotation (controller);
	}
}
