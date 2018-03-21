using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {

	public OVRInput.Controller controller;
	public string buttonName;	//RHandTrigger - LHandTrigger
	public float grabRadius;
	public LayerMask grabMask;

	private GameObject grabbedObject;
	private bool grabbing;
	private Rigidbody grabbedObjectRig;


	void GrabObject() {
		grabbing = true;

		//Using raycast to detect objects. No Triggers used. The shere shoots towards a direction for a given lenght. In this case 0f since we want it to just appear.
		RaycastHit[] hits;
		hits = Physics.SphereCastAll (transform.position, grabRadius, transform.forward, 0f, grabMask);

		//Check if we hit something
		if (hits.Length > 0) {
			//Check for closest object hit in case we hit multiple
			int closestObjectHit = 0;
			for (int i = 0; i < hits.Length; i++) {
				if (hits [i].distance < hits [closestObjectHit].distance) {	closestObjectHit = i; }
			}

			grabbedObject = hits [closestObjectHit].transform.gameObject; //Closest object stored into grabbedObject.
			grabbedObjectRig = grabbedObject.GetComponent<Rigidbody>();
			grabbedObjectRig.isKinematic = true;
			grabbedObject.transform.position = this.transform.position;
			grabbedObject.transform.parent = this.transform;
		}
	}

	void DropObject() {
		grabbing = false;

		if (grabbedObject != null) {
			grabbedObject.transform.parent = null;
			grabbedObjectRig.isKinematic = false;

			grabbedObjectRig.velocity = OVRInput.GetLocalControllerVelocity (controller);
			grabbedObjectRig.angularVelocity = OVRInput.GetLocalControllerAngularVelocity (controller);

			grabbedObject = null;
		}

	}

	// Update is called once per frame
	void Update () {
		
		if (!grabbing && Input.GetAxis (buttonName) == 1) {
			//Debug.Log ("Right Hand Trigger Pressed");

			GrabObject ();
		} else if (grabbing && Input.GetAxis (buttonName) < 1) {
			//Debug.Log ("Right Hand Trigger Released");

			DropObject ();
		}
		if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0) {
			//Debug.Log ("Right Hand Trigger Pressed with OVRInput");

			//GrabObject ();
		} else if (grabbing && OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, controller) < 1) {
			//Debug.Log ("Right Hand Trigger Released with OVRInput");

			//DropObject ();
		}



	}

	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(0.5f, 0f, 1f, 0.5f);
		Gizmos.DrawSphere(transform.position, grabRadius);
	}
}
