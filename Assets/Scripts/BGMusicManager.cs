using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicManager : MonoBehaviour {

	[SerializeField]
	private AudioSource source;
	[SerializeField]
	private AudioClip[] clips;
	[SerializeField]
	private int startingClip;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		source.clip = clips [startingClip];
		source.Play ();
	}
}
