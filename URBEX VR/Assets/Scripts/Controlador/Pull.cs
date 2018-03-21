using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pull : MonoBehaviour {

	public Rigidbody body;
	public OVRInput.Controller controller;
	public LayerMask climbMask;
	public string grabbableTag;
	public bool canGrip;

	[HideInInspector]
	public Vector3 prevPos;


	// Use this for initialization
	void Start () {
		grabbableTag = "Grabbable";
		prevPos = OVRInput.GetLocalControllerPosition (controller);
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log ("TRIGGER ENTER");
		if (col.gameObject.tag == grabbableTag) {
			canGrip = true;
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == grabbableTag) {
			canGrip = false;

		}
	}
}
