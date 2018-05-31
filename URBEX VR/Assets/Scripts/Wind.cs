using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

	public bool checkForHeight = true;
    public GameObject minHeight;
    public GameObject maxHeight;
    public AudioSource windAudio;
	public AudioSource cityAudio;
    public float maxAudio;
    public float minAudio;

    // Use this for initialization
    void Start() {
        maxAudio = 1;
        minAudio = 0;
        CheckPos();
    }

    // Update is called once per frame
    void Update() {
		if (checkForHeight) {
			CheckPos ();
		} else {
			windAudio.volume = 0.5f;
			cityAudio.volume = 0.5f;
		}
		
    }

    void CheckPos() {
        if (transform.position.y >= maxHeight.transform.position.y) {
            windAudio.volume = 0.8f;
			cityAudio.volume = 0.2f;
        }
        else if (transform.position.y <= minHeight.transform.position.y) {
            windAudio.volume = 0.2f;
			cityAudio.volume = 0.8f;
        }
        else if (transform.position.y >= minHeight.transform.position.y && transform.position.y <= maxHeight.transform.position.y) {
			float volumeValue = minAudio + (transform.position.y - minHeight.transform.position.y) * (maxAudio - minAudio) / (maxHeight.transform.position.y - minHeight.transform.position.y);
			windAudio.volume = volumeValue;
			cityAudio.volume = 1.0f - volumeValue;
        }
    }
}
