using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSceneOnFall : MonoBehaviour {

	public int sceneToLoad = 2;

	void OnTriggerEnter(Collider col) {
		SceneManager.LoadScene (sceneToLoad);
	}
}
