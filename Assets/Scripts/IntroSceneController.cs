using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSceneController : MonoBehaviour {

	[SerializeField]
	private CharacterFightScene[] players;
	[SerializeField]
	private GameObject playersLayer;
	[SerializeField]
	private GameObject continueButton;
	[SerializeField]
	private GameObject fadeOutScreen;
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

	public void startFadeOut() {
		StartCoroutine (fadeOut());
	}

	IEnumerator fadeOut() {
		print ("startFadeOut");
		float a = 0;
		fadeOutScreen.SetActive (true);
		Color screenColor = fadeOutScreen.GetComponent<Image> ().color;
		while (a < 1) {
			a += 0.04f;
			fadeOutScreen.GetComponent<Image> ().color = new Color (screenColor.r, screenColor.g, screenColor.b, a);
			yield return new WaitForEndOfFrame ();
		}
		_levelManager.LoadNextScene ();
	}

	IEnumerator walkIn() {
		Vector3 playersLayerStartPos = playersLayer.transform.position;
		for (int i = 0; i < players.Length; i++) {
			players [i].SetWalkingAnim (true);
		}
		while (playersLayer.transform.localPosition.x < 0) {
			playersLayer.transform.position = new Vector3 (playersLayerStartPos.x + 0.04f, playersLayerStartPos.y, playersLayerStartPos.z);
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
