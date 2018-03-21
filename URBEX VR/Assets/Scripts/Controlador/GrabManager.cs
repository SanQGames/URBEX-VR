using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
private float RotationScaleMultiplier = 1.0f;
public float RotationAmount = 1.5f;
private float SimulationRate = 60f;

//Update
Vector3 euler = transform.rotation.eulerAngles;
float rotateInfluence = SimulationRate * Time.deltaTime * RotationAmount * RotationScaleMultiplier;
Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
euler.y += secondaryAxis.x * rotateInfluence;
currentRotationAmount += secondaryAxis.x * rotateInfluence;
*/
public class GrabManager : MonoBehaviour {

	public Rigidbody body;
	public CharacterController charController;
	public CapsuleCollider charCollider;

	public Pull left;
	public Pull right;
	public OVRInput.Controller lController;
	public OVRInput.Controller rController;
	public Transform leftControllerObj;
	public Transform rightControllerObj;
	//public OVRPlayerController playerController;
	//public float rotationAmountY = 0.0f;

	private float prevRightControllerValue;
	private float prevLeftControllerValue;

	private bool leftGripping;
	private bool rightGripping;

	void Start() {
		prevRightControllerValue = OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, rController);
		prevLeftControllerValue = OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, lController);
		leftGripping = false;
		rightGripping = false;
		//rotationAmountY = playerController.currentRotationAmount;
	}

	void Update() {
		//rotationAmountY = playerController.currentRotationAmount;
	}

	void FixedUpdate () {

		bool isGripped = left.canGrip || right.canGrip;

		if (isGripped) {
			if (left.canGrip && OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, lController) > 0) {
				leftGripping = true;
				//Debug.Log ("PULL");
				//body.useGravity = false;
				//charController.attachedRigidbody.useGravity = false;
				charController.enabled = false;
				body.isKinematic = true;
				//charController.attachedRigidbody.isKinematic = true;
				Vector3 temp = (left.prevPos - leftControllerObj.position);
				//Vector3 tempRot = temp;
				//tempRot.x = temp.x * Mathf.Cos (-rotationAmountY) + temp.z * Mathf.Sin (-rotationAmountY);
				//tempRot.z = -temp.x * Mathf.Sin (-rotationAmountY) + temp.z * Mathf.Cos (-rotationAmountY);
				body.transform.position += temp;

			} else if(left.canGrip && (OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, lController) == 0 && prevLeftControllerValue > 0) ) {
				leftGripping = false;
				//body.useGravity = true;
				//charController.attachedRigidbody.useGravity = true;
				charController.enabled = true;
				body.isKinematic = false;
				//charController.attachedRigidbody.isKinematic = false;
				//body.transform.position += (left.prevPos - OVRInput.GetLocalControllerPosition (lController));
				body.AddForce( ((left.prevPos - leftControllerObj.position) / Time.deltaTime), ForceMode.VelocityChange );
			}

			if (right.canGrip && OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, rController) > 0) {
				rightGripping = true;
				//Debug.Log ("PULL");
				//body.useGravity = false;
				//charController.attachedRigidbody.useGravity = false;
				charController.enabled = false;
				body.isKinematic = true;
				//charController.attachedRigidbody.isKinematic = true;
				Vector3 temp = (right.prevPos - rightControllerObj.position);
				//Vector3 tempRot = temp;
				//tempRot.x = temp.x * Mathf.Cos (-rotationAmountY) + temp.z * Mathf.Sin (-rotationAmountY);
				//tempRot.z = -temp.x * Mathf.Sin (-rotationAmountY) + temp.z * Mathf.Cos (-rotationAmountY);
				body.transform.position += temp;

			} else if(!leftGripping && right.canGrip && (OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, rController) == 0 && prevRightControllerValue > 0)) {
				rightGripping = false;
				//body.useGravity = true;
				//charController.attachedRigidbody.useGravity = true;
				charController.enabled = true;
				body.isKinematic = false;
				//charController.attachedRigidbody.isKinematic = false;
				//body.transform.position += (right.prevPos - OVRInput.GetLocalControllerPosition (rController));
				//body.velocity = (right.prevPos - rightControllerObj.position) / Time.deltaTime;
				body.AddForce( ((right.prevPos - rightControllerObj.position) / Time.deltaTime), ForceMode.VelocityChange );
			}

		} else {
			//body.useGravity = true;
			charController.enabled = false;
			body.isKinematic = false;
			//charController.attachedRigidbody.isKinematic = false;
		}		

		/*
		if(OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, lController) == 0) {
			Debug.Log ("LEFT UP");
		}

		if(OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, rController) == 0) {
			Debug.Log ("RIGHT UP");
		}
		*/

		prevRightControllerValue = OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, rController);
		prevLeftControllerValue = OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, lController);

		//left.prevPos = OVRInput.GetLocalControllerPosition (lController);
		//right.prevPos = OVRInput.GetLocalControllerPosition (rController);

		left.prevPos = leftControllerObj.position;
		right.prevPos = rightControllerObj.position;
	}
}
