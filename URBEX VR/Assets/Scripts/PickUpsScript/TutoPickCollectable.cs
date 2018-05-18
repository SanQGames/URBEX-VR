﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoPickCollectable : MonoBehaviour {

	// Use this for initialization
	public OVRInput.Controller lController;
	public OVRInput.Controller rController;
	bool once;
	public bool detectable = true;

	[Header("Feedback Sound")]
	public AudioSource audio;

	void Start () {
		once = true;
	}

	// Update is called once per frame
	void Update () {
	}
	void OnTriggerStay(Collider col) {
		if (col.gameObject.layer.Equals (10)) {

			if ( (OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, lController) > 0 || OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, rController) > 0) && once ) {
				once = false;
				audio.Play ();
				ObjectPicked ();
			}
		}
	}
	void ObjectPicked(){
		detectable = false;
		HideObject ();
	}
	public void ShowObject(){
		transform.localScale = new Vector3(1, 1, 1);
	}
	public void HideObject(){
		transform.localScale = new Vector3(0, 0, 0);
	}
}