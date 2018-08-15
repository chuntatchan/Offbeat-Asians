using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicManager : MonoBehaviour {

	public static BGMusicManager instance = null;
	[SerializeField]
	private AudioSource source;
	[SerializeField]
	private AudioClip[] clips;
	[SerializeField]
	private int startingClip;


	void Awake () {
		if (instance == null) {
			//if not, set it to this.
			instance = this;
		}
		//If instance already exists:
		else if (instance != this) {
			//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
			Destroy (gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
	}

	// Use this for initialization
	void Start () {
		source.clip = clips [startingClip];
		source.Play ();
	}
}
