using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPull : MonoBehaviour {

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

	// Update is called once per frame
	void Update () {
		if (canGrip && OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0) {
			//Debug.Log ("PULL");
			body.useGravity = false;
			body.isKinematic = true;
			body.transform.position += (prevPos - OVRInput.GetLocalControllerPosition (controller));

		} else {
			body.useGravity = true;
			body.isKinematic = false;
		}
		prevPos = OVRInput.GetLocalControllerPosition (controller);
	}

	void OnTriggerEnter(Collider col) {
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
