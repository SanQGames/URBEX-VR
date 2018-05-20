using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCollectable : MonoBehaviour {

	// Use this for initialization
	public int playerprefNumber; //1,2 or 3
	public OVRInput.Controller lController;
	public OVRInput.Controller rController;
	public PickUpManager manager;
	bool once;
	public bool detectable = true;
	public bool tutoCollictable = false;

	[Header("Feedback Sound")]
	public AudioSource audio;

	void Start () {
		if (playerprefNumber > 0)
			tutoCollictable = false;
		else
			tutoCollictable = true;
			 
		once = true;
	}

	// Update is called once per frame
	void Update () {
		if (playerprefNumber == 1 && Input.GetKeyDown ("y") && !tutoCollictable)
			ObjectPicked ();
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
		if (!tutoCollictable) manager.SetPickup (playerprefNumber, true);
		HideObject ();
	}
	public void ShowObject(){
		transform.localScale = new Vector3(1, 1, 1);
	}
	public void HideObject(){
		transform.localScale = new Vector3(0, 0, 0);
	}
}
