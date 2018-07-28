using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public List<string> transitionScenes;

	private int currentStage;
	[SerializeField]
	private string[] allStages;

	private void Start() {
        DontDestroyOnLoad(gameObject);
		currentStage = -1;
        assignTransitionScenes();
	}

    private void assignTransitionScenes()
    {
        for (int i = 0; i < allStages.Length; i = i + 2)
        {
            int j = Random.Range(0, transitionScenes.Count);
            allStages[i] = transitionScenes[j];
            transitionScenes.RemoveAt(j);
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
