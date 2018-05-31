using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeWhenPicked : MonoBehaviour {

	// Use this for initialization
	[Header("ReadySound")]
	public AudioSource audioReady;

	public FadeScript FadeManager;
	public OVRInput.Controller lController;
	public OVRInput.Controller rController;
	bool once;
	public FinishedLevelManager finManager;
	public bool levelFinished;
	public bool grabable;
	void Start () {
		once = true;
		grabable = true;
	}

	void OnTriggerStay(Collider col) {
		if (col.gameObject.layer.Equals (10)) {

			if ( (OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, lController) > 0 || OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, rController) > 0) && once ) {
				once = false;
				audioReady.Play ();
				FadeManager.StartFadeOut ();
			}
			if (levelFinished) finManager.GetComponent<FinishedLevelManager> ().SetPrefabs ();
		}
			
	}
	public void CantGrab(){
		grabable = false;
	}
}