using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour {

	public CharacterControls movementController;
	//WIND AND CITY ARE CONTROLED BY Wind.cs

	public bool walking = false;
	public bool grounded = false;
	public bool pastGrounded = false;
	public bool step = false;

	public float timeUntilStep = 0.600f; //600ms
	public float timeSpent = 0.0f;

	[Header("STEPS")]
	public AudioClip[] steps;
	public AudioSource audioSteps;

	[Header("LANDING")]
	public AudioClip landingSound;
	public AudioSource audioLanding;

	void Update() {
		Vector2 thumbValue = OVRInput.Get (OVRInput.Axis2D.PrimaryThumbstick);
		if (thumbValue.x != 0 || thumbValue.y != 0 || Input.GetKey(KeyCode.W)) {
			walking = true;
		} else {
			walking = false;
		}

		if(grounded) timeSpent += Time.deltaTime;
		if(walking && grounded && timeSpent>=timeUntilStep) {
			step = true;
			timeSpent = 0.0f;
		}


	}

	void LateUpdate() {
		//walking = OVRInput.GetButton(Button.Axis2....);
		grounded = movementController.grounded;

		//STEPS
		if(step) {
			audioSteps.clip = SelectClip();
			audioSteps.Play();
			step = false;
		}

		//LANDING
		if (!pastGrounded && grounded) {
			audioLanding.clip = landingSound;
			audioLanding.Play();
		}


		pastGrounded = grounded;
	}

	AudioClip SelectClip() {
		int randomValue = Random.Range (0, 5);
		return steps [randomValue];
	}
}


