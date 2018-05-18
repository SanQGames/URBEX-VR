using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeWhenPicked : MonoBehaviour {

	// Use this for initialization
	public FadeScript FadeManager;
	public OVRInput.Controller lController;
	public OVRInput.Controller rController;
	bool once;
	public FinishedLevelManager finManager;
	public bool levelFinished;
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
				FadeManager.StartFadeOut ();
			}
			if (levelFinished) finManager.GetComponent<FinishedLevelManager> ().SetPrefabs ();
		}
			
	}
}