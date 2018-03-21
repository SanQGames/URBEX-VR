using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPointer : MonoBehaviour
{

    public List<Transform> collectables;
    public Transform pointer;
    public Transform target;

    public float distToFlash = 10; //distancia para que empiece a hacer pitidos y eso
    public float distToLoseTrack = 25;

    float cEmission;
    float speed = 0;

    float timeCounter;
    float maxTimeCounter;

    bool reachedBlue = false;
    float prevPongValue;
    float pingPongCounter = 0;

    public AudioSource radarSound;
    bool effectPlayed; //esta variable esta en true mientras hay la animacion

	public bool useEffect; //hace todo lo de iluminarse
	bool morePossibleObjects = true;
    void Start()
    {
        timeCounter = 0;
        maxTimeCounter = 4;

        if (collectables.Count > 0)
        {
            target = FindNewTarget();
        }

        cEmission = 1;
    }

    // Update is called once per frame
    void Update()
    {
		//controlar que el player pueda quitar el sonido por si le es molesto

        //sistema buscar nuevo target + distancia actual
		if (collectables.Count > 0 && target != null && morePossibleObjects) {
			float curDist = Vector3.Distance (target.position, pointer.transform.position);
			if (curDist > distToLoseTrack || !target.GetComponent<PickCollectable> ().detectable) {
				target = FindNewTarget ();
			}

			if (curDist < distToFlash && useEffect) {
				if (timeCounter < maxTimeCounter) {
					//de 1 a 0 segun lo cerca que esté
					cEmission = curDist / distToFlash;
					if (cEmission >= 0.66f && speed != 2) {
						speed = 2;
						radarSound.pitch = 0.75f;
					} else if (cEmission > 0.33f && cEmission < 0.66f && speed != 4) {
						speed = 4;
						radarSound.pitch = 1.0f;
					} else if (cEmission <= 0.33f && speed != 8) {
						speed = 8;
						radarSound.pitch = 1.5f;
					}

					timeCounter += Time.deltaTime * speed;
				} else {
					if (!effectPlayed) {
						radarSound.Play ();
						effectPlayed = true;
					}
					if (FlashLight ())
						timeCounter = 0;
				}
			} else if (speed != 0) {
				speed = 0;

				if (effectPlayed) {
					Material mat = GetComponent<Renderer> ().material;

					mat.SetColor ("_EmissionColor", Color.black);
				}
			}


			//sistema apuntar a target
			Quaternion _lookRotation = Quaternion.LookRotation ((target.position - pointer.transform.position).normalized);

			//over time
			pointer.transform.rotation = Quaternion.Slerp (pointer.transform.rotation, _lookRotation, Time.deltaTime * 3.0f);
		}
    }

    Transform FindNewTarget()
    {
		int posTarget = 0;
		float minDis;

		minDis = 1000;

		for (int i = 0; i < collectables.Count; i++) {
			if (collectables [i].GetComponent<PickCollectable>().detectable) { 
				float tempDis = Vector3.Distance (collectables [i].position, pointer.transform.position);
			
				if (tempDis < minDis) {
					minDis = tempDis;
					posTarget = i;
				}
			}
		}
		if (!collectables [posTarget].GetComponent<PickCollectable> ().detectable) {
			useEffect = false;
			morePossibleObjects = false;
		}
		return collectables [posTarget];
    }

    bool FlashLight()
    {

        pingPongCounter += Time.deltaTime;
        float pingPongValue = Mathf.PingPong(pingPongCounter * speed, 1.0f);

        Material mat = GetComponent<Renderer>().material;
        Color lerpedColor = Color.Lerp(Color.black, Color.blue, pingPongValue);
        Color finalColor = lerpedColor * Mathf.LinearToGammaSpace(1.0f);

        mat.SetColor("_EmissionColor", finalColor);

        if (prevPongValue > pingPongValue && !reachedBlue)
        {
            reachedBlue = true;
        }

        if (pingPongValue < 0.1 && reachedBlue)
        {
            mat.SetColor("_EmissionColor", Color.black);
            prevPongValue = 0;
            pingPongCounter = 0;
            reachedBlue = false;
            effectPlayed = false;
            return true;
        }

        prevPongValue = pingPongValue;

        return false;
    }
}