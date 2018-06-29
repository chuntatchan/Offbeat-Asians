using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationOffset : MonoBehaviour {

	public GameObject[] characters;

	private float speedMultiplierCounter;
	private float CycleOffsetCounter;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < characters.Length; i++) {
			speedMultiplierCounter = Random.Range (0.75f, 1f);
			CycleOffsetCounter = Random.Range (0, 1f);
			characters [i].GetComponent<Animator> ().SetFloat ("SpeedMultiplier", speedMultiplierCounter);
			characters [i].GetComponent<Animator> ().SetFloat ("CycleOffset", CycleOffsetCounter);
		}
	}

	// Idle-Break Animation (?)

}
