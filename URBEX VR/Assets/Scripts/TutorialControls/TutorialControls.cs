using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialControls : MonoBehaviour {

	public int HUB;

	public float firstDelay = 7.0f;
	public float secondDelay = 10.0f;
	private float countdown = 0.0f;
	public int index = 0;
	private bool fadedOut = false;

	Color col;
	public float fadeSpeed = 10.0f;

	public SpriteRenderer[] imagesGrab;
	public SpriteRenderer[] imagesMove;
	public SpriteRenderer[] imagesRotate;
	public SpriteRenderer controlsBase;
	public SpriteRenderer urbexLogo;
	public GameObject collectable;

	public int mode = 0;

	// Use this for initialization
	void Start () {
		countdown = firstDelay;
		if (PlayerPrefs.GetInt ("l0") == 0)
			HUB = 4;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			PlayerPrefs.SetInt ("l1", 1);
			PlayerPrefs.SetInt ("l2", 0);
			BoolPrefs.SetBool ("l1c1", false);
			BoolPrefs.SetBool ("l1c2", false);
			BoolPrefs.SetBool ("l1c3", false);
			BoolPrefs.SetBool ("l2c1", false);
			BoolPrefs.SetBool ("l2c2", false);
			BoolPrefs.SetBool ("l2c3", false);
		}

		countdown -= Time.deltaTime;
		//FIRST WE FADE OUT THE LOGO
		if (countdown < 0 && !fadedOut) {
			col = urbexLogo.color;
			col.a -= fadeSpeed;
			urbexLogo.color = col;
			if (col.a < 0) {
				fadedOut = true;
				//NOW WE ENABLE THE BASE IMAGE
				controlsBase.gameObject.SetActive(true);
				countdown = secondDelay;
			}
		}

		//ONCE THE LOGO IS FADED WE DON'T CARE ABOUT IT
		if(fadedOut) {
			switch (mode) {
			case 0:
				//GRAB
				foreach (SpriteRenderer rend in imagesGrab) {
					rend.gameObject.SetActive (true);
				}
				collectable.SetActive (true);
				if (countdown < 0) {
					mode++;
					countdown = secondDelay;
					foreach (SpriteRenderer rend in imagesGrab) {
						rend.gameObject.SetActive (false);
					}
					collectable.SetActive (false);
				}
				break;
			case 1:
				//MOVE
				foreach (SpriteRenderer rend in imagesMove) {
					rend.gameObject.SetActive (true);
				}
				if (countdown < 0) {
					mode++;
					countdown = secondDelay;
					foreach (SpriteRenderer rend in imagesMove) {
						rend.gameObject.SetActive (false);
					}
				}
				break;
			case 2:
				//ROTATE
				foreach (SpriteRenderer rend in imagesRotate) {
					rend.gameObject.SetActive (true);
				}
				if (countdown < 0) {
					foreach (SpriteRenderer rend in imagesRotate) {
						rend.gameObject.SetActive (false);
					}
					SceneManager.LoadScene (HUB);
				}
				break;
			}
		}
	}
}
