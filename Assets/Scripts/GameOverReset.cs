using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverReset : MonoBehaviour {

	public GameObject p1Stats;
	public GameObject p2Stats;
	public GameObject levelManager;
	public GameObject playerInv;
	public GameObject animManager;

	// Use this for initialization
	void Start () {
		p1Stats = GameObject.FindGameObjectWithTag ("Player1");
		p2Stats = GameObject.FindGameObjectWithTag ("Player2");
		levelManager = GameObject.FindGameObjectWithTag ("levelManager");
		playerInv = GameObject.FindGameObjectWithTag ("Inventory");
		animManager = GameObject.Find ("AnimationManager");

		Destroy (p1Stats);
		Destroy (p2Stats);
		Destroy (levelManager);
		Destroy (playerInv);
		Destroy (animManager);

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene ("IntroScene");
		}
	}
}
