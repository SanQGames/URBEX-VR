using UnityEngine;
using System.Collections;

//[RequireComponent (typeof (Rigidbody))]
//[RequireComponent (typeof (CapsuleCollider))]

public class CharacterControls : MonoBehaviour {

	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	public bool grounded = false;
	private bool prevGrounded = false;
	public GameObject forewardVector;
	public Rigidbody rigidbody;
	private Vector3 prevPos;



	void Awake () {
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
		prevPos = gameObject.transform.position;
	}

	void FixedUpdate () {
		canJump = false;
		if (grounded) {
			// Calculate how fast we should be moving
			Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= speed;

			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rigidbody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

			// Jump
			if (canJump && Input.GetButton("Jump")) {
				rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}
		} else if (!grounded && prevGrounded) {
			rigidbody.velocity += (gameObject.transform.position - prevPos);
		}
		// We apply gravity manually for more tuning control
		rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));

		prevGrounded = grounded;
		grounded = false;
		prevPos = gameObject.transform.position;
	}

	void OnCollisionStay () {
		grounded = true;    
	}

	float CalculateJumpVerticalSpeed () {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}