using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUp : MonoBehaviour {

	public float duration;

	// Use this for initialization
	void Start () {
		StartCoroutine (_destroyAfterSeconds());
	}

	IEnumerator _destroyAfterSeconds() {
		yield return new WaitForSeconds (duration);
		Destroy (gameObject);
	}

}
