using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public List<string> avaliableStages;

	private int currentStage;
	[SerializeField]
	private string[] allStages;

	private void Start() {
		currentStage = -1;
		for (int i = 0; i < allStages.Length; i++) {
			//allStages [i] =
		}
	}

	private string GetNextScene() {
		currentStage++;
		if (allStages.Length == currentStage) {
			return "Ending";
		} else {
			return allStages[currentStage];
		}
	}

	public void LoadNextScene() {
		SceneManager.LoadScene (GetNextScene());
	}
}
