using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSceneController : MonoBehaviour {

	[SerializeField]
	private CharacterStats[] players;
	[SerializeField]
	private GameObject playersLayer;
	[SerializeField]
	private GameObject continueButton;
	[SerializeField]
	private LevelManager _levelManager;

	// Use this for initialization
	void Start () {
		continueButton.SetActive (false);
		StartCoroutine (walkIn());
		for (int i = 0; i < players.Length; i++) {
			DontDestroyOnLoad (players[i].gameObject);
		}
		DontDestroyOnLoad (_levelManager.gameObject);
	}

	IEnumerator walkIn() {
		Vector3 playersLayerStartPos = playersLayer.transform.position;
		for (int i = 0; i < players.Length; i++) {
			players [i].SetWalkingAnim (true);
		}
		while (playersLayer.transform.localPosition.x < 0) {
			playersLayer.transform.position = new Vector3 (playersLayerStartPos.x + 0.08f, playersLayerStartPos.y, playersLayerStartPos.z);
			playersLayerStartPos = playersLayer.transform.position;
			yield return new WaitForEndOfFrame ();
		}
		for (int i = 0; i < players.Length; i++) {
			players [i].SetWalkingAnim (false);
		}
		continueButton.SetActive (true);
		yield return 0;
	}
}
