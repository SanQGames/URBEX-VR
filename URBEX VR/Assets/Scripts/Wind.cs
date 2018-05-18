using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public GameObject minHeight;
    public GameObject maxHeight;
    public AudioSource windAudio;
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
        CheckPos();
    }

    void CheckPos() {
        if (transform.position.y >= maxHeight.transform.position.y) {
            windAudio.volume = 1;
        }
        else if (transform.position.y <= minHeight.transform.position.y) {
            windAudio.volume = 0;
        }
        else if (transform.position.y >= minHeight.transform.position.y && transform.position.y <= maxHeight.transform.position.y) {
            windAudio.volume = minAudio + (transform.position.y - minHeight.transform.position.y) * (maxAudio - minAudio) / (maxHeight.transform.position.y - minHeight.transform.position.y);
        }
    }
}
