using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimationOffset : MonoBehaviour {

	public List<Animator> characters;

	private float speedMultiplierCounter;
	private float CycleOffsetCounter;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        characters.Clear();
        startAnimations();
    }

    // Use this for initialization
    void startAnimations () {

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < allEnemies.Length; i++)
        {
            if (allEnemies[i].GetComponent<Animator>() != null)
            {
                characters.Add(allEnemies[i].GetComponent<Animator>());
            }
        }

        GameObject[] allPlayer1 = GameObject.FindGameObjectsWithTag("Player1");
        for (int i = 0; i < allPlayer1.Length; i++)
        {
            if (allPlayer1[i].GetComponent<Animator>() != null)
            {
                characters.Add(allPlayer1[i].GetComponent<Animator>());
            }
        }

        GameObject[] allPlayer2 = GameObject.FindGameObjectsWithTag("Player2");
        for (int i = 0; i < allPlayer2.Length; i++)
        {
            if (allPlayer2[i].GetComponent<Animator>() != null)
            {
                characters.Add(allPlayer2[i].GetComponent<Animator>());
            }
        }

        for (int i = 0; i < characters.Count; i++) {
			speedMultiplierCounter = Random.Range (0.75f, 1f);
			CycleOffsetCounter = Random.Range (0, 1f);
			characters [i].SetFloat ("SpeedMultiplier", speedMultiplierCounter);
			characters [i].SetFloat ("CycleOffset", CycleOffsetCounter);
		}
	}

	// Idle-Break Animation (?)

}
